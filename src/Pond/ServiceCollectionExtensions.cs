using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pond
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPond(this IServiceCollection services, Action<PondOptions> configure = null)
        {
            configure ??= new Action<PondOptions>(options => { });
            services.Configure(configure);

            services
                .AddSingleton<IFilePoolFactory, FilePoolFactory>()
                .AddTransient<IFilePoolConfigurationSelector, DefaultFilePoolConfigurationSelector>()
                .AddTransient(typeof(IFilePool<>), typeof(FilePool<>))
                .AddTransient<IFilePool, FilePool>()

                ;

            return services;
        }
    }
}
