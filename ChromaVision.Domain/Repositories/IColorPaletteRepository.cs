using ChromaVision.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Domain.Repositories
{
    public interface IColorPaletteRepository : IRepository<ColorPalette>
    {
        Task<IReadOnlyList<ColorPalette>> GetByUserIdAsync(Guid userId);
        Task<IReadOnlyList<ColorPalette>> SearchByNameAsync(string searchTerm);
    }
}
