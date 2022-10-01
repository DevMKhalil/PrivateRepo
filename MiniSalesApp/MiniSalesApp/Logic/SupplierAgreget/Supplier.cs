using CSharpFunctionalExtensions;
using MiniSalesApp.Application.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.SupplierAgreget
{
    public class Supplier
    {
        public Supplier()
        {

        }

        public int SupplierId { get; internal set; }
        public int Serial { get; internal set; }
        public string Name { get; internal set; }
        public string Phone { get; internal set; }
        public string Address { get; internal set; }
        public decimal Balance { get; internal set; }

        public static Result<Supplier> CreateSupplier(SupplierDto SupplierDto, int maxSerial)
        {
            var res = ValidateSupplier(SupplierDto);

            if (res.IsFailure)
                return Result.Failure<Supplier>(res.Error);

            var Supplier = new Supplier()
            {
                Serial = maxSerial + 1,
                Name = SupplierDto.Name,
                Phone = SupplierDto.Phone,
                Address = SupplierDto.Address,
                Balance = SupplierDto.Balance
            };

            return Result.Success(Supplier);
        }

        public static Result ValidateSupplier(SupplierDto SupplierDto)
        {
            if (string.IsNullOrEmpty(SupplierDto.Name))
                return Result.Failure(Messages.NameIsRequired);

            if (SupplierDto.Serial <= 0)
                return Result.Failure(Messages.SerialIsRequired);

            return Result.Success();
        }

        public Result<Supplier> UpdateSupplier(SupplierDto SupplierDto)
        {
            var res = ValidateSupplier(SupplierDto);

            if (res.IsFailure)
                return Result.Failure<Supplier>(res.Error);

            Name = SupplierDto.Name;
            Phone = SupplierDto.Phone;
            Address = SupplierDto.Address;
            Balance = SupplierDto.Balance;

            return Result.Success(this);
        }
    }
}
