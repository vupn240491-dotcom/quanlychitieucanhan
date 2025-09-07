using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
        
        [Required]
        public string CategoryId { get; set; } = string.Empty;
        public Category Category { get; set; } = null!;
        
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}