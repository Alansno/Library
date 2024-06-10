using Biblioteca.Models.Dto;
using Biblioteca.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> saveBook([FromBody] BookDto bookDto)
        {
            try
            {
                var book = await _bookService.saveBook(bookDto);
                if (book == false) return BadRequest();

                return StatusCode(StatusCodes.Status201Created, new { message = "Libro agregado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create-loan")]
        public async Task<IActionResult> createLoan([FromBody] LoanDto loanDto)
        {
            try
            {
                var loan = await _bookService.createLoan(loanDto);
                if (loan == false) return BadRequest();

                return StatusCode(StatusCodes.Status201Created, new { message = "Préstamo realizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("get-third-genre")]
        public async Task<IActionResult> getThirdGenre()
        {
            try
            {
                var userGenre = await _bookService.findUserThreeGenre();

                return Ok(userGenre);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("get-books-more-30-days")]
        public async Task<IActionResult> getBooksMore30()
        {
            try
            {
                var bookMore = await _bookService.booksMore30Days();
                return Ok(bookMore);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("books-not-loans")]
        public async Task<IActionResult> booksNotLoans()
        {
            try
            {
                var booksNotLoans = await _bookService.booksNotLoans();
                return Ok(booksNotLoans);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("user-loans/{id}")]
        public async Task<IActionResult> userLoans([FromQuery] int id)
        {
            try
            {
                var userLoans = await _bookService.usersLoans(id);
                return Ok(userLoans);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("report-loans-month/{month}")]
        public async Task<IActionResult> reportLoans([FromQuery] int month)
        {
            try
            {
                var reportLoans = await _bookService.reportLoansGenreForMonth(month);
                return Ok(reportLoans);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
