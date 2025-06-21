using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Infrastructure.DataAccess;
using Footstep.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITracesWriteOnlyRepository, TracesRepostory>();
            services.AddScoped<ITracesUpdateOnlyRepository, TracesRepostory>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var conectionString = configuration.GetConnectionString("Connection");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

            services.AddDbContext<FootstepDbContext>(config => config.UseMySql(conectionString, serverVersion));
        }
    }
}
