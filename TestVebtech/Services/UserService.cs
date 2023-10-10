using Microsoft.EntityFrameworkCore;
using TestVebtech.Interfaces;
using TestVebtech.Models;

namespace TestVebtech.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> AddRoleToUser(int userId, int roleId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);

            if (user != null && role != null) 
            {
                user.Roles.Add(role);

                await _context.SaveChangesAsync();

                return true;
            }
          

            return false;
        }

        public async Task<User> AddUser(User user)
        {
            if (await _context.Users.AllAsync(x => x.Email != user.Email))
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }

            return null;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                 _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<User>> GetAllUsers(string nameColumnToFilter, string filter,int? pageIndex, string nameColumnToSort)
        {
            var users =  _context.Users.Include(x => x.Roles);

            var filteredUsers = Filtering.FilterUsers(users, nameColumnToFilter, filter );
            var sortedUsers = Sorting.Sort(filteredUsers, nameColumnToSort);

            var g = sortedUsers.Select(x => new User {
                Id = x.Id, Name = x.Name, Email = x.Email, Age = x.Age,
                Roles = (List<Role>)x.Roles.Select(j => new Role {Id = j.Id, Name = j.Name})
            });

            return await Pagination<User>.CreatePaginationAsync(g, pageIndex ?? 1, 2);
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.Where(x => x.Id == id).Include(x => x.Roles).FirstOrDefaultAsync();

            if (user != null)
                return user;

            return null;
        }

        public async Task<User> UpdateUser(int id, User newUser)
        {
            var oldUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (oldUser != null)
            {
                _context.Entry(oldUser).CurrentValues.SetValues(newUser);
                await _context.SaveChangesAsync();

                return newUser;
            }

            return null;
        }
    }
}
