using System;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Model.Domain;
using NzWalks.Repositories;
using NzWalks.Model.DTO;
using AutoMapper;

namespace NzWalks.Controllers
{  
    [ApiController]
    [Route("Regions")]
    public class RegionController : Controller
    {   
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions() {
            
            var regions = await regionRepository.GetAllAsync();

            //return DTO regions
 /*           var regionsDTO = new List<RegionDTO>();
            regions.ToList().ForEach(region => {
                var RegionDTO = new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population
                };

                regionsDTO.Add(RegionDTO);
            });*/

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionsDTO);
        }
    }
}
