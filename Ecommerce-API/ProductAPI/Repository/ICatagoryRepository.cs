using ProductAPI.Model.Domain;

namespace ProductAPI.Repository
{
    public interface ICatagoryRepository
    {
        Task<bool> AddCatagoryAsync(Catagory catagory);
        Task<List<Catagory>> GetAllCatagoriesAsync();

    }
}
