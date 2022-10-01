using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Materials.Dtos;
using MiniSalesApp.Application.InterFaces;
using MiniSalesApp.Logic.MaterialAgreget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Materials.Commands.UpdateMaterial
{
    public class UpdateMaterialCommand : IRequest<Result<int>>
    {
        public MaterialDto Material { get; set; }
    }

    public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateMaterialCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            Maybe<Material> material = await _context.Materials
               .FirstOrDefaultAsync(x => x.MaterialId == request.Material.MaterialId);

            if (material.HasNoValue)
                return Result.Failure<int>(Messages.MaterialNotFound);

            material.Value.UpdateMaterial(request.Material);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(material.Value.MaterialId);
        }
    }
}
