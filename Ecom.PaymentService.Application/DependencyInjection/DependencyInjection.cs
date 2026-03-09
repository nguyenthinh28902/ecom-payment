using Ecom.PaymentService.Application.Interface.Auth;
using Ecom.PaymentService.Application.Service.Auth;
using Ecom.PaymentService.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecom.PaymentService.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionApplication(this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddDependencyInjectionInfrastructure(configuration);
            services.AddStackExchangeRedis(configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ICurrentCustomerService, CurrentCustomerService>();
            services.AddScoped<IBaseService, BaseService>();
            services.AddCmsApplication();
            services.AddWebApplication(configuration);
            return services;
        }
    }
}
