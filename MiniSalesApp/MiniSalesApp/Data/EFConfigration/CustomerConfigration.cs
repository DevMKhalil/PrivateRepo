using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniSalesApp.Logic.CustomerAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Data.EFConfigration
{
    class CustomerConfigration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.Property(p => p.CustomerId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(200);
            builder.Property(p => p.Phone).HasMaxLength(50);
            builder.Property(p => p.Address).HasMaxLength(500);
        }
    }
}
