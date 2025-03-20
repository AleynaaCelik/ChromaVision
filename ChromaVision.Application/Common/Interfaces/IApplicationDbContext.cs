using ChromaVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ColorPalette> ColorPalettes { get; }
        DbSet<ColorItem> ColorItems { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
