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
    BlogCommentEntitySchemaDefinition
    : IEntityTypeConfiguration<BlogCommentEntity>
    {
        public void Configure(EntityTypeBuilder<BlogCommentEntity> builder)
        {
            builder.ToTable("BlogComment", "dbo").HasKey(x => x.BlogCommentId);
            builder
                .Property(x => x.BlogCommentId)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType(TSqlTypes.Int);
            builder
                .Property(x => x.ParentBlogCommentId)
                .HasColumnType(TSqlTypes.Int);
            builder 
                .Property(x => x.BlogId)
                .IsRequired()
                .HasColumnType(TSqlTypes.Int);

            builder
                .Property(x => x.ApplicationUserId)
                .IsRequired()
                .HasColumnType(TSqlTypes.NVarChar + "450)");
            builder
                .Property(x => x.Content)
                .IsRequired()
                .HasColumnType(TSqlTypes.NVarChar + "300)");
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
                .Property(x => x.ActiveInd)
                .IsRequired()
                .HasColumnType(TSqlTypes.Bit)
                .HasDefaultValueSql("CONVERT(BIT ,1)");

            builder
                .HasOne(x => x.Blog)
                .WithMany(x => x.BlogComments)
                .HasForeignKey(x => x.BlogId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(x => x.user)
                .WithMany(x => x.BlogComments)
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
