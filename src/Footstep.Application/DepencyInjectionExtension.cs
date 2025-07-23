using Footstep.Application.AutoMapper;
using Footstep.Application.UseCases.Comments.Create;
using Footstep.Application.UseCases.Comments.Delete;
using Footstep.Application.UseCases.Comments.GetByAuthorId;
using Footstep.Application.UseCases.Comments.GetByParentIdAndAuthorId;
using Footstep.Application.UseCases.Comments.GetByParentsId;
using Footstep.Application.UseCases.Comments.Update;
using Footstep.Application.UseCases.RelationUser.Follow;
using Footstep.Application.UseCases.Traces.Create;
using Footstep.Application.UseCases.Traces.Delete;
using Footstep.Application.UseCases.Traces.GetAll;
using Footstep.Application.UseCases.Traces.GetById;
using Footstep.Application.UseCases.Traces.GetByRay;
using Footstep.Application.UseCases.Traces.Update;
using Footstep.Application.UseCases.Users.GetByEmail;
using Footstep.Application.UseCases.Users.Login;
using Footstep.Application.UseCases.Users.Register;
using Footstep.Application.UseCases.Users.UpdatePreferences;
using Footstep.Application.UseCases.Users.UpdateUnlockedStyles;
using Microsoft.Extensions.DependencyInjection;

namespace Footstep.Application
{
    public static class DepencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreatePointOfInterestUseCase, CreatePointOfInterestUseCase>();
            services.AddScoped<IDeletePointOfInterestUseCase, DeletePointOfInterestUseCase>();
            services.AddScoped<IUpdatePointOfInterestUseCase, UpdatePointOfInterestUseCase>();
            services.AddScoped<IGetByIdPointOfInterestUseCase, GetByIdPointsOfInterestUseCase>();
            services.AddScoped<IGetAllPoitntOfInterestUseCase, GetAllPointOfInterestUseCase>();
            services.AddScoped<IGetNearbyPointsOfInterestUseCase, GetNearbyPointsOfInterestUseCase>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetByEmailUserUseCase, GetByEmailUserUseCase>();
            services.AddScoped<IUpdatePreferencesUserUseCase, UpdatePreferencesUserUseCase>();
            services.AddScoped<IUpdateUnlockedStylesUserUseCase, UpdateUnlockedStylesUserUseCase>();
            services.AddScoped<IFollowUserRelationUseCase, FollowUserRelationUseCase>();
            services.AddScoped<IUnfollowUserRelationUseCase, UnfollowUserRelationUseCase>();
            services.AddScoped<IGetFollowersUserRelationUseCase, GetFollowersUserRelationUseCase>();
            services.AddScoped<IGetFollowingUserRelationUseCase, GetFollowingUserRelationUseCase>();
            services.AddScoped<ICreateCommentUseCase, CreateCommentUseCase>();
            services.AddScoped<IDeleteCommentUseCase, DeleteCommentUseCase>();
            services.AddScoped<IGetCommentsByParentsIdUseCase, GetCommentsByParentsIdUseCase>();
            services.AddScoped<IGetCommentsByAuthorIdUseCase, GetCommentsByAuthorIdUseCase>();
            services.AddScoped<IGetCommentsByParentsIdAndAuthorIdUseCase, GetCommentsByParentsIdAndAuthorIdUseCase>();
            services.AddScoped<IUpdateStatusCommentsUseCase, UpdateStatusCommentsUseCase>();
        }
    }
}
