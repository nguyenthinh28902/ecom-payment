using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Auth
{
    public class PaymentPermission
    {
        public const string PaymentRead = "payment.read";
        public const string PaymentCreate = "payment.create";
        public const string PaymentUpdate = "payment.update";
        public const string PaymentDelete = "payment.delete";
    }
}
