namespace Order.Application.Features.Orders.Queries.GetOrdersList;

public record GetOrdersListQuery(string UserName):IRequest<List<OrderDto>>;