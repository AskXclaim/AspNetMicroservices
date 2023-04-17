namespace Basket.Api.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountGrpcClient;
    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountGrpcClient)=>
        _discountGrpcClient = discountGrpcClient;
    

    public async Task<CouponGrpcModel> GetCoupon(string productName)
    {
        var request = new GetDiscountRequest {ProductName = productName};

        return await _discountGrpcClient.GetDiscountAsync(request);
    }
}