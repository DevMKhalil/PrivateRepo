using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.BankPaymentAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    public class BankPaymentConfigration : IEntityTypeConfiguration<BankPayment>
    {
        public void Configure(EntityTypeBuilder<BankPayment> builder)
        {
            builder.ToTable("BankPayment");
            builder.Property(p => p.BankPaymentId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
