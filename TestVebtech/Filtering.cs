using TestVebtech.Models;

namespace TestVebtech
{
    public class Filtering
    {
        public static IQueryable<User> FilterUsers(IQueryable<User> users, string nameColumn, string filter)
        {
            if (String.IsNullOrEmpty(nameColumn) || String.IsNullOrEmpty(filter))
                return users;
            switch (nameColumn.ToLower())
            {
                case "username":
                    users = users.Where(u => u.Name.Contains(filter));
                    break;
                case "age":
                    users = users.Where(u => u.Age.ToString().Contains(filter));
                    break;
                case "email":
                    users = users.Where(u => u.Email.Contains(filter));
                    break;
                case "rolename":
                    users = users.Where(x => x.Roles.Single().Name.Contains(filter));
                    break;
            }

            return users;
        }
    }
}
