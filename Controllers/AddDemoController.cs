using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class AddDemoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddDemoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "vupn240491@tvu-onschool.edu.vn");
            if (user == null) return Content("Tài khoản không tồn tại");

            var categories = await _context.Categories.Where(c => c.UserId == user.Id).ToListAsync();
            if (!categories.Any()) return Content("Không có danh mục");

            // Xóa chi tiêu cũ
            var oldExpenses = await _context.Expenses.Where(e => e.UserId == user.Id).ToListAsync();
            _context.Expenses.RemoveRange(oldExpenses);

            // Thêm 10 chi tiêu demo
            var expenses = new List<Expense>
            {
                new() { Title = "Cơm trưa", Amount = 45000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 7), UserId = user.Id },
                new() { Title = "Xăng xe", Amount = 120000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = new DateTime(2025, 9, 6), UserId = user.Id },
                new() { Title = "Mua áo", Amount = 250000, CategoryId = categories.First(c => c.Name == "Mua sắm").Id, Date = new DateTime(2025, 9, 5), UserId = user.Id },
                new() { Title = "Xem phim", Amount = 85000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = new DateTime(2025, 9, 4), UserId = user.Id },
                new() { Title = "Khám bệnh", Amount = 300000, CategoryId = categories.First(c => c.Name == "Sức khỏe").Id, Date = new DateTime(2025, 9, 3), UserId = user.Id },
                new() { Title = "Cà phê", Amount = 25000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 7), UserId = user.Id },
                new() { Title = "Taxi", Amount = 35000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = new DateTime(2025, 9, 6), UserId = user.Id },
                new() { Title = "Sách", Amount = 180000, CategoryId = categories.First(c => c.Name == "Giáo dục").Id, Date = new DateTime(2025, 9, 2), UserId = user.Id },
                new() { Title = "Nhà hàng", Amount = 320000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = new DateTime(2025, 9, 1), UserId = user.Id },
                new() { Title = "Game", Amount = 50000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = new DateTime(2025, 8, 31), UserId = user.Id }
            };

            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();

            return Content($"Đã thêm {expenses.Count} giao dịch demo vào tài khoản vupn240491@tvu-onschool.edu.vn");
        }
    }
}