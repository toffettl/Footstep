using Footstep.Application.AutoMapper;
using Footstep.Application.UseCases.Traces.Create;
using Footstep.Application.UseCases.Traces.Delete;
using Footstep.Application.UseCases.Traces.GetAll;
using Footstep.Application.UseCases.Traces.GetById;
using Footstep.Application.UseCases.Traces.GetByRay;
using Footstep.Application.UseCases.Traces.Update;
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
            services.AddScoped<ICreateTraceUseCase, CreateTraceUseCase>();
            services.AddScoped<IDeleteTraceUseCase, DeleteTraceUseCase>();
            services.AddScoped<IUpdateTraceUseCase, UpdateTraceUseCase>();
            services.AddScoped<IGetByIdTraceUseCase, GetByIdTraceUseCase>();
            services.AddScoped<IGetAllTraceUseCase, GetAllTraceUseCase>();
            services.AddScoped<IGetTracesByRayUseCase, GetTracesByRayUseCase>();
        }
    }
}
