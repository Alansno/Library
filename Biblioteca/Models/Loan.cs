namespace Biblioteca.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime DateLoan { get; set; }
        public DateTime DateDevolution { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
