using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, BaseUserDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
