
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Core.ServicesInterfaces
{
    public interface IPhotoService
    {
         public Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
         public Task<DeletionResult> DeletePhotAsync(string publicId);

    }
}