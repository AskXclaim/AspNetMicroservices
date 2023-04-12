using Discount.Grpc.Protos;

namespace Discount.Grpc.MappingProfiles;

public class CouponProfile:Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
        CreateMap<Coupon, CouponGrpcModel>().ReverseMap();
    }
}