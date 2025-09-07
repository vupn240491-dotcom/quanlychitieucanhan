using ExpenseTracker.Data;
using ExpenseTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            // Get expenses for current year
            var yearExpenses = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId && e.Date.Year == currentYear)
                .ToListAsync();

            // Monthly expenses for current year
            var monthlyExpenses = new List<MonthlyReportData>();
            for (int month = 1; month <= 12; month++)
            {
                var monthExpenses = yearExpenses.Where(e => e.Date.Month == month).Sum(e => e.Amount);
                monthlyExpenses.Add(new MonthlyReportData
                {
                    Month = new DateTime(currentYear, month, 1).ToString("MMM"),
                    Amount = monthExpenses
                });
            }

            // Category expenses for current month
            var currentMonthExpenses = yearExpenses.Where(e => e.Date.Month == currentMonth).ToList();
            var categoryExpenses = currentMonthExpenses
                .GroupBy(e => e.Category)
                .Select(g => new CategoryReportData
                {
                    CategoryName = g.Key.Name,
                    CategoryIcon = g.Key.Icon,
                    CategoryColor = g.Key.Color,
                    Amount = g.Sum(e => e.Amount),
                    Count = g.Count(),
                    Budget = g.Key.Budget
                })
                .OrderByDescending(c => c.Amount)
                .ToList();

            // Top expenses for current month
            var topExpenses = currentMonthExpenses
                .OrderByDescending(e => e.Amount)
                .Take(10)
                .Select(e => new TopExpenseData
                {
                    Title = e.Title,
                    Amount = e.Amount,
                    CategoryName = e.Category.Name,
                    CategoryIcon = e.Category.Icon,
                    CategoryColor = e.Category.Color,
                    Date = e.Date
                })
                .ToList();

            // Calculate totals
            var totalThisMonth = currentMonthExpenses.Sum(e => e.Amount);
            var totalThisYear = yearExpenses.Sum(e => e.Amount);
            var avgPerMonth = totalThisYear / currentMonth;

            // Previous month comparison
            var previousMonth = currentMonth == 1 ? 12 : currentMonth - 1;
            var previousMonthYear = currentMonth == 1 ? currentYear - 1 : currentYear;
            var previousMonthTotal = await _context.Expenses
                .Where(e => e.UserId == userId && e.Date.Year == previousMonthYear && e.Date.Month == previousMonth)
                .SumAsync(e => e.Amount);

            var monthlyChange = previousMonthTotal > 0 
                ? ((totalThisMonth - previousMonthTotal) / previousMonthTotal) * 100 
                : 0;

            var viewModel = new ReportsViewModel
            {
                MonthlyExpenses = monthlyExpenses,
                CategoryExpenses = categoryExpenses,
                TopExpenses = topExpenses,
                TotalThisMonth = totalThisMonth,
                TotalThisYear = totalThisYear,
                AveragePerMonth = avgPerMonth,
                MonthlyChange = monthlyChange,
                CurrentMonth = new DateTime(currentYear, currentMonth, 1).ToString("MMMM yyyy", new System.Globalization.CultureInfo("vi-VN")),
                CurrentYear = currentYear
            };

            return View(viewModel);
        }
    }
}