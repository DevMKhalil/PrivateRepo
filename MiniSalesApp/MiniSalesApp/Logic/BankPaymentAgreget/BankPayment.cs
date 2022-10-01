using CSharpFunctionalExtensions;
using MiniSalesApp.Logic.BankPaymentAgreget.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.BankPaymentAgreget
{
    public class BankPayment
    {
        private BankPayment()
        {

        }

        public int BankPaymentId { get; internal set; }
        public int Serial { get; internal set; }
        public bool IsSupplierPayment { get; internal set; }
        public int? SupplierId { get; internal set; }
        public string Description { get; internal set; }
        public decimal Amount { get; internal set; }
        public int BankId { get; internal set; }
        public DateTime Date { get; internal set; }

        public static Result ValidateBankPayment(BankPaymentDto BankPaymentDto)
        {
            if (BankPaymentDto.MaybeBank.HasNoValue)
                return Result.Failure(Messages.BankNotFound);

            if (BankPaymentDto.IsSupplierPayment && BankPaymentDto.MaybeSupplier.HasNoValue)
                return Result.Failure(Messages.SelectSupplier);

            if (BankPaymentDto.Amount <= 0)
                return Result.Failure(Messages.AmountCantBeNegative);

            if (BankPaymentDto.Date < BankPaymentDto.MaybeBank.Value.Date)
                return Result.Failure(Messages.TransactionDateCantBeLessThanStartDate);

            return Result.Success();
        }

        public static Result<BankPayment> CreateBankPayment(
            BankPaymentDto BankPaymentDto,
            int maxSerial)
        {
            var res = ValidateBankPayment(BankPaymentDto);

            if (res.IsFailure)
                return Result.Failure<BankPayment>(res.Error);

            var BankPayment = new BankPayment()
            {
                Serial = maxSerial + 1,
                IsSupplierPayment = BankPaymentDto.IsSupplierPayment,
                SupplierId = BankPaymentDto.IsSupplierPayment ? BankPaymentDto.MaybeSupplier.Value.SupplierId : null,
                Amount = BankPaymentDto.Amount,
                Date = BankPaymentDto.Date,
                Description = BankPaymentDto.Description,
                BankId = BankPaymentDto.MaybeBank.Value.BankId
            };

            return Result.Success(BankPayment);
        }

        public Result<BankPayment> UpdateBankPayment(
            BankPaymentDto BankPaymentDto)
        {
            var res = ValidateBankPayment(BankPaymentDto);

            if (res.IsFailure)
                return Result.Failure<BankPayment>(res.Error);

            IsSupplierPayment = BankPaymentDto.IsSupplierPayment;
            SupplierId = BankPaymentDto.IsSupplierPayment ? BankPaymentDto.MaybeSupplier.Value.SupplierId : null;
            Amount = BankPaymentDto.Amount;
            Date = BankPaymentDto.Date;
            Description = BankPaymentDto.Description;

            return Result.Success(this);
        }
    }
}
