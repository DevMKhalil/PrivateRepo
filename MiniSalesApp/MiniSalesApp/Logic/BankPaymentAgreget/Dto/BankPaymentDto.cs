using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.BankAgreget;
using MiniSalesApp.Logic.SupplierAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BankPaymentAgreget.Dto
{
    public class BankPaymentDto
    {
        public int BankPaymentId { get; set; }
        public int Serial { get; set; }
        public bool IsSupplierPayment { get; set; }
        public Maybe<Supplier> MaybeSupplier { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Maybe<Bank> MaybeBank { get; set; }
        public DateTime Date { get; set; }
    }
}
