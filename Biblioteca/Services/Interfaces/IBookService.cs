using Biblioteca.Models.Dto;

namespace Biblioteca.Services.Interfaces
{
    public interface IBookService
    {
        public Task<bool> saveBook(BookDto bookDto);
        public Task<bool> createLoan(LoanDto loanDto);
        public Task<List<object>> findUserThreeGenre();
        public Task<List<object>> booksMore30Days();
        public Task<List<object>> booksNotLoans();
        public Task<List<object>> usersLoans(int userId);
        public Task<List<object>> reportLoansGenreForMonth(int month);
    }
}
