using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.InterFaces;
using MiniSalesApp.Logic.MaterialAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Materials.Commands.DeleteMaterial
{
    public class DeleteMaterialCommand : IRequest<Result>
    {
        public int MaterialId { get; set; }
    }

    public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteMaterialCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
        {
            Maybe<Material> material = await _context.Materials
                .FirstOrDefaultAsync(x => x.MaterialId == request.MaterialId);

            if (material.HasNoValue)
                return Result.Failure(Messages.MaterialNotFound);

            _context.Materials.Remove(material.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
