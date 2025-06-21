using Footstep.Application.AutoMapper;
using Footstep.Application.UseCases.Traces.Create;
using Footstep.Application.UseCases.Traces.Delete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
