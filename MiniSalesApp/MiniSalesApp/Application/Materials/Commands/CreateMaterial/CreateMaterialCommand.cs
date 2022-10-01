using CSharpFunctionalExtensions;
using MediatR;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiniSalesApp.Logic.MaterialAgreget;
using MiniSalesApp.Application.Materials.Dtos;

namespace MiniSalesApp.Application.Materials.Commands.CreateMaterial
{
    public class CreateMaterialCommand : IRequest<Result<int>>
    {
        public MaterialDto Material { get; set; }
    }

    public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateMaterialCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }

        public async Task<Result<int>> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            var createResult = Material.CreateMaterial(request.Material);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.Materials.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.MaterialId);
        }
    }
}
