using System;
using AutoMapper;
using NzWalks.Model.Domain;
using NzWalks.Model.DTO;

namespace NzWalks.Profiles
{
    public class WalksProfile: Profile
    {
        public WalksProfile()
        {
            CreateMap<Walk, WalkDTO>()
                .ReverseMap();

            CreateMap<WalkDifficulty, WalkDifficultyDTO>()
                .ReverseMap();
        }
    }
}
