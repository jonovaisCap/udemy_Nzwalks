using System;
using NzWalks.Model.Domain;

namespace NzWalks.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
