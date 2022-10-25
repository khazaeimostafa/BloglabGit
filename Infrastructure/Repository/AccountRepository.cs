using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Dapper; 
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using  Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAsync(ApplicationUserIdentity user, CancellationToken cancellationToken);
        Task<ApplicationUserIdentity> GetByUsernameAsync(string normalizedUsername, CancellationToken cancellationToken);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration config;

        public AccountRepository(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<IdentityResult>
        CreateAsync(
            ApplicationUserIdentity user,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Username", typeof(string));

            dataTable.Columns.Add("NormalizedUsername", typeof(string));

            dataTable.Columns.Add("Email", typeof(string));

            dataTable.Columns.Add("NormalizedEmail", typeof(string));

            dataTable.Columns.Add("Fulname", typeof(string));

            dataTable.Columns.Add("PasswordHash", typeof(string));

            dataTable
                .Rows
                .Add(user.Username,
                user.NormalizedUsername,
                user.Email,
                user.NormalizedEmail,
                user.FulName,
                user.PasswordHash);

            using (
                var connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync(cancellationToken);
                await connection
                    .ExecuteAsync("Account_Insert",
                    new
                    {
                        Account =
                            dataTable.AsTableValuedParameter("dbo.AccountType")
                    },
                    commandType: CommandType.StoredProcedure);

                return IdentityResult.Success;
            }
        }

        public async Task<ApplicationUserIdentity>
        GetByUsernameAsync(
            string normalizedUsername,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            ApplicationUserIdentity applicationUser;

            using (
                SqlConnection connection =
                    new SqlConnection(config
                            .GetConnectionString("DefaultConnection"))
            )
            {
                await connection.OpenAsync(cancellationToken);

                applicationUser =
                    await connection
                        .QuerySingleOrDefaultAsync
                        <ApplicationUserIdentity>("Account_GetByUsername",
                        new { NormalizedUsername = normalizedUsername },
                        commandType: CommandType.StoredProcedure);
            }

            return applicationUser;
        }
    }
}
