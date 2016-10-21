using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Repository.Interfaces;

namespace HomeBudgetViewer.Database.Engine.Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BudgetContext _context;

        public UnitOfWork(BudgetContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            BudgetItems = new BudgetItemRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IUserRepository Users { get; }
        public IBudgetItemRepository BudgetItems { get; }
        public int Complete()
        {
            return _context.SaveChanges();
        }


    }
}
