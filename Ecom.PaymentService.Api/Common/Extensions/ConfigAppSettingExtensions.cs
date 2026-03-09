

using Ecom.PaymentService.Core.Models.Auth;
using Ecom.PaymentService.Core.Models.Connection;

namespace Ecom.PaymentService.Common.Extensions
{
    public static class ConfigAppSettingExtensions
    {
        public static IServiceCollection AddConfigAppSettingExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisConnection>(configuration.GetSection("RedisConnection"));
            return services;
        }
    }
}
