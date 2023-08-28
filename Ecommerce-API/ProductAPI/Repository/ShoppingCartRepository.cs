using Microsoft.EntityFrameworkCore;
using ProductAPI.Model.Domain;

namespace ProductAPI.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _dbcontext;
        public ShoppingCartRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<bool> AddToCartAsync(ShoppingCart cartedProduct)
        {
            
               await _dbcontext.shoppingCarts.AddAsync(cartedProduct);
               var isAdded = await _dbcontext.SaveChangesAsync();

               if(isAdded > 0)return true;

               return false;
            
           
        }

        public async Task<List<Product>> GetCartItemsAsync(int userId)
        {
            var listOfCartedProduct =  await _dbcontext.shoppingCarts
                                            .AsNoTracking()
                                            .Where(x => x.UserId == userId)
                                            .Include(x=>x.Product)
                                            .Select(x=>x.Product)
                                            .ToListAsync();
                                                
            return listOfCartedProduct;

        }
       

        public async Task<int> CartedProductCount(int userId)
        {
            return await _dbcontext.shoppingCarts.Where(x => x.UserId == userId).CountAsync();
        }
    }
}
