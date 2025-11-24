using LibraryManagementSystem.API.Services;
using LibraryManagementSystem.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Core.Interfaces;
namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] CreateTransactionDto dto)
        {
            var result = await _service.BorrowBookAsync(dto);
            return Ok(result);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnTransactionDto dto)
        {
            var result = await _service.ReturnBookAsync(dto);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }

}
