using CSharpFunctionalExtensions;
using MiniSalesApp.Application.StoreDaily.Dtos;
using MiniSalesApp.Logic.StorePaymentAgreget;
using MiniSalesApp.Logic.StoreRecivementAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.StoreDailyAgreget
{
    public class StoreDaily
    {
        private StoreDaily()
        {
            StoreRecivementList = new List<StoreRecivement>();
            StorePaymentList = new List<StorePayment>();
        }

        public int StoreDailyId { get; set; }
        public int Serial { get; set; }
        public decimal StartAmount { get; set; }
        public decimal EndAmount { get; set; }
        public DateTime StartDate { get; set; }
        public List<StoreRecivement> StoreRecivementList { get; set; }
        public List<StorePayment> StorePaymentList { get; set; }

        public static Result ValidateStoreDaily(StoreDailyDto storeDailyDto,DateTime? lastTransactionInLastDailyDate)
        {
            if (storeDailyDto.StartAmount < 0)
                return Result.Failure(Messages.StartAmountCantBeNegative);

            if (storeDailyDto.EndAmount < 0)
                return Result.Failure(Messages.EndAmountCantBeNegative);

            if ((lastTransactionInLastDailyDate != null) && (storeDailyDto.StartDate <= lastTransactionInLastDailyDate.Value.Date))
                return Result.Failure(Messages.DateMustBeGreaterThanLastDailyDate);

            return Result.Success();
        }

        public static Result<StoreDaily> CreateStoreDaily(
            StoreDailyDto storeDailyDto,
            DateTime? lastTransactionInLastDailyDate,
            bool isTheFirst,
            decimal lastDailyEndAmount,
            int maxSerial)
        {
            var res = ValidateStoreDaily(storeDailyDto, lastTransactionInLastDailyDate);

            if (res.IsFailure)
                return Result.Failure<StoreDaily>(res.Error);

            var storeDaily = new StoreDaily()
            {
                Serial = maxSerial + 1,
                StartAmount = isTheFirst ? storeDailyDto.StartAmount : lastDailyEndAmount,
                EndAmount = isTheFirst ? storeDailyDto.StartAmount : lastDailyEndAmount,
                StartDate = storeDailyDto.StartDate
            };

            return Result.Success(storeDaily);
        }

        public Result<StoreDaily> UpdateStoreDaily(StoreDailyDto storeDailyDto, DateTime? lastTransactionInLastDailyDate, bool isTheFirst,
            decimal lastDailyEndAmount)
        {
            var res = ValidateStoreDaily(storeDailyDto, lastTransactionInLastDailyDate);

            if (res.IsFailure)
                return Result.Failure<StoreDaily>(res.Error);

            StartAmount = isTheFirst ? storeDailyDto.StartAmount : lastDailyEndAmount;
            StartDate = storeDailyDto.StartDate;

            return Result.Success(this);
        }

        public Result VaildateForDelete()
        {
            if (this.StorePaymentList.Count > 0 || this.StoreRecivementList.Count > 0)
                return Result.Failure(Messages.TheDailyHasTransaction);

            return Result.Success();
        }

        public Result VaildateIsLastDaily(int lastDailyId)
        {
            if (lastDailyId != this.StoreDailyId)
                return Result.Failure(Messages.CanOnlyDeleteOrUpdateLastDaily);

            return Result.Success();
        }

        public Result VaildateForUpdate(int lastDailyId)
        {
            if (lastDailyId != this.StoreDailyId)
                return Result.Failure(Messages.CanOnlyDeleteOrUpdateLastDaily);

            if (StorePaymentList.Count > 0 || StoreRecivementList.Count > 0)
                return Result.Failure(Messages.TheDailyHasTransaction);

            return Result.Success();
        }

        public void UpdateEndAmount()
        {
            this.EndAmount =
                this.StartAmount +
                this.StoreRecivementList.Sum(x => x.Amount) -
                this.StorePaymentList.Sum(x => x.Amount);
        }
    }
}
