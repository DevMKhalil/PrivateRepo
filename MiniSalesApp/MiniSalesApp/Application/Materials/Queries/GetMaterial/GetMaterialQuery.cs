using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Materials.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Materials.Queries.GetMaterial
{
    public class GetMaterialQuery : IRequest<List<MaterialDto>>
    {
    }

    public class GetMaterialQueryHandler : IRequestHandler<GetMaterialQuery, List<MaterialDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public GetMaterialQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }

        public async Task<List<MaterialDto>> Handle(GetMaterialQuery request, CancellationToken cancellationToken)
        {
            List<MaterialDto> result = new List<MaterialDto>();

            result = await _context.Materials.Select(x => new MaterialDto
            {
                MaterialId = x.MaterialId,
                Code = x.Code,
                Name = x.Name,
                SellPrice = x.SellPrice,
                PurchasePrice = x.PurchasePrice
            }).ToListAsync();

            return result;
        }
    }
}
