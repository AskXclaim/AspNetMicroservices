namespace Discount.Grpc.Repositories.Interfaces;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);
    Task<int> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
}