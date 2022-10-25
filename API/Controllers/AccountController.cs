using System.Security.Claims;
using API.Filters;
using API.Fluent;
using API.Helper;
using Infrastructure.Services;
using Met.DTOs;
using Met.Data;
using Met.MetExtensions;
using Met.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using fv = FluentValidation.Results;
using Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HttpsOnly]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppIdentityDbContext context;
        private readonly IdentityEmailService EmailService;
        private readonly TokenUrlEncoderService TokenUrlEncoder;
        private readonly SignInManager<AppUser> SignInManager;
        private readonly IConfiguration configuration;
        private readonly TokenValidationParameters tokenValidationParameters;

        public AccountController(UserManager<AppUser> userManager,
            AppIdentityDbContext context,
            IdentityEmailService emailService,
            TokenUrlEncoderService encoder,
            SignInManager<AppUser> signinManager,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters
        )
        {
            this.userManager = userManager;
            this.context = context;
            this.EmailService = emailService;
            this.TokenUrlEncoder = encoder;
            this.SignInManager = signinManager;
            this.configuration = configuration;
            this.tokenValidationParameters = tokenValidationParameters;

        }

        [HttpPost(Name = "Register-Account")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {

            RegisterResponse res = new RegisterResponse();

            var validator = new RegisterVMValidator();
            fv.ValidationResult validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors[0].ErrorMessage);
            }


            if (await userManager.MkhCheckEmailExistsAsync(model.EmailAddress))
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                "Email address is in use");
                
            }

            AppUser UserExist =
                await userManager.FindByEmailAsync(model.EmailAddress);

            if (
                UserExist != null &&
                !await userManager.IsEmailConfirmedAsync(UserExist)
            )
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                "User is exist but not confirmed Please Confirm Your Account");
            }

            if (UserExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                $"User by  {model.EmailAddress} EmailAddress is Exist");
            }

            AppUser user =
                new AppUser
                {
                    Email = model.EmailAddress,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName
                };

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                AppUser appUser =
                    await userManager.FindByEmailAsync(model.EmailAddress);

                switch (model.Role)
                {
                    case UserRoles.Student:
                        await userManager
                            .AddToRoleAsync(user, UserRoles.Student);
                        break;
                    case UserRoles.Manager:
                        await userManager
                            .AddToRoleAsync(user, UserRoles.Manager);
                        break;
                    default:
                        break;
                }

                await userManager
                    .AddClaimAsync(appUser, new Claim("guest", "true"));

                await context.SaveChangesAsync();
                string userToken =
                    await EmailService
                        .SendAccountConfirmEmail(appUser,
                        "ConfirmAccount",
                        "Account");
                res.Url = userToken;
                res.IsSuccessfull = true;
                return Ok(res);
            }


            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("Login")]
        [ServiceFilter(typeof(LoginValidationAttribute))]
        public async Task<IActionResult> Login(LoginRequest model)

        {
           
            AppUser user = HttpContext.Items["userExist"] as AppUser;

            #region LoginValidationAttributeOverride
            //if (user == null)
            //{
            //    return BadRequest("Such User Not Exist");
            //}


            //if (user != null && !await userManager.IsEmailConfirmedAsync(user))
            //{
            //    await EmailService.SendAccountConfirmEmail(user,
            //    "ConfirmAccount", "Account");

            //    return StatusCode(StatusCodes.Status403Forbidden,
            //       "  Please First Confirm Your Account");
            //} 
            #endregion


            //if (await userManager.CheckPasswordAsync(user: user, model.Password)
            if (await userManager.CheckPasswordAsync(user: user,model.Password)
         )
            {
                AuthResultResponse userToken =
                    await user
                        .GenerateJWTTokenAsync(userManager,
                        context,
                        configuration);

                var response =
                    new UserAuthBase()
                    {
                        appUserAuth = userToken,
                        Claims =
                            (await userManager.GetClaimsAsync(user)).ToList(),
                        IsAuthenticated = true,
                        UserId = user.Id,
                        UserName = user.UserName
                    };

                return Ok(response);
            }

            return BadRequest("Error in Your Login");

        }

        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            Microsoft.Extensions.Primitives.StringValues headers =
                HttpContext.Request.Headers["Authorization"];

            AppUser user =
                await userManager.MkhFindUserByEmail(HttpContext.User);

            UserAuthBase response =
                new UserAuthBase()
                {
                    appUserAuth =
                        await user
                            .GenerateJWTTokenAsync(userManager,
                            context,
                            configuration,
                            null),
                    Claims = (await userManager.GetClaimsAsync(user)).ToList(),
                    IsAuthenticated = true,
                    UserId = user.Id,
                    UserName = user.UserName
                };

            return Ok(response);
        }

        [HttpGet]
        [Route("All-Users")]
        public async Task<IActionResult> AllUsers()
        {
            return Ok(await userManager.Users.ToListAsync());
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResultResponse>>
        RefreshToken(RefreshTokenRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Pease, Provide all Fields");
            }

            return Ok(await userManager
                .VerifyAndGenerateTokenAsync(model,
                context,
                tokenValidationParameters,
                configuration));
        }

        [HttpPost("ConfirmAccount")]
        public async Task<ActionResult>
       ConfirmAccount(RegisterConfirmDTO model)
        {

            if (model is null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, model.ShowConfirmedMessage ? model.ShowConfirmedMessage : "Null Model");

            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid");
            }


            AppUser user = await userManager.FindByEmailAsync(model.Email);

            if (user == null) return BadRequest("null User");

            if (user != null && !await userManager.IsEmailConfirmedAsync(user))
            {
                string decodeToken =
                      TokenUrlEncoder.DecodeToken(model.Token);

                IdentityResult result = await userManager.ConfirmEmailAsync(user, decodeToken);

                if (result.Succeeded)
                {
                    //var userLoginInfo = await userManager.FindByEmailAsync(model.Email);
                    //userLoginInfo.EmailConfirmed = true;
                    //model.ShowConfirmedMessage = true;
                    await context.SaveChangesAsync();
                    return Ok(userManager.IsEmailConfirmedAsync(user));
                }

                return BadRequest("Unvalid Email Confirm");
            }
            return Ok("Fake Token");

        }


        [HttpPost("ConfirmEmailResend")]
        public async Task<ActionResult>
             ConfirmEmailResend(string model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please INsert Email");
            }

            AppUser user = await userManager.FindByEmailAsync(model);

            if (user == null)
            {
                return BadRequest("The User Not Exist");
            }

            if (user != null &&
                 await userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest("Your Account is confirmed");
            }
            if (user != null &&
                !await userManager.IsEmailConfirmedAsync(user))
            {

                string token = await EmailService.SendAccountConfirmEmail(user, "ConfirmAccount", "Account");
                return StatusCode(StatusCodes.Status200OK, token);

            }

            return BadRequest("bad");

        }


        [HttpPost("Delete-Account")]
        public async Task<IActionResult> DeleteAccount(string model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model Is not Valid");
            }

            AppUser user = await userManager.FindByEmailAsync(model.ToString());


            if (user == null)
            {
                return BadRequest($"User With {model} Email Not Exist");
            }


            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok($"User With {model} Email Deleted");
            }


            return BadRequest(result.Errors.ToList());

        }
        [HttpGet]
        [AllowAnonymous]
        [Route("fake")]
        public async Task<ActionResult> fake()
        {
            return Ok(new fake { MyProperty = "Fake11111111111" });
        }

    }



    public class fake
    {
        
        public string MyProperty { get; set; }
    }

}
