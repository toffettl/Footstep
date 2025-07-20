using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.RelationUser;
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
            var expirationTimeMinutes = Convert.ToUInt32(configuration["JWT_EXPIRATION_MINUTES"]);
            var signingKey = configuration["JWT_SECRET"];

            services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPointsOfInterestWriteOnlyRepository, PointOfInterestRepository>();
            services.AddScoped<IPointsOfInterestUpdateOnlyRepository, PointOfInterestRepository>();
            services.AddScoped<IPointsOfInterestReadOnlyRepository, PointOfInterestRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserRelationReadOnlyRepository, UserRelationRepository>();
            services.AddScoped<IUserRelationWriteOnlyRepository, UserRelationRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var conectionString = configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<FootstepDbContext>(config =>
                config.UseNpgsql(conectionString));
        }
    }
}
