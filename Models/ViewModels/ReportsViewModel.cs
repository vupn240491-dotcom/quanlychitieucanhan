namespace ExpenseTracker.Models.ViewModels
{
    public class ReportsViewModel
    {
        public List<MonthlyReportData> MonthlyExpenses { get; set; } = new();
        public List<CategoryReportData> CategoryExpenses { get; set; } = new();
        public List<TopExpenseData> TopExpenses { get; set; } = new();
        public decimal TotalThisMonth { get; set; }
        public decimal TotalThisYear { get; set; }
        public decimal AveragePerMonth { get; set; }
        public decimal MonthlyChange { get; set; }
        public string CurrentMonth { get; set; } = string.Empty;
        public int CurrentYear { get; set; }
    }

    public class MonthlyReportData
    {
        public string Month { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    public class CategoryReportData
    {
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryIcon { get; set; } = string.Empty;
        public string CategoryColor { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int Count { get; set; }
        public decimal? Budget { get; set; }
        public decimal BudgetUsagePercentage => Budget.HasValue && Budget > 0 ? (Amount / Budget.Value) * 100 : 0;
    }

    public class TopExpenseData
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryIcon { get; set; } = string.Empty;
        public string CategoryColor { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}