using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Entities;
using Core.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;

        public PhotoService(IOptions<CloudinaryOptions> config)
        {
            Account account =
                new Account(config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret);
            cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    ImageUploadParams uploadParams =
                        new ImageUploadParams {
                            File = new FileDescription(file.Name, stream),
                            Transformation =
                                new Transformation()
                                    .Height(300)
                                    .Width(500)
                                    .Crop("fill")
                        };

                        uploadResult =  await cloudinary.UploadAsync(uploadParams);

                }
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotAsync(string publicId)
        {
            DeletionParams deletionParams =  new DeletionParams(publicId);
            DeletionResult result =  await cloudinary.DestroyAsync(deletionParams);

             return result;

        }
    }
}
