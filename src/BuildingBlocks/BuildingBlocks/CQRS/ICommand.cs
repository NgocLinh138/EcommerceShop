using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand : ICommand<Unit> //Unit: weight type for mediator
    {
    }


    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
