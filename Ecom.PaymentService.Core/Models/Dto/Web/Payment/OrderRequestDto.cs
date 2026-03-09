using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Dto.Web.Payment
{
    public class OrderRequestDto
    {
        [Required(ErrorMessage = "OrderId không được để trống")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Mã đơn hàng không được để trống")]
        [StringLength(50)]
        public string OrderCode { get; set; } = null!;

        [Required(ErrorMessage = "Số tiền thanh toán không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Đơn vị tiền tệ không được để trống")]
        [StringLength(10)]
        public string Currency { get; set; } = "USD";

        [Required(ErrorMessage = "Mã phương thức thanh toán không được để trống")]
        public string PaymentMethodCode { get; set; } = null!;

        public string Description { get; set; } = "Thanh toán đơn hàng thương mại điện tử";
    }
}
