using System;
using System.Collections.Generic;
using System.Linq;

namespace finance
{
    public class DB
    {
        public List<Income> Incomes { get; set; } = new List<Income>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public List<string> IncomeSources { get; set; } = new List<string>();
        public List<string> ExpenseCategories { get; set; } = new List<string>();

        public decimal GetBalance()
        {
            decimal totalIncome = Incomes.Sum(i => i.Amount);
            decimal totalExpense = Expenses.Sum(e => e.Amount);
            return totalIncome - totalExpense;
        }

        public string GetMainIncomeSource()
        {
            return Incomes
                .GroupBy(i => i.Source)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "Неизвестно";
        }

        public string GetMostExpensiveCategory()
        {
            return Expenses
                .GroupBy(e => e.Category)
                .OrderByDescending(g => g.Sum(e => e.Amount))
                .FirstOrDefault()?.Key ?? "Неизвестно";
        }

        public (decimal TotalIncome, decimal TotalExpense, decimal Difference) GetPeriodSummary(DateTime startDate, DateTime endDate)
        {
            decimal totalIncome = Incomes
                .Where(i => i.Date >= startDate && i.Date <= endDate)
                .Sum(i => i.Amount);

            decimal totalExpense = Expenses
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .Sum(e => e.Amount);

            decimal difference = totalIncome - totalExpense;
            return (totalIncome, totalExpense, difference);
        }
    }
}