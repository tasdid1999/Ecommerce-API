using ProductAPI.Model.Domain;
using ProductAPI.Model.DTO;

namespace ProductAPI.Repository
{
    public interface IAuthRepository
    {
        Task<User?> IsUserEmailAndPasswordExistAsync(UserLoginDTO user);

       Task<bool> RegisterAsync(UserRegisterDTO user);

       Task<bool> IsUserEmailExistAsync (string email);
    }
}
