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

namespace MiniSalesApp.Application.StoreRecivement.Commands.CreateStoreRecivement
{
    public class CreateStoreRecivementCommand : IRequest<Result<int>>
    {
        public Dtos.StoreRecivementDto StoreRecivement { get; set; }
    }

    public class CreateStoreRecivementCommandHandler : IRequestHandler<CreateStoreRecivementCommand, Result<int>>
    {
        private readonly IMiniSalesAppContext _context;

        public CreateStoreRecivementCommandHandler(IMiniSalesAppContext miniSalesAppContext)
        {
            _context = miniSalesAppContext;
        }

        public async Task<Result<int>> Handle(CreateStoreRecivementCommand request, CancellationToken cancellationToken)
        {
            var maxSerial = await (from recive in _context.StoreDailies
                                   select recive.StoreRecivementList.Max(x => (int?)x.Serial) ?? 0).FirstAsync();

            Maybe<Logic.StoreDailyAgreget.StoreDaily> lastDailyResult = await _context.StoreDailies
                .Include(x => x.StorePaymentList)
                .Include(x => x.StoreRecivementList).OrderByDescending(x => x.StoreDailyId)
                .FirstOrDefaultAsync();

            if (lastDailyResult.HasNoValue)
                return Result.Failure<int>(Messages.StoreDailyNotFound);

            Logic.StoreDailyAgreget.StoreDaily lastDaily = lastDailyResult.Value;

            Maybe<Logic.CustomerAgreget.Customer> maybeCustomer =
                await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == request.StoreRecivement.CustomerId);

            var dto = new Logic.StoreRecivementAgreget.Dto.StoreRecivementDto
            {
                Amount = request.StoreRecivement.Amount,
                Date = request.StoreRecivement.Date,
                Description = request.StoreRecivement.Description,
                IsCustomerRecivement = request.StoreRecivement.IsCustomerRecivement,
                MaybeStoreDaily = lastDaily,
                MaybeCustomer = maybeCustomer
            };

            var createResult = Logic.StoreRecivementAgreget.StoreRecivement.CreateStoreRecivement(dto, maxSerial);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            lastDaily.StoreRecivementList.Add(createResult.Value);

            lastDaily.UpdateEndAmount();

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.StoreRecivementId);
        }
    }
}
