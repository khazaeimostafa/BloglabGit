using API.Errors;
using Core.Models;
using Met.DTOs;
using Met.Entities;
using Met.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Filters
{
    public class LoginValidationAttribute : IAsyncActionFilter
    {
        private readonly IdentityEmailService EmailService;
        private readonly UserManager<AppUser> userManager;

        public LoginValidationAttribute(IdentityEmailService emailService, UserManager<AppUser> userManager)
        {
            this.EmailService = emailService;
            this.userManager = userManager;
        }

        public async Task
        OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {

            #region mkhCommwnts
            //var modelstate = new ModelStateDictionary();
            ////modelstate.AddModelError("Khata", "Error in Filter");
            //modelstate.AddModelError("", "Error in Filter2");
            ////modelstate.TryAddModelError(StatusCodes.Status415UnsupportedMediaType, "415 Error");

            ////context.Result = new StatusCodeResult(StatusCodes.Status423Locked);
            //context.Result =
            //     new BadRequestObjectResult(modelstate);

            //context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);


            //......................................................


            //context.Result = new ContentResult
            //{
            //    Content = "شما اجازه ورود ندارید",
            //    StatusCode = StatusCodes.Status405MethodNotAllowed
            //};

            //return;



            //......................



            //context.Result =
            //     new BadRequestObjectResult(new
            //      ApiResponse(405,"ApiResponse Eroor"));
            //return;

            #endregion


            if (!context.ModelState.IsValid)
            {

                ContentResult result = new ContentResult();
                result.Content = "داده های ورودی شما نامعتبر می باشد";
                result.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = result;

                return;
            }

            LoginRequest model =
                (LoginRequest)context.ActionArguments["model"];

            if (model == null)
            {

                context.Result = new BadRequestObjectResult(model.EmailAddress);

                return;
            }

            AppUser user =
                await userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {
                context.Result = new BadRequestObjectResult("Such User Not Exist");
                return;
            }

            if (user != null && await userManager.IsLockedOutAsync(user))
            {
                context.Result =
                    new BadRequestObjectResult("Your Account is Locked");

                return;
            }

            if (user != null && !(await userManager.IsEmailConfirmedAsync(user))
            )
            {

                await EmailService.SendAccountConfirmEmail(user,
              "ConfirmAccount", "Account");
                context.Result =
                    new BadRequestObjectResult("حساب کاربریتان تایید نشده \n ایمیل تایید حساب برای شما ارسال شده انرا بررسی کیند");

                return;
            }
            else
            {
                context.HttpContext.Items.Add("userExist", user);
                await next();
            }
        }
    }
}
