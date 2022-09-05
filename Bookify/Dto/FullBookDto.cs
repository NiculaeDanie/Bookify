namespace Bookify.Dto
{
    public class FullBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RealeseDate { get; set; }
        public bool IsFavorited { get; set; }
        public string ImageUrl { get; set;}

        public List<AuthorGetDto> Author { get; set; }
        public List<GenreGetDto> Genres { get; set; }
    }
}
