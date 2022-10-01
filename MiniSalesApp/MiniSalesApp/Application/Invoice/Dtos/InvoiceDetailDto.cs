using AutoMapper;
using CSharpFunctionalExtensions;
using MiniSalesApp.Common.Mapping;
using MiniSalesApp.Logic.MaterialAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Invoice.Dtos
{
    public class InvoiceDetailDto : IMapFrom<Logic.InvoiceAgreget.InvoiceDetail>
    {
        public InvoiceDetailDto()
        {

        }
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
        public decimal SellPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Logic.InvoiceAgreget.InvoiceDetail, InvoiceDetailDto>();
        }
    }
}
