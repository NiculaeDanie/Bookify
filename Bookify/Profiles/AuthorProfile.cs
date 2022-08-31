using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;

namespace Bookify.Profiles
{
    public class AuthorProfile: Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorPutPostDto, Author>();
            CreateMap<Author, AuthorGetDto>();
        }
    }
}
