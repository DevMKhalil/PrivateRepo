using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.BankAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    public class BankConfigration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Bank");
            builder.Property(p => p.BankId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(200);
            builder.HasMany(p => p.BankRecivementList).WithOne().HasForeignKey(x => x.BankId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.BankPaymentList).WithOne().HasForeignKey(x => x.BankId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
