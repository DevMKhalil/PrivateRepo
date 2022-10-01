using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Bill.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Bill.Queries.GetBillById
{
    public class GetBillByIdQuery : IRequest<Result<BillDto>>
    {
        public int BillId { get; set; }
    }

    public class GetBillByIdQueryHandler : IRequestHandler<GetBillByIdQuery, Result<BillDto>>
    {
        private readonly IMiniSalesAppContext _context;
        private readonly IMapper _mapper;

        public GetBillByIdQueryHandler(IMiniSalesAppContext miniSalesAppContext,IMapper mapper)
        {
            _context = miniSalesAppContext;
            _mapper = mapper;
        }
        public async Task<Result<BillDto>> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
        {
            Maybe<BillDto> maybeBill = 
                await _context.Bills.Include(z => z.BillDetailList)
                .Where(x => x.BillId == request.BillId)
                .ProjectTo<BillDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (maybeBill.HasNoValue)
                return Result.Failure<BillDto>(Messages.BillNotFound);

            return Result.Success(maybeBill.Value);
        }
    }
}
