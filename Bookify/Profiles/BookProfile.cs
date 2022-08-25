using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;

namespace Bookify.Profiles
{
    public class BookProfile: Profile
    {
        public BookProfile()
        {
            CreateMap<BookPutPostDto, Book>();
            CreateMap<Book, BookGetDto>();
        }
    }
}
