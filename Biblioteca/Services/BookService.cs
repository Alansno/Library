using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Models.Dto;
using Biblioteca.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Biblioteca.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _libraryContext;
        private readonly string _connectionString;

        public BookService(LibraryContext libraryContext, IConfiguration configuration)
        {
            _libraryContext = libraryContext;
            _connectionString = configuration.GetConnectionString("Connection");
        }

        public async Task<List<object>> booksMore30Days()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT l.BookId, b.Title FROM Loans l JOIN Books b ON l.BookId = b.Id WHERE DATEDIFF(day, l.DateLoan, l.DateDevolution) > 30";
                    var result = await connection.QueryAsync(sql);

                    if (!result.Any()) return new List<object>();
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<object>> booksNotLoans()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "WITH BooksNotLoans AS ( SELECT b.Id FROM Books b EXCEPT SELECT l.BookId FROM Loans l ) SELECT b.Title FROM Books b WHERE b.Id IN ( SELECT Id FROM BooksNotLoans )";
                    var result = await connection.QueryAsync(sql);

                    if (!result.Any()) return new List<object>();
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> createLoan(LoanDto loanDto)
        {
            var newLoan = new Loan
            {
                DateLoan = loanDto.DateLoan,
                DateDevolution = loanDto.DateDevolution,
                UserId = loanDto.UserId,
                BookId = loanDto.BookId,
            };

            try
            {
                await _libraryContext.Loans.AddAsync(newLoan);
                await _libraryContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<object>> findUserThreeGenre()
        {
            try
            {
                var userGenre = await (from l in _libraryContext.Loans
                                       join u in _libraryContext.Users on l.UserId equals u.Id
                                       join b in _libraryContext.Books on l.BookId equals b.Id
                                       group new { l, u, b } by l.UserId
                                       into g
                                       where g.Select(loan => loan.b.genre).Distinct().Count() >= 3
                                       select new
                                       {
                                           UserId = g.Key,
                                           Name = g.Select(x => x.u.Name)
                                       .FirstOrDefault()
                                       }).ToListAsync();


                if (!userGenre.Any()) return new List<object>();

                return userGenre.Cast<object>().ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<object>> reportLoansGenreForMonth(int month)
        {
            if (month <= 0 || month > 12) throw new ArgumentException("El mes debe estar entre 1 y 12");
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT DISTINCT b.genre, COUNT(l.BookId) as totalLoans FROM Loans l JOIN Books b ON l.BookId = b.Id WHERE MONTH(l.DateLoan) = @month GROUP BY b.genre";
                    var result = await connection.QueryAsync(sql, new { month });

                    if (!result.Any()) return new List<object>();
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> saveBook(BookDto bookDto)
        {
            var newBook = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                genre = bookDto.genre,
                YearPub = bookDto.YearPub
            };

            try
            {

                await _libraryContext.Books.AddAsync(newBook);
                await _libraryContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<object>> usersLoans(int userId)
        {
            try
            {
                var userLoans = await (from l in _libraryContext.Loans
                                       join u in _libraryContext.Users on l.UserId equals u.Id
                                       join b in _libraryContext.Books on l.BookId equals b.Id
                                       where u.Id == userId
                                       select new
                                       {
                                           nameUser = u.Name,
                                           bookTitle = b.Title
                                       }
                                       ).ToListAsync();

                if (!userLoans.Any()) return new List<object>();
                return userLoans.Cast<object>().ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
