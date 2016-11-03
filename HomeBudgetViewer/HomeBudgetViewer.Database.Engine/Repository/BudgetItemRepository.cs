
using System;
using System.Collections.Generic;
using System.Linq;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Models.Enum;

namespace HomeBudgetViewer.Database.Engine.Repository
{
    public class BudgetItemRepository : Repository<BudgetItem>, IBudgetItemRepository
    {
        public BudgetItemRepository(DbContext context) : base(context)
        {
        }

        public BudgetContext BudgetContext
        {
            get { return Context as BudgetContext; }
        }

        public List<BudgetItem> GetAllExpensesOfUserByMonth(int userId, DateTime date)
        {
            var items = BudgetContext.BudgetItem.
                Where(
                    item =>
                        item.Date.Month == date.Month && item.Date.Year == date.Year && item.UserId == userId &&
                        item.ItemType == ItemType.Expense.ToString()).ToList();

            return items;
        }

        public List<BudgetItem> GetAllIncomesOfUserByMonth(int userId, DateTime date)
        {
            var items = BudgetContext.BudgetItem.
                Where(
                    item =>
                        item.Date.Month == date.Month && item.Date.Year == date.Year && item.UserId == userId &&
                        item.ItemType == ItemType.Income.ToString()).ToList();

            return items;
        }

        public double GetSumOfMonthExpenses(int userId, DateTime date)
        {
            var sum =
                BudgetContext.BudgetItem.Where(
                    item =>
                        item.User.Id == userId && item.Date.Month == date.Month &&
                        item.ItemType == ItemType.Expense.ToString()).Sum(item => item.MoneyValue);
            return sum;
        }

        public double GetSumOfMonthIncomes(int userId, DateTime date)
        {
            var sum =
                 BudgetContext.BudgetItem.Where(
                     item =>
             item.User.Id == userId && item.Date.Month == date.Month &&
             item.ItemType == ItemType.Income.ToString()).Sum(item => item.MoneyValue);
            return sum;
        }

        public List<BudgetItem> GetAllIncomesOfUserByDay(int userId, DateTime date)
        {
            var items = BudgetContext.BudgetItem.
                    Where(
                        item =>
                            item.Date.Month == date.Month && 
                            item.Date.Year == date.Year && item.UserId == userId &&
                            item.ItemType == ItemType.Income.ToString() &&
                            item.Date.Day == date.Day)
                            .ToList();
            return items;
        }

        public List<BudgetItem> GetAllExpensesOfUserByDay(int userId, DateTime date)
        {
            var items = BudgetContext.BudgetItem.
                    Where(
                        item =>
                            item.Date.Month == date.Month &&
                            item.Date.Year == date.Year && item.UserId == userId &&
                            item.ItemType == ItemType.Expense.ToString() &&
                            item.Date.Day == date.Day)
                            .ToList();
            return items;
        }

        public int GetCountOfMonthExpenses(int userId, DateTime date)
        {
            var countOfItems = BudgetContext.BudgetItem.Count(item => 
                        item.Date.Month == date.Month &&
                        item.Date.Year == date.Year && item.UserId == userId &&
                        item.ItemType == ItemType.Expense.ToString());

            return countOfItems;
        }

        public int GetCountOfMonthIncomes(int userId, DateTime date)
        {
            var countOfItems = BudgetContext.BudgetItem.Count(item => 
                        item.Date.Month == date.Month &&
                        item.Date.Year == date.Year && item.UserId == userId &&
                        item.ItemType == ItemType.Income.ToString());

            return countOfItems;
        }

        public int GetCountOfDayExepenses(int userId, DateTime date)
        {
            var countOfItems = BudgetContext.BudgetItem.Count(item => 
                        item.Date.Month == date.Month &&
                        item.Date.Year == date.Year && item.UserId == userId &&
                        item.ItemType == ItemType.Expense.ToString() &&
                        item.Date.Day == date.Day);

            return countOfItems;
        }

        public int GetCountOfDayIncomes(int userId, DateTime date)
        {
            var countOfItems = BudgetContext.BudgetItem.Count(item =>
                        item.Date.Month == date.Month &&
                        item.Date.Year == date.Year && item.UserId == userId &&
                        item.ItemType == ItemType.Income.ToString() &&
                        item.Date.Day == date.Day);

            return countOfItems;
        }

        public int GetCountOfItems(int userId, DateTime date, ItemType itemType, DateFilter dateFiler)
        {
            int countOfItems=0;
            switch (dateFiler)
            {
                case DateFilter.None:
                    countOfItems = BudgetContext.BudgetItem.Count(item => 
                       item.ItemType == itemType.ToString());
                    break;
                case DateFilter.ByDay:
                    countOfItems = BudgetContext.BudgetItem.Count(item =>
                       item.Date.Month == date.Month &&
                       item.Date.Year == date.Year && item.UserId == userId &&
                       item.ItemType == itemType.ToString() &&
                       item.Date.Day == date.Day);
                    break;
                case DateFilter.ByMonth:
                    countOfItems = BudgetContext.BudgetItem.Count(item => 
                       item.Date.Month == date.Month &&
                       item.Date.Year == date.Year && item.UserId == userId &&
                       item.ItemType == itemType.ToString());
                    break;
            }

            return countOfItems;
        }

        public double GetSumOfItems(int userId, DateTime date, ItemType itemType, DateFilter dateFiler)
        {
            double itemsSum = 0;
            switch (dateFiler)
            {
                case DateFilter.None:
                    itemsSum = BudgetContext.BudgetItem.Where(item =>
                         item.User.Id == userId &&
                         item.ItemType == itemType.ToString())
                         .Sum(item => item.MoneyValue);
                    break;
                case DateFilter.ByMonth:
                    itemsSum = BudgetContext.BudgetItem.Where(item =>
                         item.User.Id == userId &&
                         item.ItemType == itemType.ToString() &&
                         item.Date.Year == date.Year &&
                         item.Date.Month == date.Month)
                         .Sum(item => item.MoneyValue);
                    break;
                case DateFilter.ByDay:
                    itemsSum = BudgetContext.BudgetItem.Where(item =>
                          item.User.Id == userId &&
                          item.ItemType == itemType.ToString() &&
                          item.Date.Year == date.Year &&
                          item.Date.Month == date.Month &&
                          item.Date.Day == date.Day)
                          .Sum(item => item.MoneyValue);
                    break;
            }

            return itemsSum;
        }

        public double GetSumOfItemsByCategory(int userId, DateTime date, ItemType itemType, DateFilter dateFilter, Category category)
        {
            double itemsSum = 0;
            switch (dateFilter)
            {
                case DateFilter.None:
                    itemsSum = BudgetContext.BudgetItem.Where(item =>
                         item.User.Id == userId &&
                         item.ItemType == itemType.ToString() &&
                         item.Category == category.ToString())
                         .Sum(item => item.MoneyValue);
                    break;
                case DateFilter.ByMonth:
                    itemsSum = BudgetContext.BudgetItem.Where(item =>
                         item.User.Id == userId &&
                         item.ItemType == itemType.ToString() &&
                         item.Date.Year == date.Year &&
                         item.Date.Month == date.Month &&
                         item.Category == category.ToString())
                         .Sum(item => item.MoneyValue);
                    break;
                case DateFilter.ByDay:
                    itemsSum = BudgetContext.BudgetItem.Where(item =>
                          item.User.Id == userId &&
                          item.ItemType == itemType.ToString() &&
                          item.Date.Year == date.Year &&
                          item.Date.Month == date.Month &&
                          item.Date.Day == date.Day &&
                          item.Category == category.ToString())
                          .Sum(item => item.MoneyValue);
                    break;
            }

            return itemsSum;
        }
    }
}
