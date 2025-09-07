using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class MonthlyBudget
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string Month { get; set; } = string.Empty;
        
        [Required]
        public int Year { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalBudget { get; set; }
        
        public string CategoryBudgets { get; set; } = "{}"; // JSON string
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
    }
}