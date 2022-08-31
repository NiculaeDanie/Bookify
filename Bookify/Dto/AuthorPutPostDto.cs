using System.ComponentModel.DataAnnotations;

namespace Bookify.Dto
{
    public class AuthorPutPostDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
