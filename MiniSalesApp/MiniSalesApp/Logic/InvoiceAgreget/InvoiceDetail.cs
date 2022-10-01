using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.InvoiceAgreget.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.InvoiceAgreget
{
    public class InvoiceDetail
    {
        internal InvoiceDetail()
        {

        }

        public int InvoiceDetailId { get; internal set; }
        public int InvoiceId { get; internal set; }
        public int MaterialId { get; internal set; }
        public int Quantity { get; internal set; }
        public decimal SellPrice { get; internal set; }

        private static Result ValidateDetail(InvoiceDetailDto invoiceDetail)
        {
            if (invoiceDetail.maybeMaterial.HasNoValue)
                return Result.Failure(Messages.MaterialNotFound);

            if (invoiceDetail.Quantity <= default(int))
                return Result.Failure(Messages.QuantityCanNotBeNegative);

            if (invoiceDetail.SellPrice <= default(decimal))
                return Result.Failure(Messages.SellPriceCanNotBeNegative);

            return Result.Success();
        }

        internal static Result<InvoiceDetail> CreateInvoiceDetail(InvoiceDetailDto detailDto)
        {
            var validateResult = ValidateDetail(detailDto);

            if (validateResult.IsFailure)
                return Result.Failure<InvoiceDetail>(validateResult.Error);

            InvoiceDetail invoiceDetail = new InvoiceDetail
            {
                MaterialId = detailDto.maybeMaterial.Value.MaterialId,
                Quantity = detailDto.Quantity,
                SellPrice = detailDto.SellPrice
            };

            return Result.Success(invoiceDetail);
        }

        internal Result<InvoiceDetail> UpdateInvoiceDetail(InvoiceDetailDto detailDto)
        {
            var validateResult = ValidateDetail(detailDto);

            if (validateResult.IsFailure)
                return Result.Failure<InvoiceDetail>(validateResult.Error);

            MaterialId = detailDto.maybeMaterial.Value.MaterialId;
            Quantity = detailDto.Quantity;
            SellPrice = detailDto.SellPrice;

            return Result.Success(this);
        }
    }
}
