using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API.Helper;
using API.Errors;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public BuggyController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("notfound")]
        public async Task<ActionResult> GetNotFoundResult()
        {
            AppUser thing = await userManager.FindByEmailAsync("h43r");
            if (thing != null)
            {
                NotFoundObjectResult mkhNotFoundresult = new NotFoundObjectResult(
                    new
                    {
                        title = "Not Found",
                        status = 404
                    });

                return mkhNotFoundresult;
            }

            var mkhOKResult = new OkObjectResult(
                    new
                    {
                        title = "Found",
                        status = 200
                    });

            return mkhOKResult;
        }



        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            var response =
                new ErrorResponse { status = StatusCodes.Status400BadRequest, message = "400 Error" };


            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
        }
    }
}
