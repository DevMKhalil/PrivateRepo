using AutoMapper;
using CSharpFunctionalExtensions;
using MiniSalesApp.Common.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Bill.Dtos
{
    public class BillDto : IMapFrom<Logic.BillAgreget.Bill>
    {
        public BillDto()
        {
            BillDetailList = new List<BillDetailDto>();
        }
        public int BillId { get; set; }
        public int Serial { get; set; }
        public int SupplierId { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public string Discription { get; set; }
        public DateTime Date { get; set; }
        public List<BillDetailDto> BillDetailList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Logic.BillAgreget.Bill, BillDto>()
                .ForMember(x => x.BillDetailList, opt => opt.MapFrom(src => src.BillDetailList));
        }
    }
}
