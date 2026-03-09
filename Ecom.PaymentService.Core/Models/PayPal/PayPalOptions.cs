using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.PayPal
{
    public class PayPalOptions
    {
        public const string PayPal = "PayPal";
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string PaypalEnvironment { get; set; } = "sandbox";
        public string ReturnUrl { get; set; } = string.Empty;
        public string CancelUrl { get; set; } = string.Empty;

        // Chỉ comment dòng quan trọng: Tự động xác định BaseUrl dựa trên Mode (sandbox/production)
        public string BaseUrl => PaypalEnvironment.ToLower() == "production"
            ? "https://api-m.paypal.com"
            : "https://api-m.sandbox.paypal.com";
    }
}
