namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CatalogController : Controller
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id:length(24)}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(string id)
    {
        var productDto = await _mediator.Send(new GetProductQuery(id));
        return Ok(productDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        var productsDto = await _mediator.Send(new GetProductsQuery());
        return Ok(productsDto);
    }

    [HttpGet("{name:length(30)}")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductDto>>> GetProductByName(string name)
    {
        var productsDto = await _mediator.Send(new GetProductsByNameQuery(name));
        return Ok(productsDto);
    }

    [HttpGet("{categoryName:length(30)}")]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductDto>>> GetProductByCategory(string categoryName)
    {
        var productsDto = await _mediator.Send(new GetProductsByCategoryNameQuery(categoryName));
        return Ok(productsDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductDto product)
    {
        var createdProductId = await _mediator.Send(new CreateProductCommand(product.Name, product.Category,
            product.Summary, product.Description, product.ImageFile, product.Price));
        product.Id = createdProductId;
        return CreatedAtRoute($"{nameof(GetProduct)}", new {id = createdProductId}, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product)
    {
        var isProductUpdated = await _mediator.Send(new UpdateProductCommand(product.Name, product.Category,
            product.Summary, product.Description, product.ImageFile, product.Price));
        return Ok(isProductUpdated);
    }

    [HttpDelete("{id:length(24)}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var isProductDeleted = await _mediator.Send(new DeleteProductCommand(id));
        return Ok(isProductDeleted);
    }
}