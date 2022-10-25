using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helper;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.TypeConfigure
{
    public class
    PhotoEntitySchemaDefinition
    : IEntityTypeConfiguration<PhotoEntity>
    {
        public void Configure(EntityTypeBuilder<PhotoEntity> builder)
        {
            builder.ToTable("Photo", "dbo");
            builder.HasKey(x => x.PhotoId);
            builder
                .Property(x => x.PhotoId)
                .IsRequired()
                .HasColumnType(TSqlTypes.Int)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.ApplicationUserId)
                .IsRequired()
                .HasColumnType(TSqlTypes.NVarChar + "450)");
            builder
                .Property(x => x.PublicId)
                .IsRequired()
                .HasColumnType(TSqlTypes.NVarChar + "50)");

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasColumnType(TSqlTypes.NVarChar + "30)");

            builder
                .Property(x => x.ImageUrl)
                .IsRequired()
                .HasColumnType(TSqlTypes.NVarChar + "250)");
            builder
                .Property(x => x.PublishDate)
                .IsRequired()
                .HasColumnType(TSqlTypes.DateTime)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(x => x.UpdateDate)
                .IsRequired()
                .HasColumnType(TSqlTypes.DateTime)
                .HasDefaultValueSql("GETDATE()");
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Photos)
                .HasForeignKey(x => x.ApplicationUserId);
        }
    }
}
