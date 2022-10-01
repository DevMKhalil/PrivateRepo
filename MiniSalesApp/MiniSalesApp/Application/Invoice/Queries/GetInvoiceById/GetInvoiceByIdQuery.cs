using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Invoice.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Invoice.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQuery : IRequest<Result<InvoiceDto>>
    {
        public int InvoiceId { get; set; }
    }

    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, Result<InvoiceDto>>
    {
        private readonly IMiniSalesAppContext _context;
        private readonly IMapper _mapper;

        public GetInvoiceByIdQueryHandler(IMiniSalesAppContext miniSalesAppContext,IMapper mapper)
        {
            _context = miniSalesAppContext;
            _mapper = mapper;
        }
        public async Task<Result<InvoiceDto>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            Maybe<InvoiceDto> maybeInvoice = 
                await _context.Invoices.Include(z => z.InvoiceDetailList)
                .Where(x => x.InvoiceId == request.InvoiceId)
                .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (maybeInvoice.HasNoValue)
                return Result.Failure<InvoiceDto>(Messages.InvoiceNotFound);

            return Result.Success(maybeInvoice.Value);
        }
    }
}
