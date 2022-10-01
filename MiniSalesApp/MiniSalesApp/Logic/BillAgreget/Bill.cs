using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.BillAgreget.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BillAgreget
{
    public class Bill
    {
        private Bill()
        {
            BillDetailList = new List<BillDetail>();
        }

        public int BillId { get; internal set; }
        public int Serial { get; internal set; }
        public int SupplierId { get; internal set; }
        public decimal Total { get; internal set; }
        public decimal Discount { get; internal set; }
        public decimal TotalAfterDiscount { get; internal set; }
        public string Discription { get; internal set; }
        public DateTime Date {get; internal set; }
        public List<BillDetail> BillDetailList { get; internal set; }

        private static Result ValidateBill(BillDto bill)
        {
            if (bill.MaybeSupplier.HasNoValue)
                return Result.Failure(Messages.SupplierNotFound);

            if (bill.Discount < default(decimal))
                return Result.Failure(Messages.DiscountCanNotBeNegative);

            return Result.Success();
        }

        public static Result<Bill> CreateBill(BillDto billDto,int maxSerial)
        {
            var validateResult = ValidateBill(billDto);

            if (validateResult.IsFailure)
                return Result.Failure<Bill>(validateResult.Error);

            List<BillDetail> detailList = new List<BillDetail>();
            foreach (var detailDto in billDto.BillDetailList)
            {
                var detailResult = BillDetail.CreateBillDetail(detailDto);
                if (detailResult.IsFailure)
                {
                    return Result.Failure<Bill>(detailResult.Error);
                }
                detailList.Add(detailResult.Value);
            }

            var total = detailList.Sum(x => x.Quantity * x.PurchasePrice);
            var totalAfterDiscount = total - billDto.Discount;

            Bill invoice = new Bill
            {
                Serial = maxSerial + 1,
                SupplierId = billDto.MaybeSupplier.Value.SupplierId,
                Total = total,
                Discount = billDto.Discount,
                TotalAfterDiscount = totalAfterDiscount,
                Date = billDto.Date,
                Discription = billDto.Discription,
                BillDetailList = detailList
            };

            return Result.Success(invoice);
        }

        public Result<Bill> UpdateBill(BillDto billDto)
        {
            var validateResult = ValidateBill(billDto);

            if (validateResult.IsFailure)
                return Result.Failure<Bill>(validateResult.Error);

            var deletedDetails = BillDetailList
                .FindAll(detail => !billDto.BillDetailList.Select(x => x.BillDetailId).Contains(detail.BillDetailId))
                .Select(y => y.BillDetailId);

            BillDetailList.RemoveAll(x => deletedDetails.Contains(x.BillDetailId));

            foreach (var detailDto in billDto.BillDetailList)
            {
                if (detailDto.BillDetailId == default)
                {
                    var createResult =
                        BillDetail.CreateBillDetail(detailDto);
                    if (createResult.IsFailure)
                        return Result.Failure<Bill>(createResult.Error);

                    BillDetailList.Add(createResult.Value);
                }
                else
                {
                    var oldDetail = BillDetailList.FirstOrDefault(x => x.BillDetailId == detailDto.BillDetailId);
                    var updateDetailResault = oldDetail.UpdateBillDetail(detailDto);
                    if (updateDetailResault.IsFailure)
                        return Result.Failure<Bill>(updateDetailResault.Error);
                }
            }

            Discount = billDto.Discount;
            Total = BillDetailList.Sum(x => x.Quantity * x.PurchasePrice);
            TotalAfterDiscount = Total - Discount;
            SupplierId = billDto.MaybeSupplier.Value.SupplierId;
            Date = billDto.Date;
            Discription = billDto.Discription;

            return Result.Success(this);
        }
    }
}
