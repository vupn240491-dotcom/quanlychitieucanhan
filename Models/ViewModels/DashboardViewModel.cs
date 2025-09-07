namespace ExpenseTracker.Models.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalExpenses { get; set; }
        public int TransactionCount { get; set; }
        public string TopCategory { get; set; } = string.Empty;
        public decimal WeeklyChange { get; set; }
        public List<MonthlySpendingData> MonthlySpending { get; set; } = new();
        public List<CategoryDistributionData> CategoryDistribution { get; set; } = new();
        public int CategoryCount { get; set; }
        public decimal DailyAverage { get; set; }
    }

    public class MonthlySpendingData
    {
        public string Date { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    public class CategoryDistributionData
    {
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}