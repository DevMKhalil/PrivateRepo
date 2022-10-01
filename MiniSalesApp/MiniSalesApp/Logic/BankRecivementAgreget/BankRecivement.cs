using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.BankRecivementAgreget.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BankRecivementAgreget
{
    public class BankRecivement
    {
        private BankRecivement()
        {

        }

        public int BankRecivementId { get; internal set; }
        public int Serial { get; internal set; }
        public bool IsCustomerRecivement { get; internal set; }
        public int? CustomerId { get; internal set; }
        public string Description { get; internal set; }
        public decimal Amount { get; internal set; }
        public int BankId { get; internal set; }
        public DateTime Date { get; internal set; }

        public static Result ValidateBankPayment(BankRecivementDto bankRecivementDto)
        {
            if (bankRecivementDto.MaybeBank.HasNoValue)
                return Result.Failure(Messages.BankNotFound);

            if (bankRecivementDto.IsCustomerRecivement && bankRecivementDto.MaybeCustomer.HasNoValue)
                return Result.Failure(Messages.SelectSupplier);

            if (bankRecivementDto.Amount <= 0)
                return Result.Failure(Messages.AmountCantBeNegative);

            if (bankRecivementDto.Date < bankRecivementDto.MaybeBank.Value.Date)
                return Result.Failure(Messages.TransactionDateCantBeLessThanStartDate);

            return Result.Success();
        }

        public static Result<BankRecivement> CreateBankRecivement(
            BankRecivementDto bankRecivementDto,
            int maxSerial)
        {
            var res = ValidateBankPayment(bankRecivementDto);

            if (res.IsFailure)
                return Result.Failure<BankRecivement>(res.Error);

            var BankPayment = new BankRecivement()
            {
                Serial = maxSerial + 1,
                IsCustomerRecivement = bankRecivementDto.IsCustomerRecivement,
                CustomerId = bankRecivementDto.IsCustomerRecivement ? bankRecivementDto.MaybeCustomer.Value.CustomerId : null,
                Amount = bankRecivementDto.Amount,
                Date = bankRecivementDto.Date,
                Description = bankRecivementDto.Description,
                BankId = bankRecivementDto.MaybeBank.Value.BankId
            };

            return Result.Success(BankPayment);
        }

        public Result<BankRecivement> UpdateBankRecivement(
            BankRecivementDto bankRecivementDto)
        {
            var res = ValidateBankPayment(bankRecivementDto);

            if (res.IsFailure)
                return Result.Failure<BankRecivement>(res.Error);

            IsCustomerRecivement = bankRecivementDto.IsCustomerRecivement;
            CustomerId = bankRecivementDto.IsCustomerRecivement ? bankRecivementDto.MaybeCustomer.Value.CustomerId : null;
            Amount = bankRecivementDto.Amount;
            Date = bankRecivementDto.Date;
            Description = bankRecivementDto.Description;

            return Result.Success(this);
        }
    }
}
