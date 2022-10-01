using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreDaily.Commands.DeleteStoreDaily
{
    public class DeleteStoreDailyCommand : IRequest<Result>
    {
        public int StoreDailyId { get; set; }
    }

    public class DeleteStoreDailyCommandHandler : IRequestHandler<DeleteStoreDailyCommand, Result>
    {
        private readonly IMiniSalesAppContext _context;

        public DeleteStoreDailyCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result> Handle(DeleteStoreDailyCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.StoreDailyAgreget.StoreDaily> StoreDailyResult = await _context.StoreDailies
                .Include(x => x.StoreRecivementList)
                .Include(x => x.StorePaymentList)
                .FirstOrDefaultAsync(x => x.StoreDailyId == request.StoreDailyId);

            if (StoreDailyResult.HasNoValue)
                return Result.Failure(Messages.StoreDailyNotFound);

            Logic.StoreDailyAgreget.StoreDaily storeDaily = StoreDailyResult.Value;

            int lastDailyId = await _context.StoreDailies.MaxAsync(x => (int?)x.StoreDailyId) ?? 0;

            var VaildateIsLastDailyResult = storeDaily.VaildateIsLastDaily(lastDailyId);

            if (VaildateIsLastDailyResult.IsFailure)
                return Result.Failure(VaildateIsLastDailyResult.Error);

            var validateForDeleteResult = storeDaily.VaildateForDelete();

            if (validateForDeleteResult.IsFailure)
                return Result.Failure(validateForDeleteResult.Error);

            _context.StoreDailies.Remove(storeDaily);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success();
        }
    }
}
