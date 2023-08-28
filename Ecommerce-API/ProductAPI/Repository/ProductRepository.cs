using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Model.Domain;
using ProductAPI.Model.DTO;

namespace ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public async Task<bool> CreateProductAsync(ProductDTO product)
        {
            var DBProduct = _mapper.Map<Product>(product);
           
            await _context.products.AddAsync(DBProduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Product?> DeleteProduct(int id)
        {
             var product = await _context.products.FindAsync(id);

            if (product is null) return product;

            _context.products.Remove(product);

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Tuple<List<Product>,long>> GetAllProductAsync(int page)
        {
            int productPerPage = 12;
            long totalProduct = await _context.products
                                              .AsNoTracking()
                                              .CountAsync();

            long totalNumberOfPage = (int)Math.Ceiling((double)totalProduct / productPerPage);

            var listOfProduct = await _context.products
                                              .Skip((page-1) * productPerPage)
                                              .Take(productPerPage)
                                              .ToListAsync();

            return new Tuple<List<Product>,long>(listOfProduct,totalNumberOfPage);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.products
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        public async Task<Tuple<List<Product>, long>> GetProductsByCatagoryAsync(int id, int page)
        {
            int productPerPage = 12;

            long totalProduct =  _context.products
                                         .Where(product => product.catagoryId == id)
                                         .Count();

            long totalNumberOfPage = (int)Math.Ceiling((double)totalProduct / productPerPage);

            var listOfProduct = await _context.products
                                              .AsNoTracking()
                                              .Where(product => product.catagoryId == id)
                                              .Skip((page - 1) * productPerPage)
                                              .Take(productPerPage)
                                              .ToListAsync();

           

            return new Tuple<List<Product>, long>(listOfProduct, totalNumberOfPage);
        }
    }
}
