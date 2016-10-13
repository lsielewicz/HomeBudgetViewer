
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using HomeBudgetViewer.Database.Engine.Repository.Base;

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

    }
}
