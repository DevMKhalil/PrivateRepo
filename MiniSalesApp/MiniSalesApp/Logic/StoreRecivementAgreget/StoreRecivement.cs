using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.StoreRecivementAgreget.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.StoreRecivementAgreget
{
    public class StoreRecivement
    {
        private StoreRecivement()
        {

        }

        public int StoreRecivementId { get; internal set; }
        public int Serial { get; internal set; }
        public bool IsCustomerRecivement { get; internal set; }
        public int? CustomerId { get; internal set; }
        public string Description { get; internal set; }
        public decimal Amount { get; internal set; }
        public int StoreDailyId { get; internal set; }
        public DateTime Date { get; internal set; }

        public static Result ValidateStoreRecivement(StoreRecivementDto storeRecivementDto)
        {
            if (storeRecivementDto.MaybeStoreDaily.HasNoValue)
                return Result.Failure(Messages.StoreDailyNotFound);

            if (storeRecivementDto.IsCustomerRecivement && storeRecivementDto.MaybeCustomer.HasNoValue)
                return Result.Failure(Messages.SellectCustomer);

            if (storeRecivementDto.Amount <= 0)
                return Result.Failure(Messages.AmountCantBeNegative);

            if (storeRecivementDto.Date < storeRecivementDto.MaybeStoreDaily.Value.StartDate)
                return Result.Failure(Messages.TransactionDateCantBeLessThanStartDate);

            return Result.Success();
        }

        public static Result<StoreRecivement> CreateStoreRecivement(
            StoreRecivementDto storeRecivementDto,
            int maxSerial)
        {
            var res = ValidateStoreRecivement(storeRecivementDto);

            if (res.IsFailure)
                return Result.Failure<StoreRecivement>(res.Error);

            var storeRecivement = new StoreRecivement()
            {
                Serial = maxSerial + 1,
                IsCustomerRecivement = storeRecivementDto.IsCustomerRecivement,
                CustomerId = storeRecivementDto.IsCustomerRecivement ? storeRecivementDto.MaybeCustomer.Value.CustomerId : null,
                Amount = storeRecivementDto.Amount,
                Date = storeRecivementDto.Date,
                Description = storeRecivementDto.Description,
                StoreDailyId = storeRecivementDto.MaybeStoreDaily.Value.StoreDailyId
            };

            return Result.Success(storeRecivement);
        }

        public Result<StoreRecivement> UpdateStoreRecivement(
            StoreRecivementDto storeRecivementDto)
        {
            var res = ValidateStoreRecivement(storeRecivementDto);

            if (res.IsFailure)
                return Result.Failure<StoreRecivement>(res.Error);

            IsCustomerRecivement = storeRecivementDto.IsCustomerRecivement;
            CustomerId = storeRecivementDto.IsCustomerRecivement ? storeRecivementDto.MaybeCustomer.Value.CustomerId : null;
            Amount = storeRecivementDto.Amount;
            Date = storeRecivementDto.Date;
            Description = storeRecivementDto.Description;

            return Result.Success(this);
        }
    }
}
