using System.Linq;
using HomeBudgetViewer.Database.Engine.Engine;
using HomeBudgetViewer.Database.Engine.Entities;
using HomeBudgetViewer.Database.Engine.Exceptions;
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

        public User GetUserByName(string name)
        {
            return BudgetContext.User.FirstOrDefault(u => u.Name == name);
        }

        public void AddUniqueUser(User user)
        {
            if (CheckIfUserNameExists(user.Name))
            {
                throw new HomeBudgetDbException("User already exist in database");
            }
            this.Add(user);
        }

        public bool CheckIfUserNameExists(string name)
        {
            return GetUserByName(name) != null;
        }

        public void DeleteUserUsingName(string name)
        {
            User user = GetUserByName(name);
            this.Remove(user);
        }

        public void UpdateUserUsingName(string name)
        {
            User user = GetUserByName(name);
            this.Update(user);
        }
    }
}
