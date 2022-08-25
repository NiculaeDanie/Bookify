using System.ComponentModel.DataAnnotations;

namespace Bookify.Dto
{
    public class GenrePutPostDto
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }
    }
}
