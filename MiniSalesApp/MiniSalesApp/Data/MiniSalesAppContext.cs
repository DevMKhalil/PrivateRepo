using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniSalesApp.Logic.BankAgreget;
using MiniSalesApp.Logic.BillAgreget;
using MiniSalesApp.Logic.CustomerAgreget;
using MiniSalesApp.Application.InterFaces;
using MiniSalesApp.Logic.InvoiceAgreget;
using MiniSalesApp.Logic.MaterialAgreget;
using MiniSalesApp.Logic.StoreDailyAgreget;
using MiniSalesApp.Logic.SupplierAgreget;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Data
{
    public class MiniSalesAppContext : DbContext, IMiniSalesAppContext
    {
        public DbSet<Material> Materials { get ; set ; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<StoreDaily> StoreDailies { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Bill> Bills { get; set; }

        public async Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (DbUpdateException dbExce)
            {
                return Result.Failure(dbExce.Message);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

        }

        public static readonly ILoggerFactory ConsolLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddFilter(DbLoggerCategory.Database.Transaction.Name, LogLevel.Debug);
            builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
        });



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(ConsolLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(Properties.Settings.Default.TempString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
