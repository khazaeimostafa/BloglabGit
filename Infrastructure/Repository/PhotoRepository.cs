using System.Data;
using Core.Entities;
using Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IConfiguration config;

        public PhotoRepository(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<int> DeleteAsync(int photoId)
        {
            int affectedRows = 0;
            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();
                affectedRows =
                    await connection
                        .ExecuteAsync("Photo_Delete",
                        new { PhotoId = photoId },
                        commandType: CommandType.StoredProcedure);

                return affectedRows;
            }
        }

        public async Task<List<Photo>>
        GetAllByUserIdAsync(string applicationUserId)
        {
            IEnumerable<Photo> photos;
            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();
                photos =
                    await connection
                        .QueryAsync<Photo>("Photo_GetByUserId",
                        new { ApplicationUserId = applicationUserId },
                        commandType: CommandType.StoredProcedure);
            }

            return photos.ToList();
        }

        public async Task<Photo> GetAsync(int photoId)
        {
            Photo photo;
            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();
                photo =
                    await connection
                        .QueryFirstOrDefaultAsync<Photo>("Photo_Get",
                        new { PhotoId = photoId },
                        commandType: CommandType.StoredProcedure);
            }

            return photo;
        }

        public async Task<Photo>
        InsertAsync(PhotoCreate photoCreate, string applicationUserId)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("PublicId", typeof(string));
            dataTable.Columns.Add("ImageUrl", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            int newPhotoId;

            dataTable
                .Rows
                .Add(photoCreate.PublicId,
                photoCreate.ImageUrl,
                photoCreate.Description);

            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();

                newPhotoId =
                    await connection
                        .ExecuteScalarAsync<int>("Photo_Insert",
                        new
                        {
                            Photo = dataTable.AsTableValuedParameter("dbo.PhotoType"),
                            applicationUserId = applicationUserId
                        },
                        commandType: CommandType.StoredProcedure);
            }

            Photo photo = await GetAsync(newPhotoId);
            return photo;
        }
    }
}
