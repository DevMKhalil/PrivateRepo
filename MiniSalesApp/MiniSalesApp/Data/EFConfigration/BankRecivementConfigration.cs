using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.BankRecivementAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    public class BankRecivementConfigration : IEntityTypeConfiguration<BankRecivement>
    {
        public void Configure(EntityTypeBuilder<BankRecivement> builder)
        {
            builder.ToTable("BankRecivement");
            builder.Property(p => p.BankRecivementId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
