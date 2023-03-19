using HumanOrMutant.Persistence.Contracts;
using HumanOrMutant.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanOrMutant.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceRepository(this IServiceCollection services)
        {
            services.AddTransient<IHumanDnaRepository, HumanDnaRepository>();

            return services;
        }
    }
}
