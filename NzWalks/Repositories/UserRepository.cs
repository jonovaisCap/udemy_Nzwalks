using System;
using Microsoft.EntityFrameworkCore;
using NzWalks.Data;
using NzWalks.Model.Domain;

namespace NzWalks.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NzWalksDbContext nzWalksDbContext;

        public UserRepository(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await nzWalksDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            if(user == null) {
                return null;
            }

            var userRoles = await nzWalksDbContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();

            if(userRoles.Any()) {
                user.Roles = new List<string>();
                foreach(var userRole in userRoles) {
                    var role =await nzWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null) {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null;
            return user;
        }
    }
}
