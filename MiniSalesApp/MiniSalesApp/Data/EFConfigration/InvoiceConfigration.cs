using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.InvoiceAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    public class InvoiceConfigration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice");
            builder.Property(p => p.InvoiceId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Discription).HasMaxLength(500);
            builder.HasMany(p => p.InvoiceDetailList).WithOne().HasForeignKey(x => x.InvoiceId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
