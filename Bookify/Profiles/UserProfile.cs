using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;
using Domain;

namespace Bookify.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserPutPostDto, User>();
            CreateMap<User, LoginDto>();
        }
    }
}
