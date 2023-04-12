using Dapper;

namespace Discount.Grpc.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration) => _configuration = configuration;

    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var connection = GetNpgsqlConnection();

        var coupon = connection.QueryFirstOrDefault<Coupon>(
            "SELECT DISTINCT * FROM Coupon WHERE ProductName=@ProductName", new {ProductName = productName});

        return coupon ?? new Coupon() {ProductName = "No Discount", Amount = 0.0M, Description = "No Discount"};
    }

    public async Task<int> CreateDiscount(Coupon coupon)
    {
        await using var connection = GetNpgsqlConnection();

        var isCreated = await connection.ExecuteAsync(
            "INSERT INTO Coupon(ProductName,Description,Amount) VALUES(@ProductName,@Description,@Amount)",
            new {ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount});

        return isCreated > 0 ? (await GetDiscount(coupon.ProductName)).Id : isCreated;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = GetNpgsqlConnection();

        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id",
            new
            {
                Id = coupon.Id, ProductName = coupon.ProductName,
                Description = coupon.Description, Amount = coupon.Amount
            });

        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connection = GetNpgsqlConnection();

        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName =@ProductName",
            new {ProductName = productName});

        return affected != 0;
    }

    private NpgsqlConnection GetNpgsqlConnection() => new(
        _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
}