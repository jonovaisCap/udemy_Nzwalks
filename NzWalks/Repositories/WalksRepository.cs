using System;
using NzWalks.Data;
using NzWalks.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace NzWalks.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NzWalksDbContext nzWalksDbContext;

        public WalksRepository(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {   
            walk.Id = Guid.NewGuid();
            var returnedWalk = await nzWalksDbContext.AddAsync(walk);
            await nzWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await nzWalksDbContext.Walk.FindAsync(id);
            if(walk == null) {
                return null;
            }

            nzWalksDbContext.Remove(walk);
            nzWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await 
            nzWalksDbContext.Walk
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            var walk = await nzWalksDbContext.Walk
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var returnedWalk = await nzWalksDbContext.Walk.FindAsync(id);

            if(returnedWalk == null) {
                return null;
            }

            returnedWalk.Length = walk.Length;
            returnedWalk.Name = walk.Name;
            returnedWalk.RegionId = walk.RegionId;
            returnedWalk.WalkDifficultyId = walk.WalkDifficultyId;

            await nzWalksDbContext.SaveChangesAsync();

            return returnedWalk;
        }
    }
}
