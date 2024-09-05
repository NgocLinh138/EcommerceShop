using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions
{
    public class OrderException
    {
        public class OrderNotFoundException : NotFoundException
        {
            public OrderNotFoundException(Guid id) : base("Order", id)
            {
            }
        }
    }
}
