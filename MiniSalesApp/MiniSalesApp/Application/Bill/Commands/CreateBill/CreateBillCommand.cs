using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MiniSalesApp.Application.Customers.Commands.CreateCustomer;
using MiniSalesApp.Application.Bill.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Invoice.Commands.CreateInvoice
{
    public class CreateBillCommand : IRequest<Result<int>>
    {
        public BillDto bill { get; set; }
    }

    public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateBillCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await _context.Bills.MaxAsync(x => (int?)x.Serial) ?? 0;

            var materialsIds = request.bill.BillDetailList.Select(x => x.MaterialId).ToList();

            List<Logic.MaterialAgreget.Material> materials = await _context.Materials
                .Where(z => materialsIds.Contains(z.MaterialId))
                .Select(x => x)
                .ToListAsync();

            var billDetailList = request.bill.BillDetailList.Select(y => new Logic.BillAgreget.Dtos.BillDetailDto
            {
                Quantity = y.Quantity,
                PurchasePrice = y.PurchasePrice,
                maybeMaterial = materials.FirstOrDefault(x => x.MaterialId == y.MaterialId)
            }).ToList();

            Logic.BillAgreget.Dtos.BillDto bill = new Logic.BillAgreget.Dtos.BillDto
            {
                Date = request.bill.Date,
                Discount = request.bill.Discount,
                Discription = request.bill.Discription,
                MaybeSupplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == request.bill.SupplierId),
                BillDetailList = billDetailList
            };

            var createResult = MiniSalesApp.Logic.BillAgreget.Bill.CreateBill(bill, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.Bills.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.BillId);
        }
    }
}
