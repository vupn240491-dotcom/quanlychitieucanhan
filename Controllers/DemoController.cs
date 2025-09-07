using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class DemoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DemoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "vupn240491@tvu-onschool.edu.vn");
            if (user == null) return Content("User not found");

            var categories = await _context.Categories.Where(c => c.UserId == user.Id).ToListAsync();
            if (!categories.Any()) return Content("No categories found");

            // Xóa dữ liệu cũ
            var oldExpenses = await _context.Expenses.Where(e => e.UserId == user.Id).ToListAsync();
            _context.Expenses.RemoveRange(oldExpenses);

            // Tạo 10 chi tiêu demo
            var expenses = new List<Expense>
            {
                new() { Title = "Cơm trưa văn phòng", Amount = 45000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 7), Notes = "Cơm gà xối mỡ", UserId = user.Id },
                new() { Title = "Xăng xe máy", Amount = 120000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = new DateTime(2025, 9, 6), Notes = "Đổ đầy bình", UserId = user.Id },
                new() { Title = "Mua áo thun", Amount = 250000, CategoryId = categories.First(c => c.Name == "Mua sắm").Id, Date = new DateTime(2025, 9, 5), Notes = "Áo thun cotton", UserId = user.Id },
                new() { Title = "Xem phim rạp", Amount = 85000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = new DateTime(2025, 9, 4), Notes = "Phim hành động", UserId = user.Id },
                new() { Title = "Khám răng", Amount = 300000, CategoryId = categories.First(c => c.Name == "Sức khỏe").Id, Date = new DateTime(2025, 9, 3), Notes = "Lấy cao răng", UserId = user.Id },
                new() { Title = "Cà phê sáng", Amount = 25000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 7), Notes = "Cà phê đen đá", UserId = user.Id },
                new() { Title = "Grab về nhà", Amount = 35000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = new DateTime(2025, 9, 6), Notes = "Từ quận 1 về quận 7", UserId = user.Id },
                new() { Title = "Mua sách lập trình", Amount = 180000, CategoryId = categories.First(c => c.Name == "Giáo dục").Id, Date = new DateTime(2025, 9, 2), Notes = "Sách ASP.NET Core", UserId = user.Id },
                new() { Title = "Ăn tối nhà hàng", Amount = 320000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 1), Notes = "Nhà hàng Hàn Quốc", UserId = user.Id },
                new() { Title = "Game mobile", Amount = 50000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = new DateTime(2025, 8, 31), Notes = "Nạp thẻ game", UserId = user.Id },
                new() { Title = "Bánh mì sáng", Amount = 15000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 7), Notes = "Bánh mì thịt nướng", UserId = user.Id },
                new() { Title = "Bus đi làm", Amount = 8000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = new DateTime(2025, 9, 7), Notes = "Tuyến 01", UserId = user.Id },
                new() { Title = "Mua giày", Amount = 450000, CategoryId = categories.First(c => c.Name == "Mua sắm").Id, Date = new DateTime(2025, 9, 4), Notes = "Giày thể thao", UserId = user.Id },
                new() { Title = "Karaoke", Amount = 150000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = new DateTime(2025, 9, 3), Notes = "Với bạn bè", UserId = user.Id },
                new() { Title = "Thuốc cảm", Amount = 35000, CategoryId = categories.First(c => c.Name == "Sức khỏe").Id, Date = new DateTime(2025, 9, 2), Notes = "Panadol", UserId = user.Id }
            };

            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();

            return Content($"Đã tạo {expenses.Count} giao dịch demo cho tài khoản {user.Email}!");
        }
    }
}