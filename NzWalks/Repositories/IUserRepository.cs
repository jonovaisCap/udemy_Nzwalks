using System;
using NzWalks.Model.Domain;

namespace NzWalks.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string username, string password);
    }
}
