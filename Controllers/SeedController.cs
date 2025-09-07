using ExpenseTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Demo()
        {
            await DemoDataSeeder.SeedDemoData(_context, "vupn240491@tvu-onschool.edu.vn");
            return Content("Đã tạo dữ liệu demo thành công!");
        }
    }
}