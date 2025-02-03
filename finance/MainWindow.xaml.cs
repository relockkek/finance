using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace finance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DB Db { get; set; } = new DB();
        public DateTime StartDate { get; set; } =DateTime.Now.AddMonths(-1);
        public DateTime EndDate { get; set; } = DateTime.Now;
        public decimal Balance => Db.GetBalance();
        public string MainIncomeSource => Db.GetSource();
        public string ExpensiveCategory => Db.GetExpensiveCategory();
        public string PeriodSummary { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            Db.Incomes.Add(new Income { Date = DateTime.Now, Amount = 0, Source = Db.IncomeSources.FirstOrDefault()});
        }

        private void AddExpence_Click(object sender, RoutedEventArgs e)
        {
            Db.Incomes.Add(new Expenses { Date = DateTime.Now, Amount = 0, Category = Db.ExpenseCategories.FirstOrDefault() });
        }
    }
}