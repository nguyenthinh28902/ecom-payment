using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Enum
{
    public enum PaymentStatus : byte
    {
        Pending = 0,    // Chờ xác nhận/Chờ thanh toán
        Success = 1,    // Thanh toán thành công
        Failed = 2,     // Thanh toán thất bại
        Canceled = 3,   // Khách hủy thanh toán
        Refunded = 4    // Đã hoàn tiền
    }
}
