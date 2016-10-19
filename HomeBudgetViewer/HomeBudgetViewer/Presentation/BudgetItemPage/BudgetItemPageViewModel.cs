using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Presentation.BudgetItemPage.Controls.Calculator;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.BudgetItemPage
{
    public class BudgetItemPageViewModel : AppViewModelBase
    {
        private RelayCommand _navigateToCategorySelectionCommand;
        private RelayCommand<object> _switchItemType;
        private CategoryModel _selectedCategory;
        private string _itemDescription;
        private ItemType _budgetItemType;

        public BudgetItemPageViewModel()
        {
            this.CalculatorViewModel = new CalculatorViewModel(this);
            this.SelectedCategory = ViewModelLocator.Instance.CategorySelectionPageViewModel.PossibleCategories.FirstOrDefault();
        }

        public CalculatorViewModel CalculatorViewModel { get; set; }

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

        public string ItemDescription
        {
            get { return _itemDescription; }
            set
            {
                if (_itemDescription == value)
                    return;
                _itemDescription = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand NavigateToCategorySelectionCommand
        {
            get
            {
                return _navigateToCategorySelectionCommand ?? (_navigateToCategorySelectionCommand = new RelayCommand(
                    () =>
                    {
                        this.NavigationService.Navigate(typeof(CategorySelectionPage.CategorySelectionPage));
                    }));
            }
        }

        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory == value)
                    return;
                _selectedCategory = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _assignBudgetItemToDatabase;
        public RelayCommand AssignBudgetItemToDataBase
        {
            get
            {
                return _assignBudgetItemToDatabase ?? (_assignBudgetItemToDatabase = new RelayCommand(() =>
                {
                    if (this.CalculatorViewModel.CurrentArithmeticalNumber == "0" ||
                        string.IsNullOrEmpty(this.CalculatorViewModel.CurrentArithmeticalNumber))
                        return;
                    this.CalculatorViewModel.CalculateResultCommand.Execute(this.CalculatorViewModel);
                    double moneyValue;
                    try
                    {
                        moneyValue = double.Parse(this.CalculatorViewModel.CurrentArithmeticalNumber);
                    }
                    catch
                    {
                        moneyValue = 0;
                    }

                    var budgetItem = new BudgetItem()
                    {
                        Category = this.SelectedCategory.CategoryEnum.ToString(),
                        UserId = SettingsService.Instance.CurrentUser.Id,
                        Date = DateTime.Now,
                        Description = this.ItemDescription,
                        MoneyValue = moneyValue,
                        ItemType = this.BudgetItemType.ToString(),       
                    };
                        
                    using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                    {
                        unitOfWork.BudgetItems.Add(budgetItem);
                        unitOfWork.Complete();
                    }
                    this.NavigationService.Navigate(typeof(HomeBudgetViewer.MainPage));
                    this.ClearViewModel();
                }));
            }
        }

        public RelayCommand<object> SwitchItemType
        {
            get
            {
                return _switchItemType ?? (_switchItemType = new RelayCommand<object>(param =>
                {
                    if (param != null)
                    {
                        var itemType = (ItemType) param;
                        this.BudgetItemType = itemType;
                    }
                }));
            }
        }

        private void ClearViewModel()
        {
            this.CalculatorViewModel.ClearOutputCommand.Execute(this.CalculatorViewModel);
            this.ItemDescription = string.Empty;
            this.SelectedCategory =
                ViewModelLocator.Instance.CategorySelectionPageViewModel.PossibleCategories.FirstOrDefault();
        }

    }
}
