using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Helpers;

namespace ExpenseTracker.Models.ViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public List<string> Categories { get; set; } = ExpenseCategories.All;
    }
}
