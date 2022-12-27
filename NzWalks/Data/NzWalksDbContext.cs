using System;
using Microsoft.EntityFrameworkCore;
using NzWalks.Model.Domain;

namespace NzWalks.Data
{
    public class NzWalksDbContext : DbContext
    {
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> options): base(options)
        {
            
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walk { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
