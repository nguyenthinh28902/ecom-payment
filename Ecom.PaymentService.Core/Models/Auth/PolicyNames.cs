using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Auth
{
    public static class PolicyNames
    {
        public const string PaymentRead = "PaymentReadPolicy";
        public const string PaymentReadWeb = "PaymentReadWebPolicy";
        public const string PaymentWriteWeb = "PaymentWriteWebPolicy";
        public const string PaymentWrite = "PaymentWritePolicy";
        public const string Internal = "InternalPolicy";
    }
}
