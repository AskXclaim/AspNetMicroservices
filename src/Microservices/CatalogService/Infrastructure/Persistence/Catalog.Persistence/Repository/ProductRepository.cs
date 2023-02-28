namespace Catalog.Persistence.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context) => _context = context;

    public async Task<Product> GetProduct(string id) => await _context.FindAsync(id);

    public async Task<List<Product>> GetProducts() => await _context.FindAsync();

    public async Task<List<Product>> GetProductByName(string name) =>
        await _context.FindByNameAsync(name);

    public async Task<List<Product>> GetProductByCategoryName(string categoryName) =>
        await _context.FindByCategoryAsync(categoryName);

    public async Task<bool> DoesProductExist(string name, string categoryName) =>
        (await _context.FindByNameAsync(name)).Count > 0 &&
        (await _context.FindByCategoryAsync(categoryName)).Count > 0;
    
    public async Task<string> CreateProduct(Product product) => await _context.Create(product);

    public async Task<bool> UpdateProduct(Product product) => await _context.Update(product);

    public async Task<bool> DeleteProduct(string id) => await _context.Delete(id);
}