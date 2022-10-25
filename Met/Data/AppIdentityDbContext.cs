using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.TypeConfigure;
using Met.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Met.Data
{
    //public class AppIdentityDbContext : IdentityDbContext<AppUser>
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(
            DbContextOptions<AppIdentityDbContext> options
        ) :
            base(options)
        {
        }

        public DbSet<FreshToken> RefreshTokens { get; set; }

        public DbSet<PhotoEntity> Photos { get; set; }

        public DbSet<BlogEntity> Blogs { get; set; }

        public DbSet<BlogCommentEntity> BlogComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PhotoEntitySchemaDefinition());
            builder.ApplyConfiguration(new BlogEntitySchemaDefinition());
            builder.ApplyConfiguration(new BlogCommentEntitySchemaDefinition());
            builder.ApplyConfiguration(new AppUserEntitySchemaDefinition());
            base.OnModelCreating(builder);

            foreach (var
                item
                in
                builder
                    .Model
                    .GetEntityTypes()
                    .SelectMany(k => k.GetForeignKeys())
            )
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
