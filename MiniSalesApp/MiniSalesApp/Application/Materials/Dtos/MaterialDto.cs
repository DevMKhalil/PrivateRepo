using System;
using System.Collections.Generic;
using System.Text;

namespace MiniSalesApp.Application.Materials.Dtos
{
    public class MaterialDto
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public decimal SellPrice { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
