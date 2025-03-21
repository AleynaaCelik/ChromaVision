using ChromaVision.Application.Common.Interfaces;
using ChromaVision.Domain.Entities;
using ChromaVision.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Infrastructure.Repositories
{
    public class ColorPaletteRepository : EfRepository<ColorPalette>, IColorPaletteRepository
    {
        public ColorPaletteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<ColorPalette> GetByIdAsync(Guid id)
        {
            return await _dbContext.ColorPalettes
                .Include(p => p.Colors)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IReadOnlyList<ColorPalette>> GetAllAsync()
        {
            return await _dbContext.ColorPalettes
                .Include(p => p.Colors)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ColorPalette>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.ColorPalettes
                .Include(p => p.Colors)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ColorPalette>> SearchByNameAsync(string searchTerm)
        {
            return await _dbContext.ColorPalettes
                .Include(p => p.Colors)
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
