using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public static class DemoDataSeeder
    {
        public static async Task SeedDemoData(ApplicationDbContext context, string userEmail)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return;

            var categories = await context.Categories.Where(c => c.UserId == user.Id).ToListAsync();
            if (!categories.Any()) return;

            var demoExpenses = new List<Expense>
            {
                new() { Title = "Cơm trưa văn phòng", Amount = 45000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 7, 12, 30, 0), Notes = "Cơm gà xối mỡ", UserId = user.Id },
                new() { Title = "Xăng xe máy", Amount = 120000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = new DateTime(2025, 9, 6, 8, 15, 0), Notes = "Đổ đầy bình", UserId = user.Id },
                new() { Title = "Mua áo thun", Amount = 250000, CategoryId = categories.First(c => c.Name == "Mua sắm").Id, Date = new DateTime(2025, 9, 5, 19, 20, 0), Notes = "Áo thun cotton", UserId = user.Id },
                new() { Title = "Xem phim rạp", Amount = 85000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = new DateTime(2025, 9, 4, 20, 0, 0), Notes = "Phim hành động", UserId = user.Id },
                new() { Title = "Khám răng", Amount = 300000, CategoryId = categories.First(c => c.Name == "Sức khỏe").Id, Date = new DateTime(2025, 9, 3, 14, 30, 0), Notes = "Lấy cao răng", UserId = user.Id },
                new() { Title = "Cà phê sáng", Amount = 25000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 7, 7, 45, 0), Notes = "Cà phê đen đá", UserId = user.Id },
                new() { Title = "Grab về nhà", Amount = 35000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = new DateTime(2025, 9, 6, 22, 30, 0), Notes = "Từ quận 1 về quận 7", UserId = user.Id },
                new() { Title = "Mua sách lập trình", Amount = 180000, CategoryId = categories.First(c => c.Name == "Giáo dục").Id, Date = new DateTime(2025, 9, 2, 16, 0, 0), Notes = "Sách ASP.NET Core", UserId = user.Id },
                new() { Title = "Ăn tối nhà hàng", Amount = 320000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 1, 19, 30, 0), Notes = "Nhà hàng Hàn Quốc", UserId = user.Id },
                new() { Title = "Game mobile", Amount = 50000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = new DateTime(2025, 8, 31, 21, 0, 0), Notes = "Nạp thẻ game", UserId = user.Id }
            };

            // Kiểm tra xem đã có dữ liệu demo chưa
            var existingExpenses = await context.Expenses.Where(e => e.UserId == user.Id).CountAsync();
            if (existingExpenses == 0)
            {
                context.Expenses.AddRange(demoExpenses);
                await context.SaveChangesAsync();
            }
        }
    }
}