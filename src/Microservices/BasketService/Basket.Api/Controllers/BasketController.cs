namespace Basket.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BasketController : Controller
{
    private readonly IMapper _mapper;
    private readonly IBasketRepository _basketRepository;

    public BasketController(IMapper mapper, IBasketRepository basketRepository)
    {
        _mapper = mapper;
        _basketRepository = basketRepository;
    }

    [HttpGet("userName")]
    [ProducesResponseType(typeof(ShoppingCartDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ShoppingCartDto>> GetBasket(string userName)
    {
        if (!IsUserNameValid(userName)) return GetEmptyUserNameBadRequest(userName);

        var result = await _basketRepository.GetBasket(userName);

        if (result == null)
        {
            await _basketRepository.UpdateBasket(new ShoppingCart(userName));
            return Ok(new ShoppingCartDto(userName));
        }

        var shoppingCartDto = _mapper.Map<ShoppingCart>(result);
        return Ok(shoppingCartDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCartDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ShoppingCartDto>> UpdateBasket(ShoppingCartDto shoppingCart)
    {
        var cart = _mapper.Map<ShoppingCart>(shoppingCart);
        var result = await _basketRepository.UpdateBasket(cart);
        var shoppingCartDto = _mapper.Map<ShoppingCartDto>(result);
        return Ok(shoppingCartDto);
    }

    [HttpDelete]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        if (!IsUserNameValid(userName)) return GetEmptyUserNameBadRequest(userName);

        await _basketRepository.DeleteBasket(userName);
        return Ok();
    }

    private BadRequestObjectResult GetEmptyUserNameBadRequest(string userName) =>
        BadRequest($"{nameof(userName)} is cannot be null, empty or whitespace");

    private bool IsUserNameValid(string userName) => !string.IsNullOrWhiteSpace(userName);
}