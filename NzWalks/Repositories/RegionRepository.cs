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

        public async Task<Region> GetAsync(Guid id)
        {
            var region = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nzWalksDbContext.AddAsync(region);
            await nzWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(region == null) {
                return null;
            }

            //Delete region
            nzWalksDbContext.Regions.Remove(region);
            await nzWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRegion == null) {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await nzWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
