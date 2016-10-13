using System;
using System.Linq;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;

namespace HomeBudgetViewer.Presentation.MainPage
{
    public class MainPageViewModel : AppViewModelBase
    {
        public MainPageViewModel()
        {
            using (var db = new BudgetContext())
            {
                User user = new User()
                {
                    Name = "Franek",
                    Currency = "EURO"
                };

                var b1 = new BudgetItem()
                {
                    Category = "Food",
                    Date = DateTime.Now,
                    Description = "osom",
                    MoneyValue = 123.42,
                    ItemType = "Income",
                    User = user
                };
                var b2 = new BudgetItem()
                {
                    Category = "Porn",
                    Date = DateTime.Now,
                    Description = "osom",
                    MoneyValue = 1233242,
                    ItemType = "Income",
                    User = user                    
                };
                db.BudgetItem.Add(b1);
                db.BudgetItem.Add(b2);
                db.User.Add(user);
                db.SaveChanges();
            }
            using (var db = new BudgetContext())
            {
                var list = db.BudgetItem.Where(b => b.User.Name == "Franek").ToList();
            }
        }

       
        }

    
}
