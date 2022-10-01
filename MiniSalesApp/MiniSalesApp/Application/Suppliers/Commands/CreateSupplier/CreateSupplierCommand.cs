using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Suppliers.Dtos;
using MiniSalesApp.Application.InterFaces;
using MiniSalesApp.Logic.SupplierAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommand : IRequest<Result<int>>
    {
        public SupplierDto Supplier { get; set; }
    }

    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateSupplierCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }

        public async Task<Result<int>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await _context.Suppliers.MaxAsync(x => (int?)x.Serial) ?? 0;

            var createResult = Supplier.CreateSupplier(request.Supplier, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.Suppliers.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.SupplierId);
        }
    }
}
