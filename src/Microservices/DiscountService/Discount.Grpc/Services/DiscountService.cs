using Discount.Grpc.Protos;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IDiscountRepository discountRepository, IMapper mapper, ILogger<DiscountService> logger)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<CouponGrpcModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var couponEntity = await _discountRepository.GetDiscount(request.ProductName);
        if (couponEntity == null || couponEntity.Id == 0)
        {
            var status = new Status(StatusCode.NotFound,
                $"Discount with ProductName={request.ProductName} is not found");
            _logger.LogError("Error in {DiscountName} GRPC call. Error;{Status}", nameof(GetDiscount), status);
            throw new RpcException(status);
        }

        _logger.LogInformation("Discount successfully retrieved for Product name;'{ProductName}'",
            couponEntity.ProductName);

        return _mapper.Map<CouponGrpcModel>(couponEntity);
    }

    public override async Task<CouponGrpcModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);

        var createdCouponId = await _discountRepository.CreateDiscount(coupon);

        if (createdCouponId == 0)
        {
            var status = new Status(StatusCode.Internal, $"Product:'{request.Coupon.ProductName}' not created");
            _logger.LogError(status.Detail);
            throw new RpcException(status);
        }

        _logger.LogInformation("Product:\'{ProductName}\' created", request.Coupon.ProductName);

        coupon.Id = createdCouponId;
        return _mapper.Map<CouponGrpcModel>(coupon);
    }

    public override async Task<CouponGrpcModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);

        var isUpdated = await _discountRepository.UpdateDiscount(coupon);

        if (!isUpdated)
        {
            var status = new Status(StatusCode.Internal, $"Product:'{request.Coupon.ProductName}' not updated");
            _logger.LogError(status.Detail);
            throw new RpcException(status);
        }

        _logger.LogInformation("Product:\'{ProductName}\' updated", request.Coupon.ProductName);

        return _mapper.Map<CouponGrpcModel>(coupon);
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var isDeleted = await _discountRepository.DeleteDiscount(request.ProductName);
        if (!isDeleted)
        {
            var status = new Status(StatusCode.Internal, $"Product:'{request.ProductName}' not deleted");
            _logger.LogError(status.Detail);
            throw new RpcException(status);
        }

        _logger.LogInformation("Product:\'{ProductName}\' deleted", request.ProductName);

        return new DeleteDiscountResponse()
        {
            Success = isDeleted
        };
    }
}