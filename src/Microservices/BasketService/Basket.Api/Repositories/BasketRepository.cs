namespace Basket.Api.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _cache;

    public BasketRepository(IDistributedCache cache) => _cache = cache;

    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basketAsString = await _cache.GetStringAsync(userName);
        return string.IsNullOrWhiteSpace(basketAsString)
            ? null
            : JsonSerializer.Deserialize<ShoppingCart>(basketAsString);
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
        await _cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));
        return await GetBasket(basket.UserName);
    }

    public async Task DeleteBasket(string userName)
    {
        await _cache.RemoveAsync(userName);
        if (await GetBasket(userName) != null)
            throw new Exception($"Basket for user '{userName}' was not deleted successfully");
    }
}