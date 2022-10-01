using AutoMapper;
using CSharpFunctionalExtensions;
using MiniSalesApp.Common.Mapping;
using MiniSalesApp.Logic.CustomerAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.InvoiceAgreget.Dtos
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public int Serial { get; set; }
        public Maybe<Customer> maybeCustomer { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public string Discription { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceDetailDto> invoiceDetailList { get; set; }
    }
}
