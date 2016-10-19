using System;
using System.Collections.Generic;
using HomeBudgetViewer.Database.Engine.Entities;

namespace HomeBudgetViewer.Database.Engine.Repository.Interfaces
{
    public interface IBudgetItemRepository : IRepository<BudgetItem>
    {
        List<BudgetItem> GetAllOfUserByDate(int userId, DateTime date);
    }
}