using System;
using NzWalks.Model.Domain;

namespace NzWalks.Repositories
{
    public class StaticUserRepository : IUserRepository
    {

        private List<User> users = new List<User>()
        {
            // new User()
            // {
            //     FirstName = "Read Only",
            //     LastName = "User",
            //     EmailAddress = "readonly@user.com",
            //     Id = Guid.NewGuid(),
            //     Username = "readonlyUser",
            //     Password = "Password",
            //     Roles = new List<string> {"reader"}
            // },
            // new User()
            // {
            //     FirstName = "Read Write",
            //     LastName = "User",
            //     EmailAddress = "readwrite@user.com",
            //     Id = Guid.NewGuid(),
            //     Username = "username",
            //     Password = "Password1",
            //     Roles = new List<string> {"reader", "writer"}
            // }
        };

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user =users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password == password);

            return user;
        }
    }
}
