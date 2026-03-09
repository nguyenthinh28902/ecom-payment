using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Dto.Web
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = null!;
        public int PaymentMethodId { get; set; }

        // Chỉ comment dòng quan trọng: Lấy tên phương thức thanh toán từ entity liên kết
        public string? PaymentMethodName { get; set; }

        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? ExternalTransactionId { get; set; }
        public string? PaymentMetadata { get; set; }
        public byte? Status { get; set; }

        // Chỉ comment dòng quan trọng: Trả về tên trạng thái (ví dụ: Pending, Success) dựa trên Enum
        public string? StatusName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}