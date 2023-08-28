using ProductAPI.Model.Domain;

namespace ProductAPI.Repository
{
    public interface IShoppingCartRepository
    {
        Task<List<Product>> GetCartItemsAsync(int userId);

        Task<bool> AddToCartAsync(ShoppingCart cartedProduct);

        Task<int> CartedProductCount(int userId);
    }
}
