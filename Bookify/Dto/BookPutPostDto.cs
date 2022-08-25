using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Bookify.Dto
{
    public class BookPutPostDto
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        public DateTime RealeaseDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Content { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}
