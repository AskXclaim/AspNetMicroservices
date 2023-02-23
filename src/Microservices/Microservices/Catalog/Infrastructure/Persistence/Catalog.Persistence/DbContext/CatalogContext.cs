namespace Catalog.Persistence.DbContext;

public class CatalogContext : ICatalogContext
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<ProductEntity> _products;

    public CatalogContext(IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
    {
        _mapper = mapper;
        var client = new MongoClient(databaseSettings.Value.ConnectionString);
        var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
        _products =
            database.GetCollection<ProductEntity>(databaseSettings.Value.CollectionName);
        _products = CatalogContextDataSeeder.SeedData(_products);
       
        Products = _mapper.Map<List<Product>>(_products.Find(p=>true).ToList());
    }

    public List<Product> Products { get; }

    public async Task<Product> FindAsync(string id)
    {
        var entityProduct = await _products.FindAsync(p => p.Id == id);
        return _mapper.Map<Product>(entityProduct);
    }

    public async Task<List<Product>> FindAsync()
    {
        var entityProducts = await (await _products.FindAsync(p => true)).ToListAsync();

        return _mapper.Map<List<Product>>(entityProducts);
    }

    public async Task<List<Product>> FindByNameAsync(string name)
    {
        var filter = Builders<ProductEntity>.Filter.Eq(p => p.Name, name);
        var entityProducts = await (await _products.FindAsync(filter)).ToListAsync();

        return _mapper.Map<List<Product>>(entityProducts);
    }

    //Note that you don't have to use filter you can use other ways.
    public async Task<List<Product>> FindByCategoryAsync(string categoryName)
    {
        var filter = Builders<ProductEntity>.Filter.Eq(p => p.Category, categoryName);
        var entityProducts = await (await _products.FindAsync(filter)).ToListAsync();

        return _mapper.Map<List<Product>>(entityProducts);
    }
    
    public async Task<string> Create(Product product)
    {
        var entityProduct = _mapper.Map<ProductEntity>(product);
        await _products.InsertOneAsync(entityProduct);

        return entityProduct.Id;
    }

    public async Task<bool> Update(Product product)
    {
        var entityProduct = _mapper.Map<ProductEntity>(product);
        var updateResult = await _products.ReplaceOneAsync(
            p => p.Id == entityProduct.Id, replacement: entityProduct);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
        var deletedResult = await _products.DeleteOneAsync(p => p.Id == id);
        return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
    }
}