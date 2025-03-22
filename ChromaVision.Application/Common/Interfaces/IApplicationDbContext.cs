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

        // EntityFramework metodları ekleyelim
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Set metodu ekleyelim
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        // Entry metodu ekleyelim
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}

