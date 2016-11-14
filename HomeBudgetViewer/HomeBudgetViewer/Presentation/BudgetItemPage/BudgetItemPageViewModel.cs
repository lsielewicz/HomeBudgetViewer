using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Controls.ConfirmationDialog;
using HomeBudgetViewer.Controls.InformationDialog;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Messages;
using HomeBudgetViewer.Models.Enum;
using HomeBudgetViewer.Presentation.BudgetItemPage.Controls.Calculator;
using HomeBudgetViewer.Services.SettingService;

namespace HomeBudgetViewer.Presentation.BudgetItemPage
{
    public class BudgetItemPageViewModel : AppViewModelBase
    {
        private RelayCommand _navigateToCategorySelectionCommand;
        private RelayCommand _deleteSelectedBudgetItemCommand;
        private RelayCommand<object> _switchItemType;
        private CategoryModel _selectedCategory;
        private string _itemDescription;
        private DateTime _itemDate;
        private ItemType _budgetItemType;
        public BudgetItem ModifyingItem { get; set; }
       
        public BudgetItemPageViewModel()
        {
            this.MessengerInstance.Register<IsModifyingStateToBudgetItemMessage>(this, HandleModifyingStateMessage);
            this.CalculatorViewModel = new CalculatorViewModel(this);
            this.SelectedCategory = ViewModelLocator.Instance.CategorySelectionPageViewModel.PossibleCategories.FirstOrDefault(c=>c.CategoryEnum == Category.Other);
        }

        public CalculatorViewModel CalculatorViewModel { get; set; }
        public BudgetItemAction BudgetItemAction { get; set; }

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

        public DateTime ItemDate
        {
            get { return _itemDate; }
            set
            {
                _itemDate = value;
                this.RaisePropertyChanged();
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
                        this.NavigationService.Navigate(typeof(CategorySelectionPage.CategorySelectionPage),this.BudgetItemType);
                    }));
            }
        }

        public CategoryModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
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
                return _assignBudgetItemToDatabase ?? (_assignBudgetItemToDatabase = new RelayCommand(async () =>
                {
                    if (this.CalculatorViewModel.CurrentArithmeticalNumber == "0" ||
                        string.IsNullOrEmpty(this.CalculatorViewModel.CurrentArithmeticalNumber))
                    {
                        InformationDialog dialog = new InformationDialog(this.GetLocalizedString("CannotAddItemWithEmptyValue"));
                        await dialog.ShowAsync();
                        return;
                    }

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
                    switch (this.BudgetItemAction)
                    {
                        case BudgetItemAction.Adding:
                            var budgetItem = new BudgetItem()
                            {
                                Category = this.SelectedCategory.CategoryEnum.ToString(),
                                UserId = SettingsService.Instance.CurrentUser.Id,
                                Date = this.ItemDate,
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
                            break;
                        case BudgetItemAction.Modifying:
                            if (this.ModifyingItem != null)
                            {
                                this.ModifyingItem.Category = this.SelectedCategory.CategoryEnum.ToString();
                                this.ModifyingItem.Description = this.ItemDescription;
                                this.ModifyingItem.MoneyValue = moneyValue;
                                this.ModifyingItem.Date = this.ItemDate.Date;
                                this.ModifyingItem.ItemType = this.BudgetItemType.ToString();
                            }
                            using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                            {
                                unitOfWork.BudgetItems.Update(ModifyingItem);
                                unitOfWork.Complete();
                            }
                            this.NavigationService.Navigate(typeof(OverviewPage.OverviewPage));
                            break;
                    }
                   
                    this.ClearViewModelData();
                }));
            }
        }

        public RelayCommand<object> SwitchItemType
        {
            get
            {
                return _switchItemType ?? (_switchItemType = new RelayCommand<object>(param =>
                {
                    if (this.SelectedCategory == null)
                    {
                        this.SelectedCategory = ViewModelLocator.Instance.CategorySelectionPageViewModel.PossibleCategories.FirstOrDefault(c => c.CategoryEnum == Category.Other);
                    }
                    if (param != null)
                    {
                        var itemType = (ItemType) param;
                        this.BudgetItemType = itemType;
                        if (this.SelectedCategory.ItemType != this.BudgetItemType)
                        {
                            this.SelectedCategory =
                                ViewModelLocator.Instance.CategorySelectionPageViewModel.PossibleCategories
                                    .FirstOrDefault(c => c.CategoryEnum == Category.Other);
                        }
                    }
                }));
            }
        }

        public void ClearViewModelData()
        {
            this.CalculatorViewModel.ClearOutputCommand.Execute(this.CalculatorViewModel);
            this.ItemDescription = string.Empty;
            this.SelectedCategory =
                ViewModelLocator.Instance.CategorySelectionPageViewModel.PossibleCategories.FirstOrDefault(c=>c.ItemType == ItemType.Common);
            this.BudgetItemAction = BudgetItemAction.Adding;
            this.ItemDate = DateTime.Now;
            this.RaisePropertyChanged("BudgetItemAction");
        }

        private void HandleModifyingStateMessage(IsModifyingStateToBudgetItemMessage data)
        {
            this.ModifyingItem = data.BudgetItem;
            if (this.ModifyingItem != null)
            {
                this.BudgetItemAction = BudgetItemAction.Modifying;
                this.ItemDescription = this.ModifyingItem.Description;
                this.CalculatorViewModel.CurrentArithmeticalNumber = this.ModifyingItem.MoneyValue.ToString();
                this.SelectedCategory =
                    ViewModelLocator.Instance.CategorySelectionPageViewModel.PossibleCategories.FirstOrDefault(
                        c => c.CategoryEnum.ToString() == this.ModifyingItem.Category);
                this.BudgetItemType = (ItemType)Enum.Parse(typeof(ItemType), this.ModifyingItem.ItemType);
                this.ItemDate = ModifyingItem.Date;
            }
        }

        public RelayCommand DeleteSelectedBudgetItemCommand
        {
            get
            {
                return _deleteSelectedBudgetItemCommand ?? (_deleteSelectedBudgetItemCommand = new RelayCommand(async () =>
                {
                    if (this.ModifyingItem == null)
                        return;

                    var dialog = new ConfirmationDialog(this.GetLocalizedString("RemoveDialogText"));
                    await dialog.ShowAsync();
                    if (dialog.ConfirmationDialogResult == ConfirmationDialogResult.Confirmed)
                    {
                        using (var unitOfWork = new UnitOfWork(new BudgetContext()))
                        {
                            unitOfWork.BudgetItems.Remove(this.ModifyingItem);
                            unitOfWork.Complete();
                        }
                        this.NavigationService.Navigate(typeof(OverviewPage.OverviewPage));
                        this.ClearViewModelData();
                    }
                                     
                }, () => this.ModifyingItem != null && this.BudgetItemAction == BudgetItemAction.Modifying));
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if(parameter == null)
                this.ClearViewModelData();
            this.CalculatorViewModel.InitializeCurrency();
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
