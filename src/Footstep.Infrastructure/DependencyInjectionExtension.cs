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
            var expirationTimeMinutes = Convert.ToUInt32(1000);
            var signingKey = "-T%QuRqutu)]LoDn7Let59URPHGsTLWp3b1aQKE";

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
            var conectionString = "Host=interchange.proxy.rlwy.net;Port=37540;Database=footstep;Username=postgres;Password=MSYYgFCfUyXFFLJLNDpKWTuPGmRGGKJK;";

            services.AddDbContext<FootstepDbContext>(config =>
                config.UseNpgsql(conectionString));
        }
    }
}
