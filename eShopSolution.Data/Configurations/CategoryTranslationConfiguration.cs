using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");
            builder.HasKey(cT => cT.Id);
            builder.Property(cT => cT.Id).UseIdentityColumn();

            builder.Property(cT => cT.Name).IsRequired().HasMaxLength(200);
            builder.Property(cT => cT.SeoAlias).IsRequired().HasMaxLength(200);

            builder.Property(cT => cT.SeoDescription).HasMaxLength(200);
            builder.Property(cT => cT.SeoTitle).HasMaxLength(200);
            builder.Property(cT => cT.LanguageId).HasMaxLength(5).IsUnicode(false).IsRequired();

            builder.HasOne(cT => cT.Category).WithMany(c => c.CategoryTranslations).HasForeignKey(cT => cT.CategoryId);
            builder.HasOne(cT => cT.Language).WithMany(c => c.CategoryTranslations).HasForeignKey(cT => cT.LanguageId);
        }
    }
}
