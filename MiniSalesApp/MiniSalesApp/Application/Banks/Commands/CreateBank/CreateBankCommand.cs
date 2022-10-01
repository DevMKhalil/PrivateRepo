using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.Banks.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.Banks.Commands.CreateBank
{
    public class CreateBankCommand : IRequest<Result<int>>
    {
        public BankDto Bank { get; set; }
    }

    public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateBankCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await _context.StoreDailies.MaxAsync(x => (int?)x.Serial) ?? 0;

            var createResult = Logic.BankAgreget.Bank.CreateBank(
                request.Bank, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.Banks.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.BankId);
        }
    }
}
