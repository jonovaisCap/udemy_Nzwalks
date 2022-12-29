using System;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Model.Domain;
using NzWalks.Repositories;
using NzWalks.Model.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllRegions() {
            
            var regions = await regionRepository.GetAllAsync();

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRegionAsync(Guid id) {

            var region = await regionRepository.GetAsync(id);

            if(region == null)
            {
                return NotFound();
            }
            var RegionDTO = mapper.Map<RegionDTO>(region);

            return Ok(RegionDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegionAsync(NewRegionDTO newRegion) {

            //convert to domain model
            
            var region = new Region()
            {
                Code = newRegion.Code,
                Area = newRegion.Area,
                Lat = newRegion.Lat,
                Long = newRegion.Long,
                Name = newRegion.Name,
                Population = newRegion.Population
            };

            //Pass to repository
            region = await regionRepository.AddAsync(region);
            //Convert back to DTO
            var regionDTO = mapper.Map<RegionDTO>(region);

            return CreatedAtAction(nameof(GetRegionAsync), new {id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id) {

            var region = await regionRepository.DeleteAsync(id);

            if(region == null) {
                return NotFound();
            }
            
            var regionDTO = mapper.Map<RegionDTO>(region);

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO regionDTO) {

            var region = new Region()
            {
                Code = regionDTO.Code,
                Area = regionDTO.Area,
                Lat = regionDTO.Lat,
                Long = regionDTO.Long,
                Name = regionDTO.Name,
                Population = regionDTO.Population
            };
            var updatedRegion = await regionRepository.UpdateAsync(id, region);

            if(updatedRegion == null) {
                return NotFound();
            }

            var responseRegion = mapper.Map<RegionDTO>(updatedRegion);
            return Ok(responseRegion);
        }

    }
}