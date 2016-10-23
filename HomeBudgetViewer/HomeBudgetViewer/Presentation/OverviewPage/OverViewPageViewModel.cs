using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Currency;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Messages;
using HomeBudgetViewer.Models.Enum;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.OverviewPage
{
    public class OverviewPageViewModel : AppViewModelBase
    {
        private delegate List<BudgetItem> GetDataByDate(int id, DateTime date);

        private ObservableCollection<BudgetItem> _currentItems;
        private RelayCommand<object> _switchMonthCommand;
        private RelayCommand<object> _switchItemTypeCommand;
        private RelayCommand<object> _switchCurrentFilteringCommand;
        private BudgetItem _selectedBudgetItem;
        private ItemType _budgetItemType;
        public int CurrentMonthIndex { get; private set; }
        public int CurrentDayIndex { get; private set; }
        public DateTime CurrentDateTime { get; private set; }
        public DateFilter CurrentDateFilter { get; private set; }

        public OverviewPageViewModel()
        {
            this.CurrentDateTime = DateTime.Now.Date;
            this.CurrentMonthIndex = 0;
            this.CurrentDayIndex = 0;
        }

        public BudgetItem SelectedBudgetItem
        {
            get
            {
                return _selectedBudgetItem;
            }
            set
            {
                _selectedBudgetItem = value;
                this.OnSelectedBudgetItemChanged();
            }
        }

        private void OnSelectedBudgetItemChanged()
        {
            if (this.SelectedBudgetItem == null)
                return;
            this.NavigationService.Navigate(typeof(BudgetItemPage.BudgetItemPage),true);
            MessengerInstance.Send<IsModifyingStateToBudgetItemMessage>(new IsModifyingStateToBudgetItemMessage(this.SelectedBudgetItem));
        }
        public string CurrentDateHeader
        {
            get
            {
                switch (this.CurrentDateFilter)
                {
                    case DateFilter.ByMonth:
                        return $"{this.GetLocalizedMonth(this.CurrentDateTime.Month)} {this.CurrentDateTime.Year}";
                    case DateFilter.ByDay:
                        return $"{this.CurrentDateTime.ToString("D")}";
                }
                return string.Empty;
            }
        }

        public string CurrentCurrencyString
        {
            get
            {
                var currencySymbol =
                    CurrencyModel.PossibleCurrencies.FirstOrDefault(
                        c => c.CurrencyName == SettingsService.Instance.CurrentUser.Currency).CurrencySymbol;
                return currencySymbol ?? "$";
            }
        }

        public ItemType BudgetItemType
        {
            get { return _budgetItemType; }
            set
            {
                if (_budgetItemType == value)
                    return;

                _budgetItemType = value;
                RaisePropertyChanged();
            }
        }


        private void GetData()
        {
            GetDataByDate getDataMethod = null;
            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
            {
                List<BudgetItem> items=null;
                if(this.BudgetItemType == ItemType.Expense && this.CurrentDateFilter == DateFilter.ByMonth)
                    getDataMethod = unitOfWork.BudgetItems.GetAllExpensesOfUserByMonth;
                else if(this.BudgetItemType == ItemType.Expense && this.CurrentDateFilter == DateFilter.ByDay)
                    getDataMethod = unitOfWork.BudgetItems.GetAllExpensesOfUserByDay;
                else if(this.BudgetItemType == ItemType.Income && this.CurrentDateFilter == DateFilter.ByMonth)
                    getDataMethod = unitOfWork.BudgetItems.GetAllIncomesOfUserByMonth;
                else
                    getDataMethod = unitOfWork.BudgetItems.GetAllIncomesOfUserByDay;
                
                items = getDataMethod(SettingsService.Instance.CurrentUser.Id, this.CurrentDateTime);

                CurrentItems = new ObservableCollection<BudgetItem>(items);
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            this.CurrentDateFilter = DateFilter.ByMonth;
            this.GetData();
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public ObservableCollection<BudgetItem> CurrentItems
        {
            get { return _currentItems; }
            set
            {
                _currentItems = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<object> SwitchMonthCommand
        {
            get
            {
                return _switchMonthCommand ?? (_switchMonthCommand = new RelayCommand<object>(param =>
                {
                    if (param != null)
                    {
                        var direction = (SwitchingMonthDirection) param;
                        switch (direction)
                        {
                            case SwitchingMonthDirection.Next:
                                if (this.CurrentDateFilter == DateFilter.ByMonth)
                                    this.CurrentMonthIndex++;
                                else
                                    this.CurrentDayIndex++;
                                break;
                            case SwitchingMonthDirection.Previous:
                                if (this.CurrentDateFilter == DateFilter.ByMonth)
                                    this.CurrentMonthIndex--;
                                else
                                    this.CurrentDayIndex--;
                                break;
                        }
                        if (this.CurrentDateFilter == DateFilter.ByMonth)
                            this.CurrentDateTime = DateTime.Now.Date.AddMonths(this.CurrentMonthIndex);
                        else
                            this.CurrentDateTime = DateTime.Now.Date.AddDays(this.CurrentDayIndex);

                        this.RaisePropertyChanged("CurrentDateHeader");
                        this.GetData();
                    }
                }));
            }
        }

        public RelayCommand<object> SwitchItemTypeCommand
        {
            get
            {
                return _switchItemTypeCommand ?? (_switchItemTypeCommand = new RelayCommand<object>(param =>
                {
                    if (param != null)
                    {
                        var itemType = (ItemType)param;
                        this.BudgetItemType = itemType;
                        GetData();
                    }
                }));
            }
        }

        public RelayCommand<object> SwitchCurrentFilteringCommand
        {
            get
            {
                return _switchCurrentFilteringCommand ?? (_switchCurrentFilteringCommand = new RelayCommand<object>(
                    param =>
                    {
                        if (param != null)
                        {
                            var paramFilter = (DateFilter) param;
                            if (this.CurrentDateFilter != paramFilter)
                            {
                                this.CurrentDateFilter = paramFilter;
                                this.CurrentDateTime = DateTime.Now;
                                this.CurrentMonthIndex = 0;
                                this.CurrentDayIndex = 0;
                                this.GetData();
                                this.RaisePropertyChanged("CurrentDateHeader");
                            }
                        }
                    }));
            }
        }
    }
}
