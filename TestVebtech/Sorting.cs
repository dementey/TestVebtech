using TestVebtech.Models;

namespace TestVebtech
{
    public class Sorting
    {
        public static IOrderedQueryable<User> Sort(IQueryable<User> users, string nameColumn)
        {
            switch (nameColumn.ToLower())
            {
                case "username":
                    users = users.OrderBy(x => x.Name);
                    break;
                case "age":
                    users = users.OrderBy(x => x.Age);
                    break;
                case "email":
                    users = users.OrderBy(x => x.Email);
                    break;
                case "rolename":
                    users = users.OrderBy(x => x.Roles.Single().Name);
                    break;
                default:
                    users = users.OrderBy(x => x.Id);
                    break;
            }

            return (IOrderedQueryable<User>)users;
        }
    }
}
