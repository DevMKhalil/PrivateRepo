using CSharpFunctionalExtensions;
using MediatR;
using MiniSalesApp.Application.Suppliers.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiniSalesApp.Logic.SupplierAgreget;
using Microsoft.EntityFrameworkCore;

namespace MiniSalesApp.Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommand : IRequest<Result<int>>
    {
        public SupplierDto Supplier { get; set; }
    }

    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateSupplierCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            Maybe<Supplier> Supplier = await _context.Suppliers
               .FirstOrDefaultAsync(x => x.SupplierId == request.Supplier.SupplierId);

            if (Supplier.HasNoValue)
                return Result.Failure<int>(Messages.SupplierNotFound);

            Supplier.Value.UpdateSupplier(request.Supplier);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(Supplier.Value.SupplierId);
        }
    }
}
