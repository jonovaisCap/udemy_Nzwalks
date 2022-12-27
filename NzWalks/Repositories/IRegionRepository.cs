using System;
using NzWalks.Model.Domain;

namespace NzWalks.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
