using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Models;
using Met.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Met.MetExtensions
{
    public static class AuthExtensions


    {

        public static string MkhUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return (claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);


        }
        public static async Task<AppUser> MkhFindUserByEmail(this UserManager<AppUser> userManager,
            ClaimsPrincipal claimsPrincipal)
        {
            string email = claimsPrincipal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await userManager.FindByEmailAsync(email);

        }


        public static async Task<bool>
        MkhCheckEmailExistsAsync(
            this UserManager<AppUser> userManager,
            string EmailAddress
        )
        {
            return await userManager.FindByEmailAsync(EmailAddress) != null
                ? true
                : false;
        }
    }
}
