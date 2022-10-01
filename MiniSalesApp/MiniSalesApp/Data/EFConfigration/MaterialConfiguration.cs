using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.MaterialAgreget;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniSalesApp.Data.EFConfigration
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Material");
            builder.Property(p => p.MaterialId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(200);
        }
    }
}
