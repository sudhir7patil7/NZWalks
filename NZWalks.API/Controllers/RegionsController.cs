using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();
            //Return DTO Regions
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id= region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            //
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //Validate teh request
            if(!validateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }
            //Request(DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };
            //Pass details to Repository
            region = await regionRepository.AddAsync(region);
            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }
        [HttpDelete]
        public async Task<IActionResult>DeleteRegionAsync(Guid id)
        {
            //Get Region From Database
            var region =await regionRepository.DeleteAsync(id);
            //If null Found
            if (region==null)
            {
                return NotFound();
            }
            //Convert response bACK TO DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //return OK response
            return Ok(regionDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };
            //Update region using repository
            region=await regionRepository.UpdateAsync(id,region);
            //If null then NotFound
            if (region==null)
            {
                return NotFound();
            }
            //Convert Domain back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //return OK response
            return Ok(regionDTO);
        }
        #region Private methods
        private bool validateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest==null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), 
                    $"Add Region Data is Required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), $"{nameof(addRegionRequest.Code)} can not be null or empty or whitespace.");
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), $"{nameof(addRegionRequest.Name)} can not be null or empty or whitespace.");
            }
            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} can not be less than or equal to zero.");
            }
            if (addRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat), $"{nameof(addRegionRequest.Lat)} can not be less than or equal to zero.");
            }           
            if (addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} can not be less than or equal to zero.");
            }if (addRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{nameof(addRegionRequest.Population)} can not be less than or equal to zero.");
            }
            if(ModelState.ErrorCount>0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion

    }

}

