using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Model.Domain;
using ProductAPI.Model.DTO;

namespace ProductAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper mapper;

        public AuthRepository(AppDbContext appDbContext , IMapper _mapper)
        {
            _appDbContext = appDbContext;
            mapper = _mapper;
        }
        public async Task<User?> IsUserEmailAndPasswordExistAsync(UserLoginDTO user)
        {
            var ExistinigUser = await _appDbContext.users
                                                   .FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);

            return ExistinigUser;
        }

        public async Task<bool> IsUserEmailExistAsync(string email)
        {
            var isExist = await _appDbContext.users
                                             .FirstOrDefaultAsync(x => x.Email == email);

            if(isExist is null) return false;

            return true;
        }

        public async Task<bool> RegisterAsync(UserRegisterDTO user)
        {
            var DBUser = mapper.Map<User>(user);

            await _appDbContext.AddAsync(DBUser);

            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
