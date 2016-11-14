using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;

namespace HomeBudgetViewer.Presentation.BudgetItemPage.CategorySelectionPage
{
    public class CategorySelectionPageViewModel : AppViewModelBase
    {
        private RelayCommand<object> _switchItemTypeCommand;

        public CategorySelectionPageViewModel()
        {
            this.PossibleCategories = new ObservableCollection<CategoryModel>(this.GetLocalizedCategories(CategoryModel.PossibleCategories, this.ItemType));
        }

        private ItemType _itemType;
        public ItemType ItemType
        {
            get
            {
                return _itemType;
            }
            set
            {
                if (_itemType == value)
                    return;
                _itemType = value;
                ViewModelLocator.Instance.BudgetItemPageViewModel.BudgetItemType = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<CategoryModel> PossibleCategories { get; set; }

        public CategoryModel SelectedCategory
        {
            get
            {
                return ViewModelLocator.Instance.BudgetItemPageViewModel.SelectedCategory;
            }
            set
            {
                ViewModelLocator.Instance.BudgetItemPageViewModel.SelectedCategory = value;
                OnCategoryChanged();
            }
        }

        private void OnCategoryChanged()
        {
            if (this.SelectedCategory != null)
            {
                this.NavigationService.Navigate(typeof(BudgetItemPage), true);
            }
        }

        private List<CategoryModel> GetLocalizedCategories(List<CategoryModel> possibleCategories, ItemType itemType)
        {
            List<CategoryModel> localizedCategories = new List<CategoryModel>();
            possibleCategories.Where(c => c.ItemType == ItemType.Common || c.ItemType == itemType)
                .ToList()
                .ForEach(
                    category =>
                    {
                        localizedCategories.Add(new CategoryModel()
                        {
                            CategoryName = this.GetLocalizedString($"{category.CategoryName}"),
                            CategoryEnum = category.CategoryEnum,
                            ItemType = category.ItemType
                        });
                    });

            return localizedCategories;
        }

        public RelayCommand<object> SwitchItemTypeCommand
        {
            get
            {
                return this._switchItemTypeCommand ?? (_switchItemTypeCommand = new RelayCommand<object>((param) =>
                {
                    if (param != null)
                    {
                        var eParam = (ItemType) param;
                        this.ItemType = eParam;
                        this.PossibleCategories = new ObservableCollection<CategoryModel>(this.GetLocalizedCategories(CategoryModel.PossibleCategories,this.ItemType));
                        this.RaisePropertyChanged("PossibleCategories");
                    }
                }));
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter != null)
            {
                var eParameter = (ItemType) parameter;
                this.ItemType = eParameter;
                this.PossibleCategories = new ObservableCollection<CategoryModel>(this.GetLocalizedCategories(CategoryModel.PossibleCategories, this.ItemType));
                this.SelectedCategory = null;
                this.RaisePropertyChanged("PossibleCategories");
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
