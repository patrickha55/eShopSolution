using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.Property(l => l.Name).IsRequired().HasMaxLength(20);
        }
    }
}
