using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;

namespace HomeBudgetViewer.Presentation.BudgetItemPage.CategorySelectionPage
{
    public class CategorySelectionPageViewModel : AppViewModelBase
    {      
        public CategorySelectionPageViewModel()
        {
            var localizedCategories = this.GetLocalizedCategories(CategoryModel.PossibleCategories);
            this.PossibleCategories = new ObservableCollection<CategoryModel>(localizedCategories);
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
            this.NavigationService.Navigate(typeof(BudgetItemPage));
        }

        private List<CategoryModel> GetLocalizedCategories(List<CategoryModel> possibleCategories)
        {
            List<CategoryModel> localizedCategories = new List<CategoryModel>();
            possibleCategories.ForEach(category =>
            {
                localizedCategories.Add(new CategoryModel()
                {
                    CategoryName = this.GetLocalizedString($"{category.CategoryName}"),
                    CategoryEnum = category.CategoryEnum
                });
            });
            return localizedCategories;
        }
    }
}
