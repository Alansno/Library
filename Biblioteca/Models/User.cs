namespace Biblioteca.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public List<Book> Books { get; } = [];
        public List<Loan> Loans { get; } = [];
    }
}
