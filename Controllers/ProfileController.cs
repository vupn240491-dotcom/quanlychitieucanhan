using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);
            
            if (user == null)
                return NotFound();

            // Get user statistics
            var totalExpenses = await _context.Expenses
                .Where(e => e.UserId == userId)
                .SumAsync(e => e.Amount);

            var totalTransactions = await _context.Expenses
                .Where(e => e.UserId == userId)
                .CountAsync();

            var categoriesCount = await _context.Categories
                .Where(c => c.UserId == userId)
                .CountAsync();

            var firstExpenseDate = await _context.Expenses
                .Where(e => e.UserId == userId)
                .OrderBy(e => e.Date)
                .Select(e => e.Date)
                .FirstOrDefaultAsync();

            ViewBag.TotalExpenses = totalExpenses;
            ViewBag.TotalTransactions = totalTransactions;
            ViewBag.CategoriesCount = categoriesCount;
            ViewBag.FirstExpenseDate = firstExpenseDate;
            ViewBag.DaysUsing = firstExpenseDate != default ? (DateTime.Now - firstExpenseDate).Days : 0;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string name, string email)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);
            
            if (user == null)
                return NotFound();

            user.Name = name;
            user.Email = email;
            user.UserName = email;

            var result = await _userManager.UpdateAsync(user);
            
            if (result.Succeeded)
            {
                ViewBag.Success = "Cập nhật thông tin thành công!";
            }
            else
            {
                ViewBag.Error = "Có lỗi xảy ra khi cập nhật thông tin.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);
            
            if (user == null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            
            if (result.Succeeded)
            {
                ViewBag.Success = "Đổi mật khẩu thành công!";
            }
            else
            {
                ViewBag.Error = "Mật khẩu hiện tại không đúng hoặc mật khẩu mới không hợp lệ.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}