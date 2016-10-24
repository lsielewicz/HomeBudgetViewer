using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Models.Enum;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.SummaryPage
{
    public class SummaryPageViewModel : DateOperationViewModelBase
    {
        private double _averageExpenses;
        private double _averageIncomes;
        private double _averageBalance;
        private bool _isChartVisibile;

        public SummaryPageViewModel()
        {
            ChartCollection = new List<KeyValuePair<string, int>>();
        }

        public List<KeyValuePair<string,int>> ChartCollection { get; set; }

        public bool IsChartVisible
        {
            get
            {
                return _isChartVisibile;
            }
            set
            {
                if (_isChartVisibile == value)
                    return;
                _isChartVisibile = value;
                this.RaisePropertyChanged();
            }
        }

        public double AverageExpenses
        {
            get
            {
                return _averageExpenses;
            }
            set
            {
                _averageExpenses = value;
                this.RaisePropertyChanged();
            }
        }

        public double AverageIncomes
        {
            get
            {
                return _averageIncomes;
            }
            set
            {
                _averageIncomes = value;
                this.RaisePropertyChanged();
            }
        }

        public double AverageBalance
        {
            get
            {
                return _averageBalance;
            }
            set
            {
                _averageBalance = value;
                this.RaisePropertyChanged();
            }
        }

        public override void GetData()
        {
            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
            {
                int currentUserId = SettingsService.Instance.CurrentUser.Id;
                double sumExpenses = unitOfWork.BudgetItems.GetSumOfItems(currentUserId, this.CurrentDateTime, ItemType.Expense, this.CurrentDateFilter);
                int countExpenses = unitOfWork.BudgetItems.GetCountOfItems(currentUserId, this.CurrentDateTime, ItemType.Expense, this.CurrentDateFilter);
                double sumIncomes = unitOfWork.BudgetItems.GetSumOfItems(currentUserId, this.CurrentDateTime, ItemType.Income, this.CurrentDateFilter);

                int countIncomes = unitOfWork.BudgetItems.GetCountOfItems(currentUserId, this.CurrentDateTime, ItemType.Income, this.CurrentDateFilter);
                this.AverageExpenses = !double.IsNaN((sumExpenses / countExpenses)) ? (sumExpenses/countExpenses) : 0;
                this.AverageIncomes = !double.IsNaN((sumIncomes / countIncomes)) ? (sumIncomes / countIncomes) : 0;
                this.AverageBalance = sumIncomes - sumExpenses;

                this.ChartCollection = new List<KeyValuePair<string, int>>()
                {
                    new KeyValuePair<string, int>(this.GetLocalizedString("IncomesT"), Convert.ToInt32(sumIncomes)),
                    new KeyValuePair<string, int>(this.GetLocalizedString("ExpensesT"),Convert.ToInt32(sumExpenses))
                };
                this.IsChartVisible = Math.Abs(sumIncomes) > 1 || Math.Abs(sumExpenses) > 1;

                this.RaisePropertyChanged("ChartCollection");
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            this.CurrentDateFilter = DateFilter.None;
            this.RaisePropertyChanged("CurrentDateFilter");
            this.RaisePropertyChanged("CurrentDateHeader");
            this.RaisePropertyChanged("CurrentCurrencyString");
            this.GetData();
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

    }
}
