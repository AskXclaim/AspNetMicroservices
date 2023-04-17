namespace Order.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IUserDetailsRepository _userDetailsRepository;
    private readonly IPaymentDetailRepository _paymentDetailRepository;

    public GetOrdersListQueryHandler(IMapper mapper, IOrderRepository orderRepository,
        IAddressRepository addressRepository, IUserDetailsRepository userDetailsRepository,
        IPaymentDetailRepository paymentDetailRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _addressRepository = addressRepository;
        _userDetailsRepository = userDetailsRepository;
        _paymentDetailRepository = paymentDetailRepository;
    }

    public async Task<List<OrderDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orders = (await _orderRepository.GetOrdersByUserName(request.UserName)).ToList();

        if (!orders.Any()) return new List<OrderDto>();

        var orderDtos = new List<OrderDto>();
        foreach (var order in orders)
        {
            var address = await _addressRepository.GetByIdAsync(orders.First().AddressId);
            var userDetails = await _userDetailsRepository.GetByIdAsync(orders.First().UserDetailsId);
            var paymentDetail = await _paymentDetailRepository.GetByIdAsync(orders.First().PaymentId);
            var orderDto = new OrderDto
            {
                Address = _mapper.Map<AddressDto>(address),
                UserDetails = _mapper.Map<UserDetailsDto>(userDetails),
                PaymentDetail = _mapper.Map<PaymentDetailDto>(paymentDetail)
            };
            orderDtos.Add(orderDto);
        }

        _mapper.Map(orders, orderDtos);

        return orderDtos;
    }
}