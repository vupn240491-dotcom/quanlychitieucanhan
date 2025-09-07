using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class QuickDemoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuickDemoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Add()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "vupn240491@tvu-onschool.edu.vn");
            if (user == null) return Content("User not found");

            var categories = await _context.Categories.Where(c => c.UserId == user.Id).ToListAsync();
            if (!categories.Any()) return Content("No categories");

            // Xóa dữ liệu cũ
            var old = _context.Expenses.Where(e => e.UserId == user.Id);
            _context.Expenses.RemoveRange(old);

            // Thêm mới
            var expenses = new[]
            {
                new Expense { Title = "Cơm trưa", Amount = 45000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = DateTime.Today, UserId = user.Id },
                new Expense { Title = "Xăng xe", Amount = 120000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = DateTime.Today.AddDays(-1), UserId = user.Id },
                new Expense { Title = "Mua áo", Amount = 250000, CategoryId = categories.First(c => c.Name == "Mua sắm").Id, Date = DateTime.Today.AddDays(-2), UserId = user.Id },
                new Expense { Title = "Xem phim", Amount = 85000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = DateTime.Today.AddDays(-3), UserId = user.Id },
                new Expense { Title = "Khám bệnh", Amount = 300000, CategoryId = categories.First(c => c.Name == "Sức khỏe").Id, Date = DateTime.Today.AddDays(-4), UserId = user.Id },
                new Expense { Title = "Cà phê", Amount = 25000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = DateTime.Today, UserId = user.Id },
                new Expense { Title = "Taxi", Amount = 35000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = DateTime.Today.AddDays(-1), UserId = user.Id },
                new Expense { Title = "Sách", Amount = 180000, CategoryId = categories.First(c => c.Name == "Giáo dục").Id, Date = DateTime.Today.AddDays(-5), UserId = user.Id },
                new Expense { Title = "Nhà hàng", Amount = 320000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = DateTime.Today.AddDays(-6), UserId = user.Id },
                new Expense { Title = "Game", Amount = 50000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = DateTime.Today.AddDays(-7), UserId = user.Id }
            };

            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();

            return Content($"Added {expenses.Length} demo expenses!");
        }
    }
}