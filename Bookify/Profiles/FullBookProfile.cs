using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;

namespace Bookify.Profiles
{
    public class FullBookProfile : Profile
    {
        public FullBookProfile()
        {
            CreateMap<Book, FullBookDto>()
                .ForMember(dto => dto.Author, opt => opt.MapFrom(x => x.AuthorBook.Select(y => y.Author).ToList()))
                .ForMember(dto => dto.Genres, opt => opt.MapFrom(x => x.BookGenre.Select(y => y.Genre).ToList()));
        }
    }
}
