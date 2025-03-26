using Microsoft.Extensions.Configuration;
using ChromaVision.Application.Common.Interfaces;
using ChromaVision.Core.Interfaces;
using ChromaVision.Domain.Repositories;
using ChromaVision.Infrastructure.Logging;
using ChromaVision.Infrastructure.Repositories;
using ChromaVision.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChromaVision.Infrastructure.Data; // ApplicationDbContext için

namespace ChromaVision.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register database context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Register ApplicationDbContext as IApplicationDbContext
            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            // Register repositories
            services.AddScoped<IColorPaletteRepository, ColorPaletteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Register infrastructure services
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IOpenAIService, OpenAIService>();
            services.AddTransient<IImageProcessingService, ImageProcessingService>();
            services.AddTransient<IJwtService, JwtService>();

            // Add HttpContextAccessor
            services.AddHttpContextAccessor();

            // JWT kimlik doğrulama kısmını kaldırdım çünkü bu Program.cs içinde zaten ekleniyor
            // Bu, "Scheme already exists: Bearer" hatasını çözecek

            return services;
        }
    }
}