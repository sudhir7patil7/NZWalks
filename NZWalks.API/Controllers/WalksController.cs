using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper )
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAllWalksAsync()
        {
            //Fetch date from Database
            var walksDomin = await walkRepository.GetAllAsync();
            //Convert Domain Walks to DTO walks
            var walkDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomin);
            //return response
            return Ok(walkDTO);

        }
        [HttpGet]
        [Route("id:guid")]
        public async Task<IActionResult> getAllWalksAsync(Guid id)
        {
            //Get Walk Domain object from database
            var walkDomain = await walkRepository.GetAsync(id);
            //Convert Domain object to DTO
            var WalkDTO =  mapper.Map<Models.DTO.Walk>(walkDomain);
            //return response
            return Ok(WalkDTO);
        }
    }
}
