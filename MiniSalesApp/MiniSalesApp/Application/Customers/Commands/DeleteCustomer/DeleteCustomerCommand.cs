using CSharpFunctionalExtensions;
using MediatR;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiniSalesApp.Logic.CustomerAgreget;
using Microsoft.EntityFrameworkCore;

namespace MiniSalesApp.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Result>
    {
        public int CustomerId { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteCustomerCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            Maybe<Customer> Customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId);

            if (Customer.HasNoValue)
                return Result.Failure(Messages.CustomerNotFound);

            _context.Customers.Remove(Customer.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
