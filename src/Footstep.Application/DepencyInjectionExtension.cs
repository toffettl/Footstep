using Footstep.Application.AutoMapper;
using Footstep.Application.UseCases.Users.Register;
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
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IRegisterUserUseCase))
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("UseCase")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
