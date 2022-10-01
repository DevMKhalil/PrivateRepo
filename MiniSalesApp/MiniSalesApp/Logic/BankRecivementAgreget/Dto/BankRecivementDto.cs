using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.BankAgreget;
using MiniSalesApp.Logic.CustomerAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BankRecivementAgreget.Dto
{
    public class BankRecivementDto
    {
        public int BankRecivementId { get; set; }
        public int Serial { get; set; }
        public bool IsCustomerRecivement { get; set; }
        public Maybe<Customer> MaybeCustomer { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Maybe<Bank> MaybeBank { get; set; }
        public DateTime Date { get; set; }
    }
}
