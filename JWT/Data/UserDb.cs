using JWT.Models;

namespace JWT.Data
{
    public class UserDb
    {
        public static List<User> Users = new List<User>
        {
            new User()
            {
                Username= "Ola", Password = "1234", Role = "Admin"
            }
        };
    }
}
