using Core.Helper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TypeConfigure
{
    public class BlogEntitySchemaDefinition : IEntityTypeConfiguration<BlogEntity>
    {
        public void Configure(EntityTypeBuilder<BlogEntity> builder)
        {
            builder.HasKey(x => x.BlogId);
            builder.Property(x => x.BlogId).IsRequired().HasColumnType(TSqlTypes.Int).ValueGeneratedOnAdd();

            builder.Property(x => x.ApplicationUserId).IsRequired().HasColumnType(TSqlTypes.NVarChar+ "450)");

            builder.Property(x => x.Title).IsRequired().HasColumnType(TSqlTypes.NVarChar+"50)");

            builder.Property(x => x.PhotoId).HasColumnType(TSqlTypes.Int);

            builder.Property(x => x.PublishDate).IsRequired().HasColumnType(TSqlTypes.DateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdateDate).IsRequired().HasColumnType(TSqlTypes.DateTime).HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.ActiveInd).IsRequired().HasColumnType(TSqlTypes.Bit).HasDefaultValueSql("CONVERT(BIT ,1)");

            builder.ToTable("Blog", "dbo");
       

            builder.HasOne(x => x.user).WithMany(x => x.Blogs).HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasOne(x => x.photo).WithOne
                (x => x.Blog).HasForeignKey<BlogEntity>(x => x.PhotoId).OnDelete(DeleteBehavior.NoAction);

            
        }
    }
}
