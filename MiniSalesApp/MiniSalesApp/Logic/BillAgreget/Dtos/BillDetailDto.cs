using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.MaterialAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BillAgreget.Dtos
{
    public class BillDetailDto
    {
        public int BillDetailId { get; set; }
        public int BillId { get; set; }
        public Maybe<Material> maybeMaterial { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
