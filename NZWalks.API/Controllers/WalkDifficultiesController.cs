using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(Repositories.IWalkDifficultyRepository walkDifficultyRepository,IMapper mapper )
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficltyDomain= await walkDifficultyRepository.GetAllAsync();
            var walkDifficltyDTO=mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficltyDomain);
            return Ok(walkDifficltyDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDiffficultyById")]
        public async Task<IActionResult> GetWalkDiffficultyById(Guid id)
        {
            var walkDiccicilty=await walkDifficultyRepository.GetAsync(id);
            if (walkDiccicilty==null)
            {
                return NotFound();
            }

            //Convert Domain to DTO
            var walkDicciciltyDTO=mapper.Map<Models.DTO.WalkDifficulty>(walkDiccicilty) ;
            return Ok(walkDicciciltyDTO);
        }
        [HttpPost]
        
        public async Task<IActionResult>AddWalkDifficulties(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Convert DTO to Domain Model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };
            //Call Repository
            walkDifficultyDomain=await walkDifficultyRepository.AddAsync(walkDifficultyDomain);
            //Convert Domain to DTO
            var walkDifficultyDTO=mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            //Return response
            return CreatedAtAction(nameof(GetWalkDiffficultyById),
                new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id , Models.DTO.UpdateWalkDifficultyRequest    updateWalkDifficultyRequest )
        {
            //Convert DTO to Domain Model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };
            //Call repository to update
            walkDifficultyDomain=await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
                //Convert Domain to DTO
                var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
                //Return response
                return Ok(walkDifficultyDTO);
        }
    }
}
