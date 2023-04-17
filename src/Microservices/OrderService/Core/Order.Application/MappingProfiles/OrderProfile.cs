namespace Order.Application.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<UserDetails, UserDetailsDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<PaymentDetail, PaymentDetailDto>().ForMember(
                dest => dest.NameOnPaymentMethod,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PaymentMethod,
                opt => opt.MapFrom(src => src.PaymentMethodName))
            .ForMember(dest => dest.PaymentMethodExpirationDate,
                opt => opt.MapFrom(src => src.ExpirationDate));
        CreateMap<Domain.Entities.Order, OrderDto>().ReverseMap();
    }
}