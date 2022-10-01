using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniSalesApp.Application.StoreRecivement.Dtos;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniSalesApp.Application.StoreRecivement.Commands.UpdateStoreRecivement
{
    public class UpdateStoreRecivementCommand : IRequest<Result<int>>
    {
        public Dtos.StoreRecivementDto StoreRecivement { get; set; }
    }

    public class UpdateStoreRecivementCommandHandler : IRequestHandler<UpdateStoreRecivementCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public UpdateStoreRecivementCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }
        public async Task<Result<int>> Handle(UpdateStoreRecivementCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.StoreDailyAgreget.StoreDaily> StoreDailyRecivementResult =
               await _context.StoreDailies
               .Include(x => x.StoreRecivementList)
               .Include(x => x.StorePaymentList)
               .FirstOrDefaultAsync(z => z.StoreRecivementList.Any(t => t.StoreRecivementId == request.StoreRecivement.StoreRecivementId));

            if (StoreDailyRecivementResult.HasNoValue)
                return Result.Failure<int>(Messages.StoreDailyNotFound);

            Logic.StoreDailyAgreget.StoreDaily storeDaily = StoreDailyRecivementResult.Value;

            int lastDailyId = await _context.StoreDailies.MaxAsync(x => (int?)x.StoreDailyId) ?? 0;

            var VaildateIsLastDailyResult = storeDaily.VaildateIsLastDaily(lastDailyId);

            if (VaildateIsLastDailyResult.IsFailure)
                return Result.Failure<int>(VaildateIsLastDailyResult.Error);

            Maybe<Logic.CustomerAgreget.Customer> maybeCustomer =
               await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == request.StoreRecivement.CustomerId);

            var dto = new Logic.StoreRecivementAgreget.Dto.StoreRecivementDto
            {
                Amount = request.StoreRecivement.Amount,
                Date = request.StoreRecivement.Date,
                Description = request.StoreRecivement.Description,
                IsCustomerRecivement = request.StoreRecivement.IsCustomerRecivement,
                MaybeStoreDaily = StoreDailyRecivementResult,
                MaybeCustomer = maybeCustomer,
                StoreRecivementId = request.StoreRecivement.StoreRecivementId
            };

            var rescivement = storeDaily.StoreRecivementList.FirstOrDefault(x => x.StoreRecivementId == request.StoreRecivement.StoreRecivementId);

            var updateResult = rescivement.UpdateStoreRecivement(dto);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            storeDaily.UpdateEndAmount();

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(updateResult.Value.StoreRecivementId);
        }
    }
}
