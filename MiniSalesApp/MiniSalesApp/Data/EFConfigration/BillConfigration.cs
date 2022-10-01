using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.BillAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    public class BillConfigration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill");
            builder.Property(p => p.BillId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Discription).HasMaxLength(500);
            builder.HasMany(p => p.BillDetailList).WithOne().HasForeignKey(x => x.BillId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
