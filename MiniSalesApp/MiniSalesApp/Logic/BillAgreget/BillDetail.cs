using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.BillAgreget.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BillAgreget
{
    public class BillDetail
    {
        internal BillDetail()
        {

        }

        public int BillDetailId { get; internal set; }
        public int BillId { get; internal set; }
        public int MaterialId { get; internal set; }
        public int Quantity { get; internal set; }
        public decimal PurchasePrice { get; internal set; }

        private static Result ValidateDetail(BillDetailDto billDetail)
        {
            if (billDetail.maybeMaterial.HasNoValue)
                return Result.Failure(Messages.MaterialNotFound);

            if (billDetail.Quantity <= default(int))
                return Result.Failure(Messages.QuantityCanNotBeNegative);

            if (billDetail.PurchasePrice <= default(decimal))
                return Result.Failure(Messages.SellPriceCanNotBeNegative);

            return Result.Success();
        }

        internal static Result<BillDetail> CreateBillDetail(BillDetailDto detailDto)
        {
            var validateResult = ValidateDetail(detailDto);

            if (validateResult.IsFailure)
                return Result.Failure<BillDetail>(validateResult.Error);

            BillDetail invoiceDetail = new BillDetail
            {
                MaterialId = detailDto.maybeMaterial.Value.MaterialId,
                Quantity = detailDto.Quantity,
                PurchasePrice = detailDto.PurchasePrice
            };

            return Result.Success(invoiceDetail);
        }

        internal Result<BillDetail> UpdateBillDetail(BillDetailDto detailDto)
        {
            var validateResult = ValidateDetail(detailDto);

            if (validateResult.IsFailure)
                return Result.Failure<BillDetail>(validateResult.Error);

            MaterialId = detailDto.maybeMaterial.Value.MaterialId;
            Quantity = detailDto.Quantity;
            PurchasePrice = detailDto.PurchasePrice;

            return Result.Success(this);
        }
    }
}
