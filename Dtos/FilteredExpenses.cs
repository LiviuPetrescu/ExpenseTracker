namespace ExpenseTracker.DTOs
{
    public class FilteredExpensesResult
    {
        public IEnumerable<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
    }
}
