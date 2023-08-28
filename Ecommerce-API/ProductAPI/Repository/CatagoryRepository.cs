using Microsoft.EntityFrameworkCore;
using ProductAPI.Model.Domain;

namespace ProductAPI.Repository
{
    public class CatagoryRepository : ICatagoryRepository
    {

        private readonly AppDbContext _context;
        public CatagoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCatagoryAsync(Catagory catagory)
        {
            await _context.catagories.AddAsync(catagory);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<List<Catagory>> GetAllCatagoriesAsync()
        {
            return await _context.catagories.ToListAsync();
        }
    }
}
