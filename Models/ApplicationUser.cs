using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<MonthlyBudget> MonthlyBudgets { get; set; } = new List<MonthlyBudget>();
    }
}