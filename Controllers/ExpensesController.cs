using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();

            ViewBag.Categories = categories;
            return View(expenses);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, decimal amount, string categoryId, DateTime date, string? notes)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var expense = new Expense
            {
                Title = title,
                Amount = amount,
                CategoryId = categoryId,
                Date = date,
                Notes = notes,
                UserId = userId!
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string title, decimal amount, string categoryId, DateTime date, string? notes)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (expense != null)
            {
                expense.Title = title;
                expense.Amount = amount;
                expense.CategoryId = categoryId;
                expense.Date = date;
                expense.Notes = notes;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}