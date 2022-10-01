using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MiniSalesApp.Application.Customers.Dtos;
using MiniSalesApp.Logic.CustomerAgreget;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Result<int>>
    {
        public CustomerDto Customer { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateCustomerCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }

        public async Task<Result<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await _context.Customers.MaxAsync(x => (int?)x.Serial) ?? 0;

            var createResult = Customer.CreateCustomer(request.Customer, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.Customers.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.CustomerId);
        }
    }
}
