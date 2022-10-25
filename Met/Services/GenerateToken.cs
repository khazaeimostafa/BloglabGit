using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Met.DTOs;
using Met.Data;
using Met.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core.Models;

namespace Met.Services
{

    public static class GenerateToken
    {
        public static async Task<AuthResultResponse>
        GenerateJWTTokenAsync(
            this AppUser user,
            UserManager<AppUser> userManager,
            AppIdentityDbContext context,
            IConfiguration configuration,
            FreshToken freshToken = null
        )
        {
            List<Claim> authClaims =
            new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
         };

            IList<Claim> userClaims = await userManager.GetClaimsAsync(user);

            foreach (Claim item in userClaims)
            {
                authClaims.Add(item);
            }


            IList<string> userRoles = await userManager.GetRolesAsync(user);

            foreach (string item in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }

            SymmetricSecurityKey symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:secret"]));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,
                SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token =
             new JwtSecurityToken(issuer: configuration["JWT:Issuer"],
                 audience: configuration["JWT:Audience"],
                 claims: authClaims,
                 expires: DateTime.Now.AddDays(7),
               signingCredentials: signingCredentials);


            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);


            if (freshToken != null)
            {
                AuthResultResponse RefreshTokenResponse =
                    new AuthResultResponse()
                    {
                        Token = jwtToken,
                        ExpiresAt = token.ValidTo,
                        RefreshToken = freshToken.Token

                    };
                return RefreshTokenResponse;
            }

            FreshToken refreshTokenResp =
               new FreshToken()
               {
                   JwtId = token.Id,
                   IsRevoked = false,
                   UserId = user.Id,
                   DateExpire = DateTime.UtcNow.AddMonths(6),
                   DateAdded = DateTime.UtcNow,
                   Token =
                       Guid.NewGuid().ToString() +
                       "-" +
                       Guid.NewGuid().ToString()
               };


            await context.RefreshTokens.AddAsync(refreshTokenResp);
            await context.SaveChangesAsync();

            var response =
                new AuthResultResponse()
                {
                    Token = jwtToken,
                    RefreshToken = refreshTokenResp.Token,
                    ExpiresAt = token.ValidTo
                };
            return response;


        }
    }
}
