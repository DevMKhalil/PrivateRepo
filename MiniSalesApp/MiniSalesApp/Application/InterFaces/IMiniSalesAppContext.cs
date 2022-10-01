using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Logic.CustomerAgreget;
using MiniSalesApp.Logic.MaterialAgreget;
using MiniSalesApp.Logic.SupplierAgreget;
using MiniSalesApp.Logic.InvoiceAgreget;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiniSalesApp.Logic.StoreDailyAgreget;
using MiniSalesApp.Logic.BankAgreget;
using MiniSalesApp.Logic.BillAgreget;

namespace MiniSalesApp.Application.InterFaces
{
    public interface IMiniSalesAppContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MiniSalesApp.Logic.InvoiceAgreget.Invoice> Invoices { get; set; }
        public DbSet<MiniSalesApp.Logic.StoreDailyAgreget.StoreDaily> StoreDailies { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<MiniSalesApp.Logic.BillAgreget.Bill> Bills { get; set; }

        Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default(CancellationToken));
    }
}
