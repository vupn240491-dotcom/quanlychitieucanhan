using ExpenseTracker.Data;
using ExpenseTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endCurrentMonth = currentMonth.AddMonths(1).AddDays(-1);
            var lastWeek = DateTime.Now.AddDays(-7);

            var currentMonthExpenses = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId && e.Date >= currentMonth && e.Date <= endCurrentMonth)
                .ToListAsync();

            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();

            var totalExpenses = currentMonthExpenses.Sum(e => e.Amount);
            var transactionCount = currentMonthExpenses.Count;

            // Weekly change calculation
            var lastWeekExpenses = await _context.Expenses
                .Where(e => e.UserId == userId && e.Date >= lastWeek)
                .SumAsync(e => e.Amount);

            var previousWeekExpenses = await _context.Expenses
                .Where(e => e.UserId == userId && e.Date >= lastWeek.AddDays(-7) && e.Date < lastWeek)
                .SumAsync(e => e.Amount);

            var weeklyChange = previousWeekExpenses > 0 
                ? ((lastWeekExpenses - previousWeekExpenses) / previousWeekExpenses) * 100 
                : 0;

            // Category distribution
            var categoryDistribution = categories.Select(cat => new CategoryDistributionData
            {
                Name = cat.Name,
                Value = currentMonthExpenses.Where(e => e.CategoryId == cat.Id).Sum(e => e.Amount),
                Color = cat.Color
            }).Where(cd => cd.Value > 0).ToList();

            var topCategory = categoryDistribution.OrderByDescending(cd => cd.Value).FirstOrDefault()?.Name ?? "";

            // Monthly spending trend
            var monthlySpending = new List<MonthlySpendingData>();
            for (var date = currentMonth; date <= endCurrentMonth; date = date.AddDays(1))
            {
                var dayExpenses = currentMonthExpenses.Where(e => e.Date.Date == date.Date).Sum(e => e.Amount);
                monthlySpending.Add(new MonthlySpendingData
                {
                    Date = date.ToString("dd/MM"),
                    Amount = dayExpenses
                });
            }

            var viewModel = new DashboardViewModel
            {
                TotalExpenses = totalExpenses,
                TransactionCount = transactionCount,
                TopCategory = topCategory,
                WeeklyChange = weeklyChange,
                MonthlySpending = monthlySpending,
                CategoryDistribution = categoryDistribution,
                CategoryCount = categories.Count,
                DailyAverage = totalExpenses / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)
            };

            return View(viewModel);
        }
    }
}