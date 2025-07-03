using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Domain.Repositories.Users;
using Footstep.Domain.Security.Cryptography;
using Footstep.Domain.Security.Tokens;
using Footstep.Infrastructure.DataAccess;
using Footstep.Infrastructure.DataAccess.Repositories;
using Footstep.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Internal;

namespace Footstep.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddToken(services, configuration);
            AddRepositories(services);

            services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypto>();
        }
        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITracesWriteOnlyRepository, TracesRepostory>();
            services.AddScoped<ITracesUpdateOnlyRepository, TracesRepostory>();
            services.AddScoped<ITracesReadOnlyRepository, TracesRepostory>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var conectionString = "Host=yamabiko.proxy.rlwy.net;Port=5432;Database=footstep;Username=postgres;Password=Host=yamabiko.proxy.rlwy.net;Port=23952;Database=footstep;Username=postgres;Password=cxESXilghkzQBLYjuNtNRSuDyAjcnrVz;";

            services.AddDbContext<FootstepDbContext>(config =>
                config.UseNpgsql(conectionString));
        }
    }
}
