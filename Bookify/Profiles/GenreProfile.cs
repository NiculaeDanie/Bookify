using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;
using Domain;

namespace Bookify.Profiles
{
    public class GenreProfile: Profile
    {
        public GenreProfile()
        {
            CreateMap<GenrePutPostDto, Genre>();
            CreateMap<Genre, GenreGetDto>();
        }
    }
}
