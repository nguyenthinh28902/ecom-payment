using Ecom.PaymentService.Core.Abstractions.Persistence;
using Ecom.PaymentService.Core.Models.Connection;
using Ecom.PaymentService.Infrastructure.DbContexts;
using Ecom.PaymentService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecom.PaymentService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionInfrastructure(this IServiceCollection services,
         IConfiguration configuration)
        {
            ConnectionStrings.EcomPaymentDb = configuration.GetConnectionString("EcommerceProduct") ?? string.Empty;
            // Đăng ký DbContext sử dụng SQL Server
            services.AddDbContext<EcomPaymentDbContext>(options =>
                options.UseSqlServer(ConnectionStrings.EcomPaymentDb));

            //add kiến trúc repo and UoW
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
