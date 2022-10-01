using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.MaterialAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.InvoiceAgreget.Dtos
{
    public class InvoiceDetailDto
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public Maybe<Material> maybeMaterial { get; set; }
        public int Quantity { get; set; }
        public decimal SellPrice { get; set; }
    }
}
