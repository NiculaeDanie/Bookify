using System.ComponentModel.DataAnnotations;

namespace Bookify.Dto
{
    public class BookPostDto
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        public DateTime RealeaseDate { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile Content { get; set; }
        
        public IFormFile Image { get; set; }
    }
}
