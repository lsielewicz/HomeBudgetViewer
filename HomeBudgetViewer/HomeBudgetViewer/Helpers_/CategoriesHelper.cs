using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;

namespace HomeBudgetViewer.Helpers_
{
    public class CategoriesHelper
    {
        private static CategoriesHelper _instance;
        public static CategoriesHelper Instance
        {
            get
            {
                return _instance ?? (_instance = new CategoriesHelper());
            }
        }


        private readonly ResourceLoader _resourceLoader;
        private CategoriesHelper()
        {
            _resourceLoader = ResourceLoader.GetForViewIndependentUse();
        }

        private string GetLocalizedString(string resourceKey)
        {
            return this._resourceLoader.GetString(resourceKey);
        }

        public  List<CategoryModel> LocalizedCategories
        {
            get
            {
                List<CategoryModel> localizedCategories = new List<CategoryModel>();
                CategoryModel.PossibleCategories.ForEach(
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
        }
    }
}
