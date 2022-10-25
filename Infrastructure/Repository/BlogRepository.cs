using System.Data;
using Core.Entities;
using Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IConfiguration config;

        public BlogRepository(IConfiguration config)
        {
            this.config = config;
        } 

        public async Task<int> DeleteAsync(int blogId)
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
                        .ExecuteAsync("Blog_Delete",
                        new { BlogId = blogId },
                        commandType: CommandType.StoredProcedure);
                return affectedRows;
            }
        }

        public async Task<PagedResults<Blog>> GetAllAsync(BlogPaging blogPaging)
        {
            var results = new PagedResults<Blog>();
            var param = new DynamicParameters();
            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();
                using (
                    var multi =
                        await connection
                            .QueryMultipleAsync("Blog_GetAll",
                            new {
                                Offset =
                                    (blogPaging.Page - 1) * blogPaging.PageSize,
                                PageSize = blogPaging.PageSize
                            },
                            commandType: CommandType.StoredProcedure)
                )
                {
                    results.Items = multi.Read<Blog>();
                    results.TotalCount = multi.ReadFirst<int>();
                }
            }

            return results;
        }

        public async Task<List<Blog>> GetAllByUserIdAsync(string applicationUserId)
        {
            IEnumerable<Blog> Blogs;
            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();
                Blogs =
                    await connection
                        .QueryAsync<Blog>("Blog_GetByUserId",
                        new { ApplicationUserId = applicationUserId },
                        commandType: CommandType.StoredProcedure);
            }

            return Blogs.ToList();
        }

        public async Task<List<Blog>> GetAllFamousAsync()
        {
            IEnumerable<Blog> Blogs;
            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();
                Blogs =
                    await connection
                        .QueryAsync<Blog>("Blog_GetAllFamous",
                        new { },
                        commandType: CommandType.StoredProcedure);
            }

            return Blogs.ToList();
        }

        public async Task<Blog> GetAsync(int blogId)
        {
            Blog blog;

            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();
                blog =
                    await connection
                        .QueryFirstOrDefaultAsync<Blog>("Blog_Get",
                        new { BlogId = blogId },
                        commandType: CommandType.StoredProcedure);
            }
            return blog;
        }

        public async Task<Blog>
        UpsertAsync(BlogCreate blogCreate, string applicationUserId)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("BlogId", typeof (int));
            dataTable.Columns.Add("Title", typeof (string));
            dataTable.Columns.Add("Content", typeof (string));
            dataTable.Columns.Add("PhotoId", typeof (int));

            dataTable
                .Rows
                .Add(blogCreate.BlogId,
                blogCreate.Title,
                blogCreate.Content,
                blogCreate.PhotoId);

            int? newBlogId;
            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync();
                newBlogId =
                    await connection
                        .ExecuteScalarAsync<int?>("Blog_Upsert",
                        new {
                            Blog =
                                dataTable
                                    .AsTableValuedParameter("dbo.BlogType"),
                            ApplicationUserId = applicationUserId
                        },
                        commandType: CommandType.StoredProcedure);
            }
            newBlogId = newBlogId ?? blogCreate.BlogId;
            Blog blog = await GetAsync(newBlogId.Value);

            return blog;
        }
    }
}
