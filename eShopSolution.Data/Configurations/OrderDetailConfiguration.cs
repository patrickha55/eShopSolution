using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(oD => new { oD.OrderId, oD.ProductId });

            builder.HasOne(oD => oD.Order).WithMany(p => p.OrderDetails).HasForeignKey(oD => oD.OrderId);
            builder.HasOne(oD => oD.Product).WithMany(p => p.OrderDetails).HasForeignKey(oD => oD.ProductId);
        }
    }
}
