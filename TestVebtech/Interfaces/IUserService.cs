using TestVebtech.Models;

namespace TestVebtech.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers(string nameColumnToFilter, string filter,int? pageIndex, string nameColumnToSort);
        Task<User> GetUserById(int id);
        Task<bool> AddRoleToUser(int userId, int roleId);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(int id, User user);
        Task<bool> DeleteUser(int id);
    }
}
