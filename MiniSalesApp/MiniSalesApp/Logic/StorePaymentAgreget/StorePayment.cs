using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.StorePaymentAgreget.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.StorePaymentAgreget
{
    public class StorePayment
    {
        private StorePayment()
        {

        }

        public int StorePaymentId { get; internal set; }
        public int Serial { get; internal set; }
        public bool IsSupplierPayment { get; internal set; }
        public int? SupplierId { get; internal set; }
        public string Description { get; internal set; }
        public decimal Amount { get; internal set; }
        public int StoreDailyId { get; internal set; }
        public DateTime Date { get; internal set; }

        public static Result ValidateStorePayment(StorePaymentDto storePaymentDto)
        {
            if (storePaymentDto.MaybeStoreDaily.HasNoValue)
                return Result.Failure(Messages.StoreDailyNotFound);

            if (storePaymentDto.IsSupplierPayment && storePaymentDto.MaybeSupplier.HasNoValue)
                return Result.Failure(Messages.SelectSupplier);

            if (storePaymentDto.Amount <= 0)
                return Result.Failure(Messages.AmountCantBeNegative);

            if (storePaymentDto.Date < storePaymentDto.MaybeStoreDaily.Value.StartDate)
                return Result.Failure(Messages.DateMustBeGreaterThanLastDailyDate);

            return Result.Success();
        }

        public static Result<StorePayment> CreateStorePayment(
            StorePaymentDto storePaymentDto,
            int maxSerial)
        {
            var res = ValidateStorePayment(storePaymentDto);

            if (res.IsFailure)
                return Result.Failure<StorePayment>(res.Error);

            var storePayment = new StorePayment()
            {
                Serial = maxSerial + 1,
                IsSupplierPayment = storePaymentDto.IsSupplierPayment,
                SupplierId = storePaymentDto.IsSupplierPayment ? storePaymentDto.MaybeSupplier.Value.SupplierId : null,
                Amount = storePaymentDto.Amount,
                Date = storePaymentDto.Date,
                Description = storePaymentDto.Description,
                StoreDailyId = storePaymentDto.MaybeStoreDaily.Value.StoreDailyId
            };

            return Result.Success(storePayment);
        }

        public Result<StorePayment> UpdateStorePayment(StorePaymentDto storePaymentDto)
        {
            var res = ValidateStorePayment(storePaymentDto);

            if (res.IsFailure)
                return Result.Failure<StorePayment>(res.Error);

            IsSupplierPayment = storePaymentDto.IsSupplierPayment;
            SupplierId = storePaymentDto.IsSupplierPayment ? storePaymentDto.MaybeSupplier.Value.SupplierId : null;
            Amount = storePaymentDto.Amount;
            Date = storePaymentDto.Date;
            Description = storePaymentDto.Description;

            return Result.Success(this);
        }
    }
}
