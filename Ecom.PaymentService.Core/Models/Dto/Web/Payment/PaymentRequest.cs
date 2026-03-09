using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Dto.Web.Payment
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string OrderCode { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
