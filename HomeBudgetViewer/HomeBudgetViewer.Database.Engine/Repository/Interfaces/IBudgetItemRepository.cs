using System;
using System.Collections.Generic;
using HomeBudgetViewer.Database.Engine.Entities;

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
    }
}