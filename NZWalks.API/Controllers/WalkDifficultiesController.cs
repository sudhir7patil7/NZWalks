using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}
