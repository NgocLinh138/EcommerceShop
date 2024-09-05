using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator)
        : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            var aggregate = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity);

            var domainEvents = aggregate.SelectMany(x => x.DomainEvents).ToList();

            aggregate.ToList().ForEach(x => x.ClearDomainEvents()); 

            foreach ( var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
