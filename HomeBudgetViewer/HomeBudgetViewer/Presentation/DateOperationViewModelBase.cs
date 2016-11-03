using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Restrictions.Currency;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Messages;
using HomeBudgetViewer.Models.Enum;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation
{
    public abstract class DateOperationViewModelBase : AppViewModelBase
    {
        protected delegate List<BudgetItem> GetDataByDate(int id, DateTime date);

        private RelayCommand<object> _switchMonthCommand;
        private RelayCommand<object> _switchItemTypeCommand;
        private RelayCommand<object> _switchCurrentFilteringCommand;
        private BudgetItem _selectedBudgetItem;
        private ItemType _budgetItemType;
        public int CurrentMonthIndex { get; protected set; }
        public int CurrentDayIndex { get; protected set; }
        public DateTime CurrentDateTime { get; protected set; }
        public DateFilter CurrentDateFilter { get; protected set; }

        protected DateOperationViewModelBase()
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
            this.NavigationService.Navigate(typeof(BudgetItemPage.BudgetItemPage), true);
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
                    case DateFilter.None:
                        return $"{this.GetLocalizedString("AllTogether")}";
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

        public RelayCommand<object> SwitchMonthCommand
        {
            get
            {
                return _switchMonthCommand ?? (_switchMonthCommand = new RelayCommand<object>(param =>
                {
                    if (param != null)
                    {
                        var direction = (SwitchingMonthDirection)param;
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
                            var paramFilter = (DateFilter)param;
                            if (this.CurrentDateFilter != paramFilter)
                            {
                                this.CurrentDateFilter = paramFilter;
                                this.CurrentDateTime = DateTime.Now;
                                this.CurrentMonthIndex = 0;
                                this.CurrentDayIndex = 0;
                                this.GetData();
                                this.RaisePropertyChanged("CurrentDateHeader");
                                this.RaisePropertyChanged("CurrentDateFilter");
                            }
                        }
                    }));
            }
        }

        public virtual void GetData() { }
    }
}
