using Core.Entities;
using Infrastructure.Repository;
using Met.MetExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentController : ControllerBase
    {
        private readonly BlogRepository blogRepository;

        private readonly BlogCommentRepository blogCommentRepository;

        public BlogCommentController(
            BlogRepository blogRepository,
            BlogCommentRepository blogCommentRepository
        )
        {
            this.blogRepository = blogRepository;
            this.blogCommentRepository = blogCommentRepository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BlogComment>>
        Create(BlogCommentCreate model)
        {
            Blog blog = await blogRepository.GetAsync(model.BlogId);

            string userId = HttpContext.User.MkhUserId();

            if (blog == null) return BadRequest("Blog Not Exxist");

            BlogComment blogComment =
                await blogCommentRepository.UpsertAsync(model, userId);

            return Ok(blogComment);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<BlogComment>> Get(int model)
        {
            BlogComment blogComment =
                await blogCommentRepository.GetAsync(model);

            return Ok(blogComment);
        }

        [HttpPost("Blog-All-Comments")]
        public async Task<ActionResult<List<BlogComment>>> GetAll(int blogId)
        {
            return (await blogCommentRepository.GetAllAsync(blogId));
        }

        [HttpPost("BogComment-Delete")]
        public async Task<ActionResult<int>> Delete(int model)
        {
            string userId = HttpContext.User.MkhUserId();

            BlogComment comment = (BlogComment) Get(model).Result.Value;

            if (comment == null) return BadRequest("Comment Does Not Exist");

            if (comment.ApplicationUserId == userId)
            {
                var affectedRows =
                    await blogCommentRepository.DeleteAsync(model);

                return Ok(affectedRows);
            }
            else
            {
                return BadRequest("THis Comment Was Not Created By Current User");
            }
        }
    }
}
