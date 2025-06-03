using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Helpers;
using ExpenseTracker.DTOs;
using System.Globalization;
using ExpenseTracker.ViewModels;





namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseRepository _repository;

        public ExpensesController(ExpenseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            return Ok(ExpenseCategories.All);
        }


        [HttpGet]
    public async Task<IActionResult> GetFilteredExpenses(
    [FromQuery] string? category = null,
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 15)
{
    try
    {
        var result = await _repository.GetFilteredExpensesAsync(category, startDate, endDate, pageNumber, pageSize);

        var dtoResult = result.Expenses.Select(e => new ExpenseDto
        {
            Id = e.Id,
            Title = e.Title,
            Amount = e.Amount,
            Date = DateTime.Parse(e.Date).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
            Category = e.Category,
            Notes = e.Notes
        });

        return Ok(new
        {
            TotalAmount = result.TotalAmount,
            TotalCount = result.TotalCount,
            Expenses = dtoResult
        });
    }
    catch (Exception ex)
    {
        return BadRequest($"Failed to retrieve expenses: {ex.Message}");
    }
}


        
   
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExpenseInputViewModel expenseInput)
        {
            try
            {
                // Map ViewModel to Expense entity
                var expense = new Expense
                {
                    Title = expenseInput.Title,
                    Amount = expenseInput.Amount,
                    Date = expenseInput.Date,
                    Category = expenseInput.Category,
                    Notes = expenseInput.Notes ?? string.Empty
                };

                await _repository.AddExpenseAsync(expense);
                return Ok("Expense added successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Adding a new expense was not successful: {ex.Message}");
            }
        }

        
  

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] Expense expense)
        {
            if (id != expense.Id)
                return BadRequest("Expense Id mismatch.");

            try
            {
                await _repository.UpdateExpenseAsync(expense);
                return Ok("Expense updated successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Updating the expense failed: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            try
            {
                await _repository.DeleteExpenseAsync(id);
                return Ok("Expense deleted successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Deleting the expense failed: {ex.Message}");
            }
        }


    }
}
