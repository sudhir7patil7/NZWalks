using NZWalks.API.Models.Domain;
//using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task <Region> GetAsync(Guid id);
        Task<Region> AddAsync(Region region);
    }
}
