using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.SupplierAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    class SupplierConfigration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");
            builder.Property(p => p.SupplierId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(200);
            builder.Property(p => p.Phone).HasMaxLength(50);
            builder.Property(p => p.Address).HasMaxLength(500);
        }
    }
}
