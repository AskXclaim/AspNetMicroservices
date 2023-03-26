namespace Discount.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DiscountController : Controller
{
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;

    public DiscountController(IMapper mapper, IDiscountRepository discountRepository)
    {
        _mapper = mapper;
        _discountRepository = discountRepository;
    }

    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(CouponDto), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<CouponDto>> GetDiscount(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            return BadRequest($"Invalid {nameof(productName)}");

        var coupon = await _discountRepository.GetDiscount(productName);

        var result = _mapper.Map<CouponDto>(coupon);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CouponDto>> CreateCoupon([FromBody] Coupon coupon)
    {
        var createdCouponId = await _discountRepository.CreateDiscount(coupon);
        
        if (createdCouponId <= 0) return BadRequest();
        coupon.Id = createdCouponId;
        return CreatedAtRoute(nameof(GetDiscount),
            new {productName = coupon.ProductName}, _mapper.Map<CouponDto>(coupon));

    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CouponDto>> UpdateDiscount([FromBody] Coupon coupon)
    {
        var isUpdated = await _discountRepository.UpdateDiscount(coupon);

        if (isUpdated) return Ok(coupon);

        return BadRequest();
    }

    [HttpDelete("{productName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> DeleteDiscount(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            return BadRequest($"Invalid {nameof(productName)}");

        var isDeleted= await _discountRepository.DeleteDiscount(productName);

        return Ok(isDeleted);
    }
}