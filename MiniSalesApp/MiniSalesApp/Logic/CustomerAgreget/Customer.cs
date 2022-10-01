using CSharpFunctionalExtensions;
using MiniSalesApp.Application.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSalesApp.Logic.CustomerAgreget
{
    public class Customer
    {
        public Customer()
        {

        }

        public int CustomerId { get; internal set; }
        public int Serial { get; internal set; }
        public string Name { get; internal set; }
        public string Phone { get; internal set; }
        public string Address { get; internal set; }
        public decimal Balance { get; internal set; }

        public static Result<Customer> CreateCustomer(CustomerDto CustomerDto, int maxSerial)
        {
            var res = ValidateCustomer(CustomerDto);

            if (res.IsFailure)
                return Result.Failure<Customer>(res.Error);

            var Customer = new Customer()
            {
                Serial = maxSerial + 1,
                Name = CustomerDto.Name,
                Phone = CustomerDto.Phone,
                Address = CustomerDto.Address,
                Balance = CustomerDto.Balance
            };

            return Result.Success(Customer);
        }

        public static Result ValidateCustomer(CustomerDto CustomerDto)
        {
            if (string.IsNullOrEmpty(CustomerDto.Name))
                return Result.Failure(Messages.NameIsRequired);

            if (CustomerDto.Serial <= 0)
                return Result.Failure(Messages.SerialIsRequired);

            return Result.Success();
        }

        public Result<Customer> UpdateCustomer(CustomerDto CustomerDto)
        {
            var res = ValidateCustomer(CustomerDto);

            if (res.IsFailure)
                return Result.Failure<Customer>(res.Error);

            Name = CustomerDto.Name;
            Phone = CustomerDto.Phone;
            Address = CustomerDto.Address;
            Balance = CustomerDto.Balance;

            return Result.Success(this);
        }
    }
}
