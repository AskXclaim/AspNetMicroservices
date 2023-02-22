namespace Catalog.Application.Contracts.Persistence;

public interface IProductRepository
{
    Task<Product> GetProduct(string id);
    Task<List<Product>> GetProducts();
    Task<List<Product>> GetProductByName(string name);
    Task<List<Product>> GetProductByCategoryName(string categoryName);
    Task<bool> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}