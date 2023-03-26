namespace Discount.Api.MappingProfiles;

public class CouponProfile:Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}