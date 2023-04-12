namespace Discount.Grpc.MappingProfiles;

public class CouponProfile:Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}