using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.PayPal
{
    public class PayPalConfiguration
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string Mode { get; set; } = "sandbox"; // sandbox hoặc live
    }
}
