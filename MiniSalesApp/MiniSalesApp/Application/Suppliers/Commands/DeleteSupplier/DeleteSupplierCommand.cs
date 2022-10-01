using CSharpFunctionalExtensions;
using MediatR;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiniSalesApp.Logic.SupplierAgreget;
using Microsoft.EntityFrameworkCore;

namespace MiniSalesApp.Application.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommand : IRequest<Result>
    {
        public int SupplierId { get; set; }
    }

    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteSupplierCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            Maybe<Supplier> Supplier = await _context.Suppliers
                .FirstOrDefaultAsync(x => x.SupplierId == request.SupplierId);

            if (Supplier.HasNoValue)
                return Result.Failure(Messages.SupplierNotFound);

            _context.Suppliers.Remove(Supplier.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
