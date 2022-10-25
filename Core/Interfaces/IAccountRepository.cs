using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult>
        CreateAsync(
            ApplicationUserIdentity user, CancellationToken cancellationToken
        );

        Task<ApplicationUserIdentity>
        GetByUsernameAsync(
            string normalizedUsername, CancellationToken cancellationToken
        );
    }
}
