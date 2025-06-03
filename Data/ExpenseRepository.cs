using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using ExpenseTracker.Models;
using ExpenseTracker.DTOs;
namespace ExpenseTracker.Data
{
    public class ExpenseRepository
    {
        private readonly IConfiguration _configuration;

        public ExpenseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<FilteredExpensesResult> GetFilteredExpensesAsync(
    string? category, DateTime? startDate, DateTime? endDate,
    int pageNumber = 1, int pageSize = 15)
{
    using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

    var parameters = new DynamicParameters();
    parameters.Add("@Category", category);
    parameters.Add("@StartDate", startDate);
    parameters.Add("@EndDate", endDate);
    parameters.Add("@PageNumber", pageNumber);
    parameters.Add("@PageSize", pageSize);

    using var multi = await connection.QueryMultipleAsync(
        "GetExpensesByDateFilter",
        parameters,
        commandType: CommandType.StoredProcedure
    );

    var expenses = (await multi.ReadAsync<Expense>()).ToList();
    var totalAmount = await multi.ReadFirstOrDefaultAsync<decimal?>() ?? 0;
    var totalCount = await multi.ReadFirstOrDefaultAsync<int?>() ?? 0;

    var expenseDtos = expenses.Select(e => new ExpenseDto
    {
        Id = e.Id,
        Title = e.Title,
        Amount = e.Amount,
        Date = e.Date.ToString("dd MMM yyyy"),
        Category = e.Category,
        Notes = e.Notes
    });

    return new FilteredExpensesResult
    {
        Expenses = expenseDtos,
        TotalAmount = totalAmount,
        TotalCount = totalCount
    };
}






        public async Task AddExpenseAsync(Expense expense)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                expense.Title,
                expense.Amount,
                expense.Date,
                expense.Category,
                expense.Notes
            };

            await connection.ExecuteAsync("AddExpense", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("UpdateExpense", new
            {
                expense.Id,
                expense.Title,
                expense.Amount,
                expense.Date,
                expense.Category,
                expense.Notes
            }, commandType: CommandType.StoredProcedure);
        }
        public async Task DeleteExpenseAsync(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DeleteExpense", new { Id = id }, commandType: CommandType.StoredProcedure);

        }
    };



}
