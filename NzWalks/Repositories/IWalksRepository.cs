using System;
using NzWalks.Model.Domain;

namespace NzWalks.Repositories
{
    public interface IWalksRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();

        Task<Walk> GetAsync(Guid id);

        Task<Walk> AddAsync(Walk walk);

        Task<Walk> DeleteAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);
    }
}
