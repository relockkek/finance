using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance
{
    public class DB
    {
        public List<Income> Incomes { get; set; } = new List<Income>();
        public List<Expenses> Expenses { get; set; } = new List<Expenses>();
        public List<string> IncomeSources { get; set; } = new List<string>();
        public List<string> ExpensesCategories { get; set; } = new List<string>();

        public decimal GetBalance()
        {
            decimal totalIncome = Incomes.Sum(i => i.Amount);
            decimal totalExpenses = Expenses.Sum(e => e.Amount);
            return totalIncome - totalExpenses;
        }

        public string GetSource()
        {
            return Incomes.GroupBy(i => i.Source)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key;
        }

        public string GetExpensiveCategory()
        {
            return Expenses.GroupBy(e => e.Category)
               .OrderByDescending(g => g.Count())
               .FirstOrDefault()?.Key;
        }

        public (decimal TotalIncome, decimal TotalExpense, decimal Difference) GetPeriodSummary(DateTime startDate, DateTime endDate)
        {
            decimal totalIncome = Incomes.Where(i => i.Date >= startDate && i.Date <= endDate).Sum(i => i.Amount);
            decimal totalExpense = Expenses.Where(e => e.Date >= startDate && e.Date <= endDate).Sum(e => e.Amount);
            decimal difference = totalIncome - totalExpense;
            return (totalIncome, totalExpense, difference);
        }
    }
}

