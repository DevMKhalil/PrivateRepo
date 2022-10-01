using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.StoreRecivementAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    public class StoreRecivementConfigration : IEntityTypeConfiguration<StoreRecivement>
    {
        public void Configure(EntityTypeBuilder<StoreRecivement> builder)
        {
            builder.ToTable("StoreRecivement");
            builder.Property(p => p.StoreRecivementId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
