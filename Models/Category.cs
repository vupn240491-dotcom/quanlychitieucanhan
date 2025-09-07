using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Icon { get; set; } = string.Empty;
        
        [Required]
        public string Color { get; set; } = string.Empty;
        
        public decimal? Budget { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}