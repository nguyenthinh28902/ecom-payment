using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Dto.Web.Payment
{
    public class PaymentResponse
    {
        public string OrderCode { get; set; } = null!;
        public string ApprovalUrl { get; set; } = null!; // Link để khách click thanh toán
        public bool IsSuccess { get; set; }
    }
}
