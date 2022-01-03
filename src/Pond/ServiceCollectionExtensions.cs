using Microsoft.Extensions.DependencyInjection;
using System;

namespace Pond
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPond(this IServiceCollection services, Action<PondOptions> configure = null)
        {
            services
                .AddSingleton<IFilePoolFactory, FilePoolFactory>();

            if (configure != null)
            {
                services.Configure<PondOptions>(configure);
            }
            return services;
        }
    }
}
