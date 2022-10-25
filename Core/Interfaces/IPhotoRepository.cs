using Core.Entities;

namespace Core.Interfaces
{
    public interface IPhotoRepository
    {
         public Task<Photo> InsertAsync(PhotoCreate photoCreate ,string applicationUserId);
         
         public   Task<Photo> GetAsync(int photoId);


         public Task<List<Photo>> GetAllByUserIdAsync(string applicationUserId);

         public Task<int> DeleteAsync(int photoId);
    
    }
}