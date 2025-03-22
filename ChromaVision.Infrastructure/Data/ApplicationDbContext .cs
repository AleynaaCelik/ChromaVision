using ChromaVision.Application.Common.Interfaces;
using ChromaVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Infrastructure.Data
{
    
        public class ApplicationDbContext : DbContext, IApplicationDbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<ColorPalette> ColorPalettes { get; set; }
            public DbSet<ColorItem> ColorItems { get; set; }
            public DbSet<User> Users { get; set; }

            public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }

            public DbSet<TEntity> Set<TEntity>() where TEntity : class
            {
                return base.Set<TEntity>();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

                // Add configuration for ColorPalette
                modelBuilder.Entity<ColorPalette>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.Description).HasMaxLength(500);

                    entity.HasMany(e => e.Colors)
                        .WithOne()
                        .HasForeignKey(e => e.PaletteId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // Add configuration for ColorItem
                modelBuilder.Entity<ColorItem>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.HexCode).IsRequired().HasMaxLength(10);
                    entity.Property(e => e.Name).HasMaxLength(50);
                });

                // Add configuration for User
                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                    entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.PasswordHash).IsRequired();

                    entity.HasIndex(e => e.Username).IsUnique();
                    entity.HasIndex(e => e.Email).IsUnique();
                });
            }
        }
    }

