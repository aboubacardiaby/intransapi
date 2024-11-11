using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialTransactionApi.Features.Authentication.Keys;
using FinancialTransactionApi.Models;
using FinancialTransactionApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransactionApi.Controllers
{
    /// <summary>
    /// FinancialTransactionsController
    /// </summary>

    [ApiKey]
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IFinancialTransactionRepository _repository;

        public FinancialTransactionsController(IFinancialTransactionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GetAllTransactions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialTransaction>>> GetAllTransactions()
        {
            var transactions = await _repository.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialTransaction>> GetTransactionById(int id)
        {
            var transaction = await _repository.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<FinancialTransaction>> CreateTransaction(FinancialTransaction transaction)
        {
            var createdTransaction = await _repository.CreateTransactionAsync(transaction);
            return CreatedAtAction(nameof(GetTransactionById), new { id = createdTransaction.Id }, createdTransaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, FinancialTransaction transaction)
        {
            if (id != transaction.Id)
                return BadRequest();

            var updatedTransaction = await _repository.UpdateTransactionAsync(transaction);
            if (updatedTransaction == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var result = await _repository.DeleteTransactionAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}