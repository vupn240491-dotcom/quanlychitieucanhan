using ExpenseTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Data
{
    public static class SeedData
    {
        public static async Task SeedDefaultCategories(ApplicationDbContext context, string userId)
        {
            if (!context.Categories.Any(c => c.UserId == userId))
            {
                var defaultCategories = new List<Category>
                {
                    new() { Name = "Ăn uống", Icon = "🍜", Color = "#EF4444", UserId = userId },
                    new() { Name = "Di chuyển", Icon = "🚗", Color = "#3B82F6", UserId = userId },
                    new() { Name = "Giải trí", Icon = "🎮", Color = "#10B981", UserId = userId },
                    new() { Name = "Mua sắm", Icon = "🛍️", Color = "#F59E0B", UserId = userId },
                    new() { Name = "Sức khỏe", Icon = "🏥", Color = "#EC4899", UserId = userId },
                    new() { Name = "Giáo dục", Icon = "📚", Color = "#8B5CF6", UserId = userId }
                };

                context.Categories.AddRange(defaultCategories);
                await context.SaveChangesAsync();
            }
        }
    }
}