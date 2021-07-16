using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseIdentityColumn();

            builder.Property(p => p.ImagePath).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Caption).HasMaxLength(200).IsRequired(false);

            builder.HasOne(pI => pI.Product).WithMany(p => p.ProductImages).HasForeignKey(p => p.ProductId);
        }
    }
}
