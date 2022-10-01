using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.InvoiceAgreget.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.InvoiceAgreget
{
    public class Invoice
    {
        private Invoice()
        {
            InvoiceDetailList = new List<InvoiceDetail>();
        }

        public int InvoiceId { get; internal set; }
        public int Serial { get; internal set; }
        public int CustomerId { get; internal set; }
        public decimal Total { get; internal set; }
        public decimal Discount { get; internal set; }
        public decimal TotalAfterDiscount { get; internal set; }
        public string Discription { get; internal set; }
        public DateTime Date {get; internal set; }
        public List<InvoiceDetail> InvoiceDetailList { get; internal set; }

        private static Result ValidateInvoice(InvoiceDto invoice)
        {
            if (invoice.maybeCustomer.HasNoValue)
                return Result.Failure(Messages.CustomerNotFound);

            if (invoice.Discount < default(decimal))
                return Result.Failure(Messages.DiscountCanNotBeNegative);

            return Result.Success();
        }

        public static Result<Invoice> CreateInvoice(InvoiceDto invoiceDto,int maxSerial)
        {
            var validateResult = ValidateInvoice(invoiceDto);

            if (validateResult.IsFailure)
                return Result.Failure<Invoice>(validateResult.Error);

            List<InvoiceDetail> detailList = new List<InvoiceDetail>();
            foreach (var detailDto in invoiceDto.invoiceDetailList)
            {
                var detailResult = InvoiceDetail.CreateInvoiceDetail(detailDto);
                if (detailResult.IsFailure)
                {
                    return Result.Failure<Invoice>(detailResult.Error);
                }
                detailList.Add(detailResult.Value);
            }

            var total = detailList.Sum(x => x.Quantity * x.SellPrice);
            var totalAfterDiscount = total - invoiceDto.Discount;

            Invoice invoice = new Invoice
            {
                Serial = maxSerial + 1,
                CustomerId = invoiceDto.maybeCustomer.Value.CustomerId,
                Total = total,
                Discount = invoiceDto.Discount,
                TotalAfterDiscount = totalAfterDiscount,
                Date = invoiceDto.Date,
                Discription = invoiceDto.Discription,
                InvoiceDetailList = detailList
            };

            return Result.Success(invoice);
        }

        public Result<Invoice> UpdateInvoice(InvoiceDto invoiceDto)
        {
            var validateResult = ValidateInvoice(invoiceDto);

            if (validateResult.IsFailure)
                return Result.Failure<Invoice>(validateResult.Error);

            var deletedDetails = InvoiceDetailList
                .FindAll(detail => !invoiceDto.invoiceDetailList.Select(x => x.InvoiceDetailId).Contains(detail.InvoiceDetailId))
                .Select(y => y.InvoiceDetailId);

            InvoiceDetailList.RemoveAll(x => deletedDetails.Contains(x.InvoiceDetailId));

            foreach (var detailDto in invoiceDto.invoiceDetailList)
            {
                if (detailDto.InvoiceDetailId == default)
                {
                    var createResult =
                        InvoiceDetail.CreateInvoiceDetail(detailDto);
                    if (createResult.IsFailure)
                        return Result.Failure<Invoice>(createResult.Error);

                    InvoiceDetailList.Add(createResult.Value);
                }
                else
                {
                    var oldDetail = InvoiceDetailList.FirstOrDefault(x => x.InvoiceDetailId == detailDto.InvoiceDetailId);
                    var updateDetailResault = oldDetail.UpdateInvoiceDetail(detailDto);
                    if (updateDetailResault.IsFailure)
                        return Result.Failure<Invoice>(updateDetailResault.Error);
                }
            }

            Discount = invoiceDto.Discount;
            Total = InvoiceDetailList.Sum(x => x.Quantity * x.SellPrice);
            TotalAfterDiscount = Total - Discount;
            CustomerId = invoiceDto.maybeCustomer.Value.CustomerId;
            Date = invoiceDto.Date;
            Discription = invoiceDto.Discription;

            return Result.Success(this);
        }
    }
}
