using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Model.Domain;
using NzWalks.Model.DTO;
using NzWalks.Repositories;

namespace NzWalks.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController: Controller
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalksRepository walksRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllWalks() {
            var walks = await walksRepository.GetAllAsync();

            var walksDTO = mapper.Map<List<WalkDTO>>(walks);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalk")]
        public async Task<IActionResult> GetWalk([FromRoute] Guid id) {
            var walk = await walksRepository.GetAsync(id);

            if(walk == null) {
                return NotFound();
            }

            var WalkDTO = mapper.Map<WalkDTO>(walk);
            return Ok(WalkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] NewWalkDTO newWalkDTO) {

            if(!(await ValidateAddAsync(newWalkDTO))){
                return BadRequest(ModelState);
            }

            // Convert to domain object
            var walkDomain = new Walk
            {
                Length = newWalkDTO.Length,
                Name = newWalkDTO.Name,
                RegionId = newWalkDTO.RegionId,
                WalkDifficultyId = newWalkDTO.WalkDifficultyId
            };

            //Pass domain object to repository
            var createdWalk = await walksRepository.AddAsync(walkDomain);

            // Convert domain object to DTO
            var walkDTO = mapper.Map<WalkDTO>(createdWalk);

            //Send response to client
            return CreatedAtAction(nameof(GetWalk), new {id = walkDTO.Id}, walkDTO);
        } 

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkDTO updateWalkDTO) {

            if(!(await ValidateUpdateWalk(updateWalkDTO))) {
                return BadRequest(ModelState);
            }

            // Convert to domain object
            var walkDomain = new Walk
            {
                Length = updateWalkDTO.Length,
                Name = updateWalkDTO.Name,
                RegionId = updateWalkDTO.RegionId,
                WalkDifficultyId = updateWalkDTO.WalkDifficultyId
            };

            // Updates Walk
            var updatedWalk = await walksRepository.UpdateAsync(id, walkDomain);

            if(updatedWalk == null) {
                return NotFound();
            }

            //Convert to DTO
            var updatedWalkDTO = mapper.Map<WalkDTO>(updatedWalk);
            return Ok(updatedWalk);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id) {

            var deletedWalk = await walksRepository.DeleteAsync(id);

            if(deletedWalk != null) {
                var deletedWalkDTO = mapper.Map<WalkDTO>(deletedWalk);
                return Ok(deletedWalkDTO);
            } else
                return NotFound();
        }

        #region     Private Methods

        private async Task<bool> ValidateAddAsync(NewWalkDTO newWalkDTO) {
            if(AddWalkAsync == null) {
                ModelState.AddModelError(nameof(newWalkDTO), $"Data is required");
            }

            if(string.IsNullOrWhiteSpace(newWalkDTO.Name)) {
                ModelState.AddModelError(nameof(newWalkDTO.Name), $"Name is required");
            }

            if(newWalkDTO.Length <= 0) {
                ModelState.AddModelError(nameof(newWalkDTO.Length), $"Length should be greater than 0");
            }

            var region = await regionRepository.GetAsync(newWalkDTO.RegionId);
            if(region == null) {
                ModelState.AddModelError(nameof(newWalkDTO.RegionId), $"Region Id is invalid");
            }

            var walkDifficulty = await walkDifficultyRepository.GetAsync(newWalkDTO.WalkDifficultyId);
            if(walkDifficulty == null) {
                ModelState.AddModelError(nameof(newWalkDTO.WalkDifficultyId), $"Difficulty Id is invalid");
            }

            if(ModelState.ErrorCount > 0) {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalk(UpdateWalkDTO updateRegionDTO) {
            if(AddWalkAsync == null) {
                ModelState.AddModelError(nameof(updateRegionDTO), $"Data is required");
            }

            if(string.IsNullOrWhiteSpace(updateRegionDTO.Name)) {
                ModelState.AddModelError(nameof(updateRegionDTO.Name), $"Name is required");
            }

            if(updateRegionDTO.Length <= 0) {
                ModelState.AddModelError(nameof(updateRegionDTO.Length), $"Length should be greater than 0");
            }

            var region = await regionRepository.GetAsync(updateRegionDTO.RegionId);
            if(region == null) {
                ModelState.AddModelError(nameof(updateRegionDTO.RegionId), $"Region Id is invalid");
            }

            var walkDifficulty = await walkDifficultyRepository.GetAsync(updateRegionDTO.WalkDifficultyId);
            if(walkDifficulty == null) {
                ModelState.AddModelError(nameof(updateRegionDTO.WalkDifficultyId), $"Difficulty Id is invalid");
            }

            if(ModelState.ErrorCount > 0) {
                return false;
            }

            return true;
        }

        #endregion
    }
}
