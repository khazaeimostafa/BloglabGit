using API.Helper;
using Microsoft.AspNetCore.Identity;

namespace API.Seed
{
    public static class AppInitializer
    {
        public static async Task SeedRolesToDb(this IApplicationBuilder app)
        {
            using (var ScopeService = app.ApplicationServices.CreateAsyncScope()
            )
            {

                //Snippet-s1
                var roleManager =
                    ScopeService
                        .ServiceProvider
                        .GetRequiredService<RoleManager<IdentityRole>>();

                        if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.Student))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Student));
                }
            }
        }
    }
}
