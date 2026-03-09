using Ecom.PaymentService.Application.Interface.Cms;
using Ecom.PaymentService.Application.Service.Cms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Application.DependencyInjection
{
    public static class DependencyInjectionCmsApplication
    {
        public static IServiceCollection AddCmsApplication(this IServiceCollection services)
        {
            services.AddScoped<ITransactionManagerService, TransactionManagerService>();
            return services;
        }
    }
}
