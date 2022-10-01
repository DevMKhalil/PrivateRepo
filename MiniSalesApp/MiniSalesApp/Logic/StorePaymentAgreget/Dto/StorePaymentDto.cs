using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.StoreDailyAgreget;
using MiniSalesApp.Logic.SupplierAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.StorePaymentAgreget.Dto
{
    public class StorePaymentDto
    {
        public int StorePaymentId { get; set; }
        public int Serial { get; set; }
        public bool IsSupplierPayment { get; set; }
        public Maybe<Supplier> MaybeSupplier { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Maybe<StoreDaily> MaybeStoreDaily { get; set; }
        public DateTime Date { get; set; }
    }
}
