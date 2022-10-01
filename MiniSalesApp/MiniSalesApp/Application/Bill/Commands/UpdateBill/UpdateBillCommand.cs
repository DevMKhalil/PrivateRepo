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

namespace MiniSalesApp.Application.Invoice.Commands.UpdateInvoice
{
    public class UpdateBillCommand : IRequest<Result<int>>
    {
        public BillDto bill { get; set; }
    }

    public class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateBillCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.BillAgreget.Bill> maybeBill = 
                await _context.Bills
                .Include(z => z.BillDetailList)
                .FirstOrDefaultAsync(x => x.BillId == request.bill.BillId);

            if (maybeBill.HasNoValue)
                return Result.Failure<int>(Messages.InvoiceNotFound);

            Logic.BillAgreget.Bill bill = maybeBill.Value;

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

            Logic.BillAgreget.Dtos.BillDto billDto = new Logic.BillAgreget.Dtos.BillDto
            {
                Date = request.bill.Date,
                Discount = request.bill.Discount,
                Discription = request.bill.Discription,
                MaybeSupplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == request.bill.SupplierId),
                BillDetailList = billDetailList
            };

            var updateResult = bill.UpdateBill(billDto);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(updateResult.Value.BillId);
        }
    }
}
