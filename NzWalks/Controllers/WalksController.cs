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

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
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
    }
}
