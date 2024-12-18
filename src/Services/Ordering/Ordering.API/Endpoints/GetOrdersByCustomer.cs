﻿
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByCustomerRequest();
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", 
                async (Guid customerId, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                    var response = result.Adapt<GetOrdersByCustomerResponse>();

                    return Results.Ok(response);
                })
            .WithName("Get order by customer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get order by customer")
            .WithDescription("Get order by customer");
        }
    }
}
