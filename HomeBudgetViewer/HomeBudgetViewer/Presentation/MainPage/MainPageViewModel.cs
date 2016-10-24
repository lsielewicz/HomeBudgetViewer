using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Controls.AddUserProfileDialog;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Currency;
using HomeBudgetViewer.Presentation.BudgetItemPage.CategorySelectionPage;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.MainPage
{
    public class MainPageViewModel : AppViewModelBase
    {
        private RelayCommand _navigateToAddBudgetItemsCommand;
        private RelayCommand _navigateToOverviewCommand;
        private RelayCommand _navigateToSettingsCommand;
        private RelayCommand _navigateToAboutCommand;
        private RelayCommand _navigateToSummaryCommand;

        public MainPageViewModel()
        {
           
        }

        public RelayCommand NavigateToSummaryCommand
        {
            get
            {
                return _navigateToSummaryCommand ?? (_navigateToSummaryCommand = new RelayCommand(() =>
                {
                    this.NavigationService.Navigate(typeof(SummaryPage.SummaryPage));
                }));
            }
        }

        public RelayCommand NavigateToAboutCommand
        {
            get
            {
                return _navigateToAboutCommand ?? (_navigateToAboutCommand = new RelayCommand(() =>
                {
                    this.NavigationService.Navigate(typeof(AboutPage.AboutPage));
                }));
            }
        }

        public RelayCommand NavigateToSettingsCommand
        {
            get
            {
                return _navigateToSettingsCommand ?? (_navigateToSettingsCommand = new RelayCommand(() =>
                {
                    this.NavigationService.Navigate(typeof(SettingsPage.SettingsPage));
                }));
            }
        }

        public RelayCommand NavigateToOverviewCommand
        {
            get
            {
                return _navigateToOverviewCommand ?? (_navigateToOverviewCommand = new RelayCommand(() =>
                {
                    this.NavigationService.Navigate(typeof(OverviewPage.OverviewPage));
                }));
            }
        }

        public RelayCommand NavigateToAddBudgetItemCommand
        {
            get
            {
                return _navigateToAddBudgetItemsCommand ?? (_navigateToAddBudgetItemsCommand = new RelayCommand(() =>
                {
                    this.NavigationService.Navigate(typeof(BudgetItemPage.BudgetItemPage));
                }));
            }
        }


        public string HelloMessage
        {
            get { return $"{this.GetLocalizedString("Hello")} {SettingsService.Instance.CurrentUser.Name}"; }
        }

        public string CurrentStatusMessage
        {
            get
            {
                var message = $"{this.GetLocalizedMonth(DateTime.Now.Month)} {DateTime.Now.Year}";
                return message;
            }
        }

        public double CurrentIcomes
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                {
                    double sumIncomes = unitOfWork.BudgetItems.GetSumOfMonthIncomes(
                        SettingsService.Instance.CurrentUser.Id,DateTime.Now);
                    return sumIncomes;
                }
            }
        }

        public double CurrentExpenses
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                {
                    double sumExpenses = unitOfWork.BudgetItems.GetSumOfMonthExpenses(
                        SettingsService.Instance.CurrentUser.Id, DateTime.Now);
                    
                    return sumExpenses;
                }
            }
        }

        public string ExpensesHeader
        {
            get
            {
                return
                    $"{this.GetLocalizedString("ExpensesT")}: {this.GetMoneyValue(this.CurrentExpenses)} {CurrencyModel.PossibleCurrencies.FirstOrDefault(c => c.CurrencyEnum.ToString() == SettingsService.Instance.CurrentUser.Currency).CurrencySymbol}";
            }
        }

        public string IncomesHeader
        {
            get
            {
                return
                    $"{this.GetLocalizedString("IncomesT")}: {this.GetMoneyValue(this.CurrentIcomes)} {CurrencyModel.PossibleCurrencies.FirstOrDefault(c => c.CurrencyEnum.ToString() == SettingsService.Instance.CurrentUser.Currency).CurrencySymbol}";
            }
        }

        public double ExpensesProgress
        {
            get { return this.CurrentExpenses > this.CurrentIcomes ? 100 : (this.CurrentExpenses / this.CurrentIcomes) * 100; }
        }

        public double IncomesProgress
        {
            get { return this.CurrentIcomes > this.CurrentExpenses ? 100 : (this.CurrentIcomes / this.CurrentExpenses) * 100; }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //this.NavigationService.Navigate(typeof(BudgetItemPage.BudgetItemPage));
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }   
}
