using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.DTOs;

namespace ProgrammersBlog.Mvc.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserAddDto>();
            CreateMap<UserLoginDto, User>();
        }
    }
}
