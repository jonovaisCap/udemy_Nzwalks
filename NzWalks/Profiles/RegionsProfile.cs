using System;
using AutoMapper;
using NzWalks.Model.Domain;
using NzWalks.Model.DTO;

namespace NzWalks.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile()
        {
            CreateMap<Region, RegionDTO>()
                .ForMember(dest => dest.Id, options =>options.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
