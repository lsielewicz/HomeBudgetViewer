using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;

namespace HomeBudgetViewer.Presentation.BudgetItemPage
{
    public class BudgetItemPageViewModel : AppViewModelBase
    {
        private RelayCommand _navigateToCategorySelectionCommand;
        private CategoryModel _selectedCategory;
        public BudgetItemPageViewModel()
        {
            
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
    }
}
