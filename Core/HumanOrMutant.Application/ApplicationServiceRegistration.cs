using HumanOrMutant.Application.Interfaces;
using HumanOrMutant.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanOrMutant.Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IHumanService, HumanService>()
                .AddTransient<IStatsService, StatsService>();

            return services;
        }

    }
}
