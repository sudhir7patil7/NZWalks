using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext appDbContext;

        public RegionRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id=Guid.NewGuid();
            await appDbContext.AddAsync(region);
            appDbContext.SaveChanges(); 
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region= await appDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region==null)
            {
                return null;
            }
            //Delete the Region
            appDbContext.Regions.Remove(region);
            await appDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await appDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await appDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
