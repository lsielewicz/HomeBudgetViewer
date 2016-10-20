using System;
using System.Collections.Generic;
using HomeBudgetViewer.Database.Engine.Entities;

namespace HomeBudgetViewer.Database.Engine.Repository.Interfaces
{
    public interface IBudgetItemRepository : IRepository<BudgetItem>
    {
        List<BudgetItem> GetAllExpensesOfUserByDate(int userId, DateTime date);
        List<BudgetItem> GetAllIncomesOfUserByDate(int userId, DateTime date);
        double GetSumOfMonthExpenses(int userId, DateTime date);
        double GetSumOfMonthIncomes(int userId, DateTime date);
    }
}