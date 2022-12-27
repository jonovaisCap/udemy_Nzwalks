using System;
using NzWalks.Model.Domain;
using NzWalks.Data;
using Microsoft.EntityFrameworkCore;

namespace NzWalks.Repositories
{
    public class RegionRepository : IRegionRepository
    {

        private readonly NzWalksDbContext nzWalksDbContext;

        public RegionRepository(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nzWalksDbContext.Regions.ToListAsync();
        }
    }
}
