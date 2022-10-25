using CloudinaryDotNet.Actions;
using Core.Entities;
using Core.Interfaces;
using Core.ServicesInterfaces;
using Met.MetExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository photoRepository;

        private readonly IBlogRepository blogRepository;

        private readonly IPhotoService photoService;

        public PhotoController(
            IPhotoRepository photoRepository,
            IBlogRepository blogRepository,
            IPhotoService photoService
        )
        {
            this.photoRepository = photoRepository;
            this.blogRepository = blogRepository;
            this.photoService = photoService;
        }

        [HttpPost("Upload-Photo")]
        public async Task<ActionResult<Photo>> UploadPhoto(IFormFile file)
        {
            var userId = HttpContext.User.MkhUserId();

            ImageUploadResult uploadResult =
                await photoService.AddPhotoAsync(file);
            if (uploadResult.Error != null)
                return BadRequest(uploadResult.Error);
            PhotoCreate photoCreate =
                new PhotoCreate {
                    PublicId = uploadResult.PublicId,
                    ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                    Description = file.FileName
                };
            Photo photo =
                await photoRepository.InsertAsync(photoCreate, userId);
            return Ok(photo);
        }

        //GetByApplicationUserId
        [HttpGet("Get-By-Id")]
        public async Task<ActionResult<Photo>> GetById(int model)
        {
            Photo photo = await photoRepository.GetAsync(model);

            return Ok(photo);
        }

        [HttpGet("Get-By-ApplicationUser-Id")]
        public async Task<ActionResult<List<Photo>>> GetByApplicationUserId()
        {
            var userId = HttpContext.User.MkhUserId();

            List<Photo> photos =
                await photoRepository.GetAllByUserIdAsync(userId);

            return Ok(photos);
        }

        [HttpPost("Delete-Async")]
        public async Task<ActionResult<int>> DeleteAsync(int photoId)
        {
            var delPhoto = await photoRepository.GetAsync(photoId);

            var userId = HttpContext.User.MkhUserId();
            if (delPhoto != null)
            {
                if (delPhoto.ApplicationUserId == userId)
                {
                    var blogs =
                        await blogRepository.GetAllByUserIdAsync(userId);
                    var usedInBlogs = blogs.Any(x => x.PhotoId == photoId);

                    if (usedInBlogs)
                    {
                        return BadRequest("Can Not delete This Photo Because Used In Blog(s)");
                    }

                    var deletedResult =
                        await photoService.DeletePhotAsync(delPhoto.PublicId);
                    if (deletedResult.Error != null)
                    {
                        return BadRequest(deletedResult.Error.Message);
                    }
                    var affectedRow =
                        await photoRepository.DeleteAsync(photoId);

                    return Ok(affectedRow);
                }
                else
                {
                    return BadRequest("This User was not uploaded By The Current User");
                }
            }

            return BadRequest("Photo Does Not Exist");
        }
    }
}
