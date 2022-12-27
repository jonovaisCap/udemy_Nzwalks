using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Model.Domain;
using NzWalks.Model.DTO;
using NzWalks.Repositories;

namespace NzWalks.Controllers
{   
    [ApiController]
    [Route("WalkDifficulty")]
    public class WalkDifficultyController: Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var walkDifficulties = await walkDifficultyRepository.GetAllAsync();

            var walkDifficultiesDTO = mapper.Map<List<WalkDifficultyDTO>>(walkDifficulties);

            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id) {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);

            if(walkDifficulty != null) {
                var WalkDifficultyDTO = mapper.Map<WalkDifficultyDTO>(walkDifficulty);
                return Ok(walkDifficulty);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] NewWalkDifficultyDTO newWalkDifficultyDTO) {
            var domainDifficulty = new WalkDifficulty {
                Code = newWalkDifficultyDTO.Code
            };
            var createdDifficulty = await walkDifficultyRepository.AddAsync(domainDifficulty);

            var createdDifficultyDTO = mapper.Map<WalkDifficultyDTO>(createdDifficulty);

            return CreatedAtAction(nameof(GetAsync), new {Id = createdDifficultyDTO.Id}, createdDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] NewWalkDifficultyDTO newWalkDifficultyDTO) {
            var domainDifficulty = new WalkDifficulty {
                Code = newWalkDifficultyDTO.Code
            };

            var updatedDifficulty = await walkDifficultyRepository.UpdateAsync(id, domainDifficulty);

            if(updatedDifficulty != null) {
                var updatedDTO = mapper.Map<WalkDifficultyDTO>(updatedDifficulty);
                return Ok(updatedDTO);
            } else {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id) {
            var deletedDifficulty = await walkDifficultyRepository.DeleteAsync(id);

            if(deletedDifficulty != null) {
                var deletedDifficultyDTO = mapper.Map<WalkDifficultyDTO>(deletedDifficulty);
                return Ok(deletedDifficultyDTO);
            } else
                return NotFound();
        }
    }
}
