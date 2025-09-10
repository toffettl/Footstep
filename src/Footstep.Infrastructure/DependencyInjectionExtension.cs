using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Addresses;
using Footstep.Domain.Repositories.CommentLikes;
using Footstep.Domain.Repositories.Comments;
using Footstep.Domain.Repositories.Items;
using Footstep.Domain.Repositories.Preferences;
using Footstep.Domain.Repositories.RelationUser;
using Footstep.Domain.Repositories.Styles;
using Footstep.Domain.Repositories.Traces;
using Footstep.Domain.Repositories.UserPointOfInterestRelations;
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

            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

            services.AddScoped<IUserRelationWriteOnlyRepository, UserRelationRepository>();
            services.AddScoped<IUserRelationReadOnlyRepository, UserRelationRepository>();

            services.AddScoped<IPreferenceWriteOnlyRepository, PreferenceRepository>();
            services.AddScoped<IPreferenceReadOnlyRepository, PreferenceRepository>();

            services.AddScoped<IStyleWriteOnlyRepository, StyleRepository>();
            services.AddScoped<IStyleReadOnlyRepository, StyleRepository>();

            services.AddScoped<IItemWriteOnlyRepository, ItemRepository>();
            services.AddScoped<IItemReadOnlyRepository, ItemRepository>();
            services.AddScoped<IItemUpdateOnlyRepository, ItemRepository>();

            services.AddScoped<IPointOfInterestWriteOnlyRepository, PointOfInterestRepository>();
            services.AddScoped<IPointOfInterestUpdateOnlyRepository, PointOfInterestRepository>();
            services.AddScoped<IPointOfInterestReadOnlyRepository, PointOfInterestRepository>();

            services.AddScoped<IUserPointOfInterestRelationWriteOnlyRepository, UserPointOfInterestRelationRepository>();
            services.AddScoped<IUserPointOfInterestRelationReadOnlyRepository, UserPointOfInterestRelationRepository>();
            services.AddScoped<IUserPointOfInterestRelationUpdateOnlyRepository, UserPointOfInterestRelationRepository>();

            services.AddScoped<IAddressWriteOnlyRepository, AddressRepository>();
            services.AddScoped<IAddressReadOnlyRepository, AddressRepository>();

            services.AddScoped<ICommentsWriteOnlyRepository, CommentRepository>();
            services.AddScoped<ICommentsReadOnlyRepository, CommentRepository>();
            services.AddScoped<ICommentsUpdateOnlyRepository, CommentRepository>();

            services.AddScoped<ICommentLikeWriteOnlyRepository, CommentLikeRepository>();
            services.AddScoped<ICommentLikeReadOnlyRepository, CommentLikeRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var conectionString = configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<FootstepDbContext>(config =>
                config.UseNpgsql(conectionString));
        }
    }
}
