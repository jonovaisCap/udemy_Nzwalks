using System;
using NzWalks.Model.Domain;

namespace NzWalks.Model.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation Properties

        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
