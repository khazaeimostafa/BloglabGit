using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace API.Authorizing
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection
        AddAuthorize(
            this IServiceCollection services,
            ConfigurationManager configuration
        )
        {
            services
                .AddSingleton
                <IAuthorizationHandler,
                    canEditOnlyOtherAdminRolesAndClaimsHandler
                >();

            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

            // Codes
            {
                // .........................
                // services
                //     .AddAuthorization(options =>
                //     {
                //         options
                //             .AddPolicy("Admin",
                //             policy => policy.RequireClaim("admin", "true"));
                //     });
                // ...........................................
                // services
                //     .AddAuthorization(options =>
                //     {
                //         options
                //             .AddPolicy("EditAdmin",
                //             policy =>
                //                 policy
                //                     .AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
                //     });
                // ..............................
                // services
                //     .AddAuthorization(options =>
                //     {
                //         options
                //             .AddPolicy("Sample",
                //             policy =>
                //                 policy
                //                     .AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
                //         options.FallbackPolicy =
                //             new AuthorizationPolicy(new IAuthorizationRequirement[] {
                //                     new NameAuthorizationRequirement("bob")
                //                 },
                //                 Enumerable.Empty<String>());
                //     });
                // .......................................
                // services
                //     .AddAuthorization(options =>
                //     {
                //         options
                //             .AddPolicy("SuperAssertion",
                //             policy =>
                //                 policy
                //                     .RequireAssertion(x =>
                //                         x.User.IsInRole("") &&
                //                         x
                //                             .User
                //                             .HasClaim(c =>
                //                                 c.Type == "" && c.Value == "") ||
                //                         x.User.IsInRole("Super")));
                //         options.InvokeHandlersAfterFailure = false;
                //     });
            }

            return services;
        }
    }
}
