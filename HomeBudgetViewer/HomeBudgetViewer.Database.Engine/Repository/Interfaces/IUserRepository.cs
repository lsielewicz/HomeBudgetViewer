using HomeBudgetViewer.Database.Engine.Entities;

namespace HomeBudgetViewer.Database.Engine.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByName(string name);
        void AddUniqueUser(User user);
        bool CheckIfUserNameExists(string name);
        void DeleteUserUsingName(string name);
        void UpdateUserUsingName(string name);
    }
}