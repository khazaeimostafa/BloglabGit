using Core.Helper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.TypeConfigure
{
    public class AppUserEntitySchemaDefinition : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
             builder
                .Property(x => x.FirstName)
                
                .HasColumnType(TSqlTypes.NVarChar + "20)");
            builder
                .Property(x => x.LastName)
                 
                .HasColumnType(TSqlTypes.NVarChar + "20)");
        }
    }
}