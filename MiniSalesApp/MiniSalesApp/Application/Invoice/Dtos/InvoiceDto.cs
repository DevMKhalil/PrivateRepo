using AutoMapper;
using CSharpFunctionalExtensions;
using MiniSalesApp.Common.Mapping;
using MiniSalesApp.Logic.CustomerAgreget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Invoice.Dtos
{
    public class InvoiceDto : IMapFrom<Logic.InvoiceAgreget.Invoice>
    {
        public InvoiceDto()
        {
            InvoiceDetailList = new List<InvoiceDetailDto>();
        }
        public int InvoiceId { get; set; }
        public int Serial { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
        public string Discription { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceDetailDto> InvoiceDetailList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Logic.InvoiceAgreget.Invoice, InvoiceDto>()
                .ForMember(x => x.InvoiceDetailList, opt => opt.MapFrom(src => src.InvoiceDetailList));
        }
    }
}
