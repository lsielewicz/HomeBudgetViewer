
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;

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

        public List<BudgetItem> GetAllExpensesOfUserByDate(int userId, DateTime date)
        {
            var items = BudgetContext.BudgetItem.
                Where(
                    item =>
                        item.Date.Month == date.Month && item.Date.Year == date.Year && item.UserId == userId &&
                        item.ItemType == ItemType.Expense.ToString()).ToList();

            return items;
        }

        public List<BudgetItem> GetAllIncomesOfUserByDate(int userId, DateTime date)
        {
            var items = BudgetContext.BudgetItem.
                Where(
                    item =>
                        item.Date.Month == date.Month && item.Date.Year == date.Year && item.UserId == userId &&
                        item.ItemType == ItemType.Income.ToString()).ToList();

            return items;
        }
    }
}
