using ExpenseTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class DebugController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DebugController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CheckUser()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "vupn240491@tvu-onschool.edu.vn");
            if (user == null)
                return Content("Tài khoản không tồn tại!");

            var categories = await _context.Categories.Where(c => c.UserId == user.Id).ToListAsync();
            var expenses = await _context.Expenses.Where(e => e.UserId == user.Id).ToListAsync();

            return Content($"User ID: {user.Id}\nCategories: {categories.Count}\nExpenses: {expenses.Count}");
        }

        public async Task<IActionResult> CreateDemo()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "vupn240491@tvu-onschool.edu.vn");
            if (user == null)
                return Content("Tài khoản không tồn tại!");

            var categories = await _context.Categories.Where(c => c.UserId == user.Id).ToListAsync();
            
            var demoExpenses = new List<ExpenseTracker.Models.Expense>
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

            // Xóa dữ liệu cũ nếu có
            var oldExpenses = await _context.Expenses.Where(e => e.UserId == user.Id).ToListAsync();
            _context.Expenses.RemoveRange(oldExpenses);

            _context.Expenses.AddRange(demoExpenses);
            await _context.SaveChangesAsync();

            return Content($"Đã tạo {demoExpenses.Count} giao dịch demo cho tài khoản {user.Email}!");
        }
    }
}