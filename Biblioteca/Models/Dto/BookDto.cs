namespace Biblioteca.Models.Dto
{
    public class BookDto
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string genre { get; set; }
        public required int YearPub { get; set; }
    }
}
