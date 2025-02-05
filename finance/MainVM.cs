using System.ComponentModel;
using System.Windows;

namespace finance
{
    public class MainVM : INotifyPropertyChanged
    {
        public CommandVM AddIncome_Click { get; set; }
        public CommandVM AddExpense_Click { get; set; }
        public CommandVM CalculatePeriodSummary_Click { get; set; }
        public DB Db { get; set; } = new DB();
        public DateTime? StartDate { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime? EndDate { get; set; } = DateTime.Now;
        public decimal Balance => Db.GetBalance();
        public string MainIncomeSource => Db.GetMainIncomeSource();
        public string MostExpensiveCategory => Db.GetMostExpensiveCategory();
        private string _periodSummary;
        public string PeriodSummary
        {
            get => _periodSummary;
            set
            {
                _periodSummary = value;
                OnPropertyChanged(nameof(PeriodSummary));
            }
        }

        public MainVM()
        {
            Db = new DB();

            Db.IncomeSources.AddRange(new List<string>
            {
                "Зарплата",
                "Инвестиции",
                "Фриланс",
                "Продажа вещей"
            });


            Db.ExpenseCategories.AddRange(new List<string>
            {
                "Еда",
                "Транспорт",
                "Одежда",
                "Развлечения",
                "Коммунальные услуги"
            });

            AddIncome_Click = new CommandVM(() =>
            {
                Db.Incomes.Add(new Income
                {
                    Date = DateTime.Now,
                    Amount = 0,
                    Source = Db.IncomeSources.FirstOrDefault()
                });
                OnPropertyChanged(nameof(Balance));
                OnPropertyChanged(nameof(MainIncomeSource));
            });
            AddExpense_Click = new CommandVM(() =>
            {
              
                Db.Expenses.Add(new Expense
                {
                    Date = DateTime.Now,
                    Amount = 0,
                    Category = Db.ExpenseCategories.FirstOrDefault()
                
                });
                
                OnPropertyChanged(nameof(Balance));
                OnPropertyChanged(nameof(MostExpensiveCategory));
            });
            CalculatePeriodSummary_Click = new CommandVM(() =>
            {
                if (StartDate == null || EndDate == null)
                {
                    MessageBox.Show("Пожалуйста, выберите начальную и конечную даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var summary = Db.GetPeriodSummary(StartDate.Value, EndDate.Value);
                PeriodSummary = ($"Сумма доходов: {summary.TotalIncome:C}, расходов: {summary.TotalExpense:C}, разница: {summary.Difference:C}");
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}