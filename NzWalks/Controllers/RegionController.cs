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

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
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
        public async Task<IActionResult> AddRegionAsync(NewRegionDTO newRegion) {

            // Validate Request
            if(!ValidateAddRegionAsync(newRegion)) {
                return BadRequest(ModelState);
            }
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
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO regionDTO) {

            if(!ValidateUpdateRegion(regionDTO)) {
                return BadRequest(ModelState);
            }

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

        #region Private methods

        private bool ValidateAddRegionAsync(NewRegionDTO newRegionDTO) {

            if(newRegionDTO == null) {
                ModelState.AddModelError(nameof(newRegionDTO), $"Data is required");
            }

            if(string.IsNullOrWhiteSpace(newRegionDTO.Code)) {
                ModelState.AddModelError(nameof(newRegionDTO.Code), $"{nameof(newRegionDTO.Code)} cannot be empty or white space");
            }

            if(string.IsNullOrWhiteSpace(newRegionDTO.Name)) {
                ModelState.AddModelError(nameof(newRegionDTO.Name), $"{nameof(newRegionDTO.Name)} cannot be empty or white space");
            }

            if(newRegionDTO.Area <= 0) {
                ModelState.AddModelError(nameof(newRegionDTO.Area), $"{nameof(newRegionDTO.Name)} cannot be less or equal to zero");
            }

            if(newRegionDTO.Population < 0) {
                ModelState.AddModelError(nameof(newRegionDTO.Population), $"{nameof(newRegionDTO.Population)} cannot be less than zero");
            }

            if(ModelState.ErrorCount > 0) {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateRegion(UpdateRegionDTO updateRegionDTO) {
            if(updateRegionDTO == null) {
                ModelState.AddModelError(nameof(updateRegionDTO), $"Data is required");
            }

            if(string.IsNullOrWhiteSpace(updateRegionDTO.Code)) {
                ModelState.AddModelError(nameof(updateRegionDTO.Code), $"{nameof(updateRegionDTO.Code)} cannot be empty or white space");
            }

            if(string.IsNullOrWhiteSpace(updateRegionDTO.Name)) {
                ModelState.AddModelError(nameof(updateRegionDTO.Name), $"{nameof(updateRegionDTO.Name)} cannot be empty or white space");
            }

            if(updateRegionDTO.Area <= 0) {
                ModelState.AddModelError(nameof(updateRegionDTO.Area), $"{nameof(updateRegionDTO.Name)} cannot be less or equal to zero");
            }

            if(updateRegionDTO.Population < 0) {
                ModelState.AddModelError(nameof(updateRegionDTO.Population), $"{nameof(updateRegionDTO.Population)} cannot be less than zero");
            }

            if(ModelState.ErrorCount > 0) {
                return false;
            }

            return true;
        }

        #endregion
    }
}