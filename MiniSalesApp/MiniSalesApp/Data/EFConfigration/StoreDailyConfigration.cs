using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.StoreDailyAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    public class StoreDailyConfigration : IEntityTypeConfiguration<StoreDaily>
    {
        public void Configure(EntityTypeBuilder<StoreDaily> builder)
        {
            builder.ToTable("StoreDaily");
            builder.Property(p => p.StoreDailyId).UseIdentityColumn().IsRequired();
            builder.HasMany(p => p.StoreRecivementList).WithOne().HasForeignKey(x => x.StoreDailyId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.StorePaymentList).WithOne().HasForeignKey(x => x.StoreDailyId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
