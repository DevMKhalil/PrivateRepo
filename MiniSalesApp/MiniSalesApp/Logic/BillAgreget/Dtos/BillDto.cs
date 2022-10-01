using AutoMapper;
using CSharpFunctionalExtensions;
using MiniSalesApp.Common.Mapping;
using MiniSalesApp.Logic.SupplierAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BillAgreget.Dtos
{
    public class BillDto
    {
        public int BillId { get; set; }
        public int Serial { get; set; }
        public Maybe<Supplier> MaybeSupplier { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public string Discription { get; set; }
        public DateTime Date { get; set; }
        public List<BillDetailDto> BillDetailList { get; set; }
    }
}
