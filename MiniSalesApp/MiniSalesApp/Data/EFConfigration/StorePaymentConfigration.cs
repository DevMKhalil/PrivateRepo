using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.StorePaymentAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    public class StorePaymentConfigration : IEntityTypeConfiguration<StorePayment>
    {
        public void Configure(EntityTypeBuilder<StorePayment> builder)
        {
            builder.ToTable("StorePayment");
            builder.Property(p => p.StorePaymentId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
