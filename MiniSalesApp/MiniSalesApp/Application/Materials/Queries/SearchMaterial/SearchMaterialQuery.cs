using MediatR;
using MiniSalesApp.Application.Materials.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiniSalesApp.Logic.MaterialAgreget;
using Microsoft.EntityFrameworkCore;

namespace MiniSalesApp.Application.Materials.Queries.SearchMaterial
{
    public class SearchMaterialQuery : IRequest<List<MaterialDto>>
    {
        public string Name { get; set; }
        public int? Code { get; set; }
        public decimal? SellPrice { get; set; }
        public decimal? PurchasePrice { get; set; }
    }

    public class SearchMaterialQueryHandler : IRequestHandler<SearchMaterialQuery, List<MaterialDto>>
    {
        private readonly IMiniSalesAppContext _context;
        public SearchMaterialQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<List<MaterialDto>> Handle(SearchMaterialQuery request, CancellationToken cancellationToken)
        {
            List<MaterialDto> result = new List<MaterialDto>();

            IQueryable<Material> materials = (from material in _context.Materials select material);

            if (!string.IsNullOrEmpty(request.Name))
                materials = materials.Where(x => x.Name.Contains(request.Name));

            if (request.Code == null || request.Code > default(int))
                materials = materials.Where(x => x.Code == request.Code);

            if (request.SellPrice == null || request.SellPrice > default(decimal))
                materials = materials.Where(x => x.SellPrice == request.SellPrice);

            if (request.PurchasePrice == null || request.PurchasePrice > default(decimal))
                materials = materials.Where(x => x.PurchasePrice == request.PurchasePrice);

            result = await (from material in materials
                            select new MaterialDto
                            {
                                MaterialId = material.MaterialId,
                                Code = material.Code,
                                Name = material.Name,
                                SellPrice = material.SellPrice,
                                PurchasePrice = material.PurchasePrice
                            }).ToListAsync();

            return result;
        }
    }
}
