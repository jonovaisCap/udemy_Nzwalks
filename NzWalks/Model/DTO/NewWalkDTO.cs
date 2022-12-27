using System;

namespace NzWalks.Model.DTO
{
    public class NewWalkDTO
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
