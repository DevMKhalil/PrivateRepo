﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StorePayment.Queries.GetMaxSerial
{
    public class GetMaxSerialQuery : IRequest<int>
    {
    }

    public class GetMaxSerialQueryHandler : IRequestHandler<GetMaxSerialQuery, int>
    {
        private readonly IMiniSalesAppContext _context;
        public GetMaxSerialQueryHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<int> Handle(GetMaxSerialQuery request, CancellationToken cancellationToken)
        {
            var result = await (from pay in _context.StoreDailies
                                select pay.StorePaymentList.Max(x => (int?)x.Serial) ?? 0).FirstAsync();

            return result;
        }
    }
}
