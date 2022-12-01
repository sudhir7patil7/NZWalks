using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly AppDbContext appDbContext;

        public WalkRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            //Assign new Id
            walk.Id=Guid.NewGuid();
            await appDbContext.Walks.AddAsync(walk);
            await appDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await appDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public Task<Walk> GetAsync(Guid id)
        {
            return appDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateASync(Guid id, Walk walk)
        {
            var existingWalk=await appDbContext.Walks.FindAsync(id);
            if (existingWalk!=null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name=walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId=walk.RegionId;
                await appDbContext.SaveChangesAsync();
                return existingWalk;
            }
            return null;
        }
    }
}
