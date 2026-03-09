using Ecom.PaymentService.Application.Interface.Web;
using Ecom.PaymentService.Application.Service.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Application.DependencyInjection
{
    public static class DependencyInjectionWebApplication
    {
        public static IServiceCollection AddWebApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPaymentWebService, PaymentWebService>();   
           

            // Đăng ký gRPC Client
            //services.AddGrpcClient<ProductGrpc.ProductGrpcClient>(o =>
            //{
            //    o.Address = new Uri(productUrl);
            //})
            //.AddCallCredentials(async (context, metadata, serviceProvider) =>
            //{
            //    // Chỉ comment dòng này: Lấy Service hiện tại để lấy ID người dùng
            //    var currentCustomer = serviceProvider.GetRequiredService<ICurrentCustomerService>();

            //    if (currentCustomer.IsAuthenticated)
            //    {
            //        // Chỉ comment dòng này: Chuyển tiếp ID từ Gateway sang Service tiếp theo qua gRPC Metadata
            //        metadata.Add("X-User-Id", currentCustomer.Id.ToString());

            //        if (!string.IsNullOrEmpty(currentCustomer.Email))
            //            metadata.Add("X-User-Email", currentCustomer.Email);
            //        if (!string.IsNullOrEmpty(currentCustomer.PhoneNumber))
            //            metadata.Add("X-User-Phone", currentCustomer.PhoneNumber);
            //    }

            //    var internalKey = configuration["InternalGrpcApiKey"] ?? string.Empty;
            //    // Thêm Key nội bộ để xác thực Service-to-Service (nếu có)
            //    metadata.Add("x-internal-key",internalKey);

            //    await Task.CompletedTask;
            //}); 
            return services;
        }
    }
}
