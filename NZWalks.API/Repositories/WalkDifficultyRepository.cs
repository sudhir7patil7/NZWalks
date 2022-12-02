using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly AppDbContext appDbContext;

        public WalkDifficultyRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty )
        {
            walkDifficulty.Id = Guid.NewGuid();
            await appDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await this.appDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await appDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await  appDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id==id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id,WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await appDbContext.WalkDifficulty.FindAsync(id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }
            existingWalkDifficulty.Code = walkDifficulty.Code;
            await appDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
