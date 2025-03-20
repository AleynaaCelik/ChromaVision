using ChromaVision.Core.Interfaces;
using ChromaVision.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaVision.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            // Register core services
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
