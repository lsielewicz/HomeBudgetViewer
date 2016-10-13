using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Repository.Base;
using HomeBudgetViewer.Database.Engine.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetViewer.Database.Engine.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public BudgetContext BudgetContext
        {
            get { return Context as BudgetContext;}
        }
    }
}
