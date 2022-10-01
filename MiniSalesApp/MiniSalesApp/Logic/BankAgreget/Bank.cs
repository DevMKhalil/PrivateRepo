using CSharpFunctionalExtensions;
using MiniSalesApp.Application.Banks.Dtos;
using MiniSalesApp.Logic.BankPaymentAgreget;
using MiniSalesApp.Logic.BankRecivementAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BankAgreget
{
    public class Bank
    {
        private Bank()
        {
            BankPaymentList = new List<BankPayment>();
            BankRecivementList = new List<BankRecivement>();
        }

        public int BankId { get; private set; }
        public string Name { get; private set; }
        public int Serial { get; private set; }
        public decimal StartAmount { get; private set; }
        public DateTime Date { get; private set; }
        public List<BankPayment> BankPaymentList { get; set; }
        public List<BankRecivement> BankRecivementList { get; set; }

        public static Result ValidateBank(BankDto bankDto)
        {
            if (bankDto.StartAmount < 0)
                return Result.Failure(Messages.StartAmountCantBeNegative);

            if (string.IsNullOrEmpty(bankDto.Name))
                return Result.Failure(Messages.NameIsRequired);

            return Result.Success();
        }

        internal static Result<Bank> CreateBank(BankDto bankDto, int maxSerial)
        {
            var res = ValidateBank(bankDto);

            if (res.IsFailure)
                return Result.Failure<Bank>(res.Error);

            var bank = new Bank
            {
                Name = bankDto.Name,
                Serial = maxSerial + 1,
                StartAmount = bankDto.StartAmount,
                Date = bankDto.Date
            };

            return Result.Success(bank);
        }

        public Result<Bank> UpdateBank(BankDto bankDto)
        {
            var res = ValidateBank(bankDto);

            if (res.IsFailure)
                return Result.Failure<Bank>(res.Error);

            StartAmount = bankDto.StartAmount;
            Name = bankDto.Name;

            return Result.Success(this);
        }

        internal Result VaildateForDelete()
        {
            if (this.BankRecivementList.Count > 0 || this.BankPaymentList.Count > 0)
                return Result.Failure(Messages.BankHasTransaction);

            return Result.Success();
        }
    }
}
