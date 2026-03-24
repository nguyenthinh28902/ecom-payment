using Ecom.PaymentService.Core.Models.Connection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Infrastructure.DbContexts
{
    public partial class EcomPaymentDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Đưa logic dùng biến hằng của bạn vào đây
                optionsBuilder.UseSqlServer(ConnectionStrings.EcomPaymentDb);
            }
        }
    }
}
