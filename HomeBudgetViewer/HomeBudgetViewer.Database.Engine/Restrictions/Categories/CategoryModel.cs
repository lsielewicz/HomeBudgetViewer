using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Resources;


namespace HomeBudgetViewer.Database.Engine.Restrictions.Categories
{
    using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
    public class CategoryModel
    {
        public ItemType ItemType { get; set; }
        public Category CategoryEnum { get; set; }
        public string CategoryName { get; set; }

        private static List<CategoryModel> _possibleCategories;

        private CategoryModel(Category category, ItemType itemType = ItemType.Common)
        {
            this.CategoryEnum = category;
            this.CategoryName = category.ToString();
            this.ItemType = itemType;
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
                    new CategoryModel(Category.Car, ItemType.Expense),
                    new CategoryModel(Category.Food, ItemType.Expense),
                    new CategoryModel(Category.FoodOutsideTheHome, ItemType.Expense),
                    new CategoryModel(Category.Sport, ItemType.Expense),
                    new CategoryModel(Category.Transport, ItemType.Expense),
                    new CategoryModel(Category.Entertainment, ItemType.Expense),
                    new CategoryModel(Category.Culture, ItemType.Expense),
                    new CategoryModel(Category.Clothes, ItemType.Expense),
                    new CategoryModel(Category.Personal, ItemType.Expense),
                    new CategoryModel(Category.Children, ItemType.Expense),
                    new CategoryModel(Category.Animals, ItemType.Expense),
                    new CategoryModel(Category.Home, ItemType.Expense),
                    new CategoryModel(Category.Phone, ItemType.Expense),
                    new CategoryModel(Category.Internet, ItemType.Expense),
                    new CategoryModel(Category.Electronic, ItemType.Expense),
                    new CategoryModel(Category.Credit, ItemType.Expense),
                    new CategoryModel(Category.Lent, ItemType.Expense),
                    new CategoryModel(Category.Vacation, ItemType.Expense),
                    new CategoryModel(Category.Fashion, ItemType.Expense),
                    new CategoryModel(Category.Health, ItemType.Expense),
                    new CategoryModel(Category.Drinking, ItemType.Expense),
                    new CategoryModel(Category.Salary, ItemType.Income),
                    new CategoryModel(Category.Allowance, ItemType.Income),
                    new CategoryModel(Category.Bonus, ItemType.Income),
                    new CategoryModel(Category.PettyCash, ItemType.Income),
                    new CategoryModel(Category.WonMoney, ItemType.Income),
                    new CategoryModel(Category.OddJobs,ItemType.Income),
                    new CategoryModel(Category.PersonalSavings, ItemType.Income),
                    new CategoryModel(Category.Pension, ItemType.Income),
                    new CategoryModel(Category.Rent, ItemType.Income),
                    new CategoryModel(Category.MoneyFromTheSale,ItemType.Income),
                    new CategoryModel(Category.Other, ItemType.Common)
                });
            }
        }

    }
}
