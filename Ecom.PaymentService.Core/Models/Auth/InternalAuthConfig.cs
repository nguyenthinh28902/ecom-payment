using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Auth
{
    public class InternalAuthConfig
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
