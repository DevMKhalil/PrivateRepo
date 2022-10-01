using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.CustomerAgreget;
using MiniSalesApp.Logic.StoreDailyAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.StoreRecivementAgreget.Dto
{
    public class StoreRecivementDto
    {
        public int StoreRecivementId { get; set; }
        public bool IsCustomerRecivement { get; set; }
        public Maybe<Customer> MaybeCustomer { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Maybe<StoreDaily> MaybeStoreDaily { get; set; }
        public DateTime Date { get; set; }
    }
}
