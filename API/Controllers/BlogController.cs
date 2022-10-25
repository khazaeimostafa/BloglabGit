using Core.Entities;
using Infrastructure.Repository;
using Met.MetExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogRepository blogRepository;
        private readonly PhotoRepository photoRepository;

        public BlogController(BlogRepository blogRepository,
            PhotoRepository photoRepository)
        {
            this.blogRepository = blogRepository;
            this.photoRepository = photoRepository;
        }

        [HttpPost("Delete-Blog")]
        public async Task<ActionResult<int>> DeleteAsync(int model)
        {


            string userId = HttpContext.User.MkhUserId();


            Blog delBlog = await blogRepository.GetAsync(model);

            if (delBlog == null) return BadRequest("Cant Delete Blog Becuse Current Blog Not Exist");

            if (delBlog.ApplicationUserId == userId)
            {
                var affectedRow = await blogRepository.DeleteAsync(model);

                return Ok(affectedRow);
            }

            return BadRequest("You didn't Create This Blog");



        }


        [HttpPost("Create-Blog")]
        public async Task<ActionResult<Blog>> Create(BlogCreate model)
        {
            string userId = HttpContext.User.MkhUserId();

            if (model.PhotoId.HasValue)
            {
                var photo = await photoRepository.GetAsync(model.PhotoId.Value);

                if (photo.ApplicationUserId != userId)
                {
                    return BadRequest("You Did Not Upload Photo");
                }
            }

            var blog = await blogRepository.UpsertAsync(model, userId);

            return Ok(blog);
        }


        [HttpPost("Get-All")]
        public async Task<ActionResult<PagedResults<Blog>>> GetAll([FromQuery] BlogPaging model)
        {
            PagedResults<Blog> blogs = await blogRepository.GetAllAsync(model);

            return blogs;
        }


        [HttpPost("Get")]
        public async Task<ActionResult<Blog>> Get(int model)
        {
            Blog blog = await blogRepository.GetAsync(model);

            return Ok(blog);
        }


        [HttpGet("user/applicationUserId")]
        public async Task<ActionResult<List<Blog>>> GetByApplicationUserId(string model)
        {
            var blogs = await blogRepository.GetAllByUserIdAsync(model);

            return Ok(blogs);
        }

        [HttpGet("blogs/famous")]
        public async Task<ActionResult<List<Blog>>> Famous()
        {
            var blogs = await blogRepository.GetAllFamousAsync();

            return Ok(blogs);
        }

    }
}