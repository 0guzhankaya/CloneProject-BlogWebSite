using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Mvc.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserAddDto, User>(); // UserAddDto --> User'a dönüşür.
            CreateMap<User, UserUpdateDto>(); // User --> UserUpdateDto'ya dönüşür.
            CreateMap<UserUpdateDto, User>(); // UserUpdateDto --> User'a dönüşür.
        }
    }
}
