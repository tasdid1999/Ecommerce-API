using AutoMapper;
using ProductAPI.Model.Domain;
using ProductAPI.Model.DTO;

namespace ProductAPI.Model.Helper
{
    public class MapperHandler : Profile
    {
        public MapperHandler()
        {
            CreateMap<User, UserRegisterDTO>();
            CreateMap<UserRegisterDTO, User>();
        }
    }
}
