using CSharpFunctionalExtensions;
using MediatR;
using MiniSalesApp.Application.Customers.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiniSalesApp.Logic.CustomerAgreget;
using Microsoft.EntityFrameworkCore;

namespace MiniSalesApp.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Result<int>>
    {
        public CustomerDto Customer { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateCustomerCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Maybe<Customer> Customer = await _context.Customers
               .FirstOrDefaultAsync(x => x.CustomerId == request.Customer.CustomerId);

            if (Customer.HasNoValue)
                return Result.Failure<int>(Messages.CustomerNotFound);

            Customer.Value.UpdateCustomer(request.Customer);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(Customer.Value.CustomerId);
        }
    }
}
