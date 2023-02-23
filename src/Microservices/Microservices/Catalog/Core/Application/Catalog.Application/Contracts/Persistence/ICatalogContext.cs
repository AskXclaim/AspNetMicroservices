namespace Catalog.Application.Contracts.Persistence;

public interface ICatalogContext
{
    public List<Product> Products { get; }
    Task<Product> FindAsync(string id);
    Task<List<Product>> FindAsync();
    Task<List<Product>> FindByNameAsync(string name);
    Task<List<Product>> FindByCategoryAsync(string categoryName);
    Task<string> Create(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(string id);
}