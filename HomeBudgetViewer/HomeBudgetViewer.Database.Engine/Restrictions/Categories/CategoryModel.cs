using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudgetViewer.Database.Engine.Restrictions.Categories
{
    public class CategoryModel
    {
        public Category CategoryEnum { get; set; }
        public string CategoryName { get; set; }

        private static List<CategoryModel> _possibleCategories;

        private CategoryModel(Category category)
        {
            this.CategoryEnum = category;
            this.CategoryName = category.ToString();
        }

        public CategoryModel()
        {
            
        }

        public static List<CategoryModel> PossibleCategories
        {
            get
            {
                return _possibleCategories ?? (_possibleCategories = new List<CategoryModel>()
                {
                    new CategoryModel(Category.Car),
                    new CategoryModel(Category.Food),
                    new CategoryModel(Category.FoodOutsideTheHome),
                    new CategoryModel(Category.Sport),
                    new CategoryModel(Category.Transport),
                    new CategoryModel(Category.Entertaiment),
                    new CategoryModel(Category.Culture),
                    new CategoryModel(Category.Clothes),
                    new CategoryModel(Category.Personal),
                    new CategoryModel(Category.Children),
                    new CategoryModel(Category.Animals),
                    new CategoryModel(Category.Home),
                    new CategoryModel(Category.Phone),
                    new CategoryModel(Category.Internet),
                    new CategoryModel(Category.Electronic),
                    new CategoryModel(Category.Credit),
                    new CategoryModel(Category.Lent),
                    new CategoryModel(Category.Vacation),
                    new CategoryModel(Category.Fashion),
                    new CategoryModel(Category.Health),
                    new CategoryModel(Category.Salary),
                    new CategoryModel(Category.Drinking),
                    new CategoryModel(Category.Other),
                });
            }
        }
    }
}
