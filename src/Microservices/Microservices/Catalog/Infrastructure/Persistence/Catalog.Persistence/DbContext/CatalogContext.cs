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

        Products = _mapper.Map<List<Product>>(_products.Find(p => true).ToList());
    }

    public List<Product> Products { get; }

    public async Task<Product> FindAsync(string id)
    {
        var productEntity = _products.Find(p => p.Id == id).FirstOrDefault();
        return _mapper.Map<Product>(productEntity);
    }

    public async Task<List<Product>> FindAsync()
    {
        var productEntities = await (await _products.FindAsync(p => true)).ToListAsync();

        return _mapper.Map<List<Product>>(productEntities);
    }

    public Task<List<Product>> FindByNameAsync(string name)
    {
        var productEntities = _products.AsQueryable().Where(p =>
            p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        // var filter = Builders<ProductEntity>.Filter.Eq(p => p.Name, name);
        // var productEntities = await (await _products.FindAsync(filter)).ToListAsync();

        return Task.FromResult(_mapper.Map<List<Product>>(productEntities));
    }

    //Note that you don't have to use filter you can use other ways.
    public Task<List<Product>> FindByCategoryAsync(string categoryName)
    {
        var productEntities = _products.AsQueryable().Where(p =>
            p.Category.Equals(categoryName, StringComparison.OrdinalIgnoreCase)).ToList();
        // var filter = Builders<ProductEntity>.Filter.Eq(p =>
        //     p.Category, categoryName);
        // var entityProducts = await (await _products.FindAsync(filter)).ToListAsync();

        return Task.FromResult(_mapper.Map<List<Product>>(productEntities));
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