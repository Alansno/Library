namespace Biblioteca.Models.Dto
{
    public class LoanDto
    {
        public required DateTime DateLoan { get; set; }
        public required DateTime DateDevolution { get; set; }
        public required int UserId { get; set; }
        public required int BookId { get; set; }
    }
}
