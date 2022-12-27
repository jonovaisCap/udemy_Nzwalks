using System;
using Microsoft.EntityFrameworkCore;
using NzWalks.Data;
using NzWalks.Model.Domain;

namespace NzWalks.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NzWalksDbContext nzWalksDbContext;

        public WalkDifficultyRepository(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }
        async Task<WalkDifficulty> IWalkDifficultyRepository.AddAsync(WalkDifficulty walkDifficulty)
        {   
            walkDifficulty.Id = Guid.NewGuid();
            await nzWalksDbContext.AddAsync(walkDifficulty);
            await nzWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        async Task<WalkDifficulty> IWalkDifficultyRepository.DeleteAsync(Guid id)
        {
            var existingDifficulty = await nzWalksDbContext.WalkDifficulty.FindAsync(id);

            if(existingDifficulty != null) {
                nzWalksDbContext.Remove(existingDifficulty);
                nzWalksDbContext.SaveChangesAsync();
                return existingDifficulty;
            } else {
                return null;
            }
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nzWalksDbContext.WalkDifficulty.ToListAsync();
        }

        async Task<WalkDifficulty> IWalkDifficultyRepository.GetAsync(Guid id)
        {
            return await nzWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task<WalkDifficulty> IWalkDifficultyRepository.UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingDifficulty = await nzWalksDbContext.WalkDifficulty.FindAsync(id);

            if(existingDifficulty != null) {
                existingDifficulty.Code = walkDifficulty.Code;
                await nzWalksDbContext.SaveChangesAsync();
                return existingDifficulty;
            } else
                return null;
        }
    }
}
