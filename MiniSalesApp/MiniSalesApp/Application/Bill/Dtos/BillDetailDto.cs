using AutoMapper;
using CSharpFunctionalExtensions;
using MiniSalesApp.Common.Mapping;
using MiniSalesApp.Logic.MaterialAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Bill.Dtos
{
    public class BillDetailDto : IMapFrom<Logic.BillAgreget.BillDetail>
    {
        public BillDetailDto()
        {

        }
        public int BillDetailId { get; set; }
        public int BillId { get; set; }
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Logic.BillAgreget.BillDetail, BillDetailDto>();
        }
    }
}
