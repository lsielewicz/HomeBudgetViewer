using System;
using System.Collections.Generic;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Restrictions.Categories;
using HomeBudgetViewer.Database.Engine.Restrictions.ItemType;
using HomeBudgetViewer.Models.Enum;

namespace HomeBudgetViewer.Database.Engine.Repository.Interfaces
{
    public interface IBudgetItemRepository : IRepository<BudgetItem>
    {
        List<BudgetItem> GetAllExpensesOfUserByMonth(int userId, DateTime date);
        List<BudgetItem> GetAllIncomesOfUserByMonth(int userId, DateTime date);
        double GetSumOfMonthExpenses(int userId, DateTime date);
        double GetSumOfMonthIncomes(int userId, DateTime date);
        List<BudgetItem> GetAllIncomesOfUserByDay(int userId, DateTime date);
        List<BudgetItem> GetAllExpensesOfUserByDay(int userId, DateTime date);
        int GetCountOfMonthExpenses(int userId, DateTime date);
        int GetCountOfMonthIncomes(int userId, DateTime date);
        int GetCountOfDayExepenses(int userId, DateTime date);
        int GetCountOfDayIncomes(int userId, DateTime date);

        int GetCountOfItems(int userId, DateTime date, ItemType itemType, DateFilter dateFiler);
        double GetSumOfItems(int userId, DateTime date, ItemType itemType, DateFilter dateFiler);

        double GetSumOfItemsByCategory(int userId, DateTime date, ItemType itemType, DateFilter dateFilter,Category category);

    }
}