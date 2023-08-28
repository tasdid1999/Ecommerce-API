using ProductAPI.Model.Domain;
using ProductAPI.Model.DTO;

namespace ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(ProductDTO product);
        Task<Tuple<List<Product>, long>> GetAllProductAsync(int page);
        Task<Tuple<List<Product>, long>> GetProductsByCatagoryAsync(int id , int page);
        Task<Product?> DeleteProduct(int id);

        Task<Product?> GetProductByIdAsync(int id);
    }
}
