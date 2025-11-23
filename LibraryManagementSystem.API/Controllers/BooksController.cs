using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.DTOs;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks(
                        [FromQuery] string? search,
                        [FromQuery] string? sortBy,
                        [FromQuery] bool sortDesc = false)
        {
            var books = await _bookService.GetAllBooksAsync(search, sortBy, sortDesc);
            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var book = await _bookService.CreateBookAsync(dto);
                return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the book.");
            }
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _bookService.UpdateBookAsync(id, dto);
                return updated == null ? NotFound() : Ok(updated);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the book.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var success = await _bookService.DeleteBookAsync(id);
                return success ? NoContent() : NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the book.");
            }
        }
    }
}
