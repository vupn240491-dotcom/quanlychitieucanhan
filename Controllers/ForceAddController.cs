using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class ForceAddController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForceAddController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Now()
        {
            // Tìm user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == "vupn240491@tvu-onschool.edu.vn");
            if (user == null) 
            {
                return Content("KHÔNG TÌM THẤY USER");
            }

            // Tìm categories
            var categories = await _context.Categories.Where(c => c.UserId == user.Id).ToListAsync();
            if (!categories.Any()) 
            {
                return Content("KHÔNG CÓ CATEGORIES");
            }

            // Xóa tất cả expenses cũ
            var allExpenses = await _context.Expenses.Where(e => e.UserId == user.Id).ToListAsync();
            if (allExpenses.Any())
            {
                _context.Expenses.RemoveRange(allExpenses);
                await _context.SaveChangesAsync();
            }

            // Thêm expenses mới
            var newExpenses = new List<Expense>();
            
            var anUong = categories.FirstOrDefault(c => c.Name == "Ăn uống");
            var diChuyen = categories.FirstOrDefault(c => c.Name == "Di chuyển");
            var muaSam = categories.FirstOrDefault(c => c.Name == "Mua sắm");
            var giaiTri = categories.FirstOrDefault(c => c.Name == "Giải trí");
            var sucKhoe = categories.FirstOrDefault(c => c.Name == "Sức khỏe");
            var giaoDuc = categories.FirstOrDefault(c => c.Name == "Giáo dục");

            if (anUong != null)
            {
                newExpenses.Add(new Expense { Title = "Cơm trưa", Amount = 45000, CategoryId = anUong.Id, Date = new DateTime(2025, 9, 7), UserId = user.Id });
                newExpenses.Add(new Expense { Title = "Cà phê", Amount = 25000, CategoryId = anUong.Id, Date = new DateTime(2025, 9, 7), UserId = user.Id });
                newExpenses.Add(new Expense { Title = "Nhà hàng", Amount = 320000, CategoryId = anUong.Id, Date = new DateTime(2025, 9, 1), UserId = user.Id });
            }

            if (diChuyen != null)
            {
                newExpenses.Add(new Expense { Title = "Xăng xe", Amount = 120000, CategoryId = diChuyen.Id, Date = new DateTime(2025, 9, 6), UserId = user.Id });
                newExpenses.Add(new Expense { Title = "Taxi", Amount = 35000, CategoryId = diChuyen.Id, Date = new DateTime(2025, 9, 6), UserId = user.Id });
            }

            if (muaSam != null)
            {
                newExpenses.Add(new Expense { Title = "Mua áo", Amount = 250000, CategoryId = muaSam.Id, Date = new DateTime(2025, 9, 5), UserId = user.Id });
            }

            if (giaiTri != null)
            {
                newExpenses.Add(new Expense { Title = "Xem phim", Amount = 85000, CategoryId = giaiTri.Id, Date = new DateTime(2025, 9, 4), UserId = user.Id });
                newExpenses.Add(new Expense { Title = "Game", Amount = 50000, CategoryId = giaiTri.Id, Date = new DateTime(2025, 8, 31), UserId = user.Id });
            }

            if (sucKhoe != null)
            {
                newExpenses.Add(new Expense { Title = "Khám bệnh", Amount = 300000, CategoryId = sucKhoe.Id, Date = new DateTime(2025, 9, 3), UserId = user.Id });
            }

            if (giaoDuc != null)
            {
                newExpenses.Add(new Expense { Title = "Sách", Amount = 180000, CategoryId = giaoDuc.Id, Date = new DateTime(2025, 9, 2), UserId = user.Id });
            }

            _context.Expenses.AddRange(newExpenses);
            await _context.SaveChangesAsync();

            return Content($"ĐÃ THÊM {newExpenses.Count} GIAO DỊCH CHO USER {user.Email}");
        }
    }
}