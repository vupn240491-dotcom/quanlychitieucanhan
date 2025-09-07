using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            ViewBag.Error = "Email hoặc mật khẩu không đúng";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                Name = name
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                await SeedData.SeedDefaultCategories(_context, user.Id);
                return RedirectToAction("Index", "Dashboard");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> CreateDemo()
        {
            var user = await _userManager.FindByEmailAsync("vupn240491@tvu-onschool.edu.vn");
            if (user == null) return Content("User not found");

            var categories = await _context.Categories.Where(c => c.UserId == user.Id).ToListAsync();
            if (!categories.Any()) return Content("No categories");

            // Xóa dữ liệu cũ
            var old = _context.Expenses.Where(e => e.UserId == user.Id);
            _context.Expenses.RemoveRange(old);

            // Thêm demo
            var expenses = new[]
            {
                new Models.Expense { Title = "Cơm trưa", Amount = 45000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = DateTime.Today, UserId = user.Id },
                new Models.Expense { Title = "Xăng xe", Amount = 120000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = DateTime.Today.AddDays(-1), UserId = user.Id },
                new Models.Expense { Title = "Mua áo", Amount = 250000, CategoryId = categories.First(c => c.Name == "Mua sắm").Id, Date = DateTime.Today.AddDays(-2), UserId = user.Id },
                new Models.Expense { Title = "Xem phim", Amount = 85000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = DateTime.Today.AddDays(-3), UserId = user.Id },
                new Models.Expense { Title = "Khám bệnh", Amount = 300000, CategoryId = categories.First(c => c.Name == "Sức khỏe").Id, Date = DateTime.Today.AddDays(-4), UserId = user.Id },
                new Models.Expense { Title = "Cà phê", Amount = 25000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = DateTime.Today, UserId = user.Id },
                new Models.Expense { Title = "Taxi", Amount = 35000, CategoryId = categories.First(c => c.Name == "Di chuyển").Id, Date = DateTime.Today.AddDays(-1), UserId = user.Id },
                new Models.Expense { Title = "Sách", Amount = 180000, CategoryId = categories.First(c => c.Name == "Giáo dục").Id, Date = DateTime.Today.AddDays(-5), UserId = user.Id },
                new Models.Expense { Title = "Nhà hàng", Amount = 320000, CategoryId = categories.First(c => c.Name == "Ăn uống").Id, Date = DateTime.Today.AddDays(-6), UserId = user.Id },
                new Models.Expense { Title = "Game", Amount = 50000, CategoryId = categories.First(c => c.Name == "Giải trí").Id, Date = DateTime.Today.AddDays(-7), UserId = user.Id }
            };

            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();

            return Content($"Created {expenses.Length} demo expenses!");
        }
    }
}