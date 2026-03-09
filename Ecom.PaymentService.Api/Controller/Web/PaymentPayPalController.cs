using Ecom.PaymentService.Application.Interface.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.PaymentService.Api.Controller.Web
{
    [Route("api/thanh-toan-paypal")]
    [ApiController]
    public class PaymentPayPalController : ControllerBase
    {
        private readonly ILogger<PaymentPayPalController> _logger;
        private readonly IPaymentWebService _paymentWebService;

        public PaymentPayPalController(ILogger<PaymentPayPalController> logger, IPaymentWebService paymentWebService)
        {
            _logger = logger;
            _paymentWebService = paymentWebService;
        }

        [HttpGet("xac-nhan")]
        public async Task<IActionResult> Confirm()
        {
            _logger.LogInformation("Xử lý thanh toán cho đơn hàng");

            var result = await _paymentWebService.UpdateTransactionAfterCreatePayPalAsync("","",Core.Enum.PaymentStatus.Success);
            // Chỉ comment dòng quan trọng: Đây là điểm vào chính để xử lý thanh toán, sẽ gọi service tương ứng dựa trên phương thức thanh toán
            return Ok(new { Message = "Chức năng xử lý thanh toán đang được phát triển." });
        }

        [HttpGet("huy-thanh-toan")]
        public async Task<IActionResult> Cancel()
        {
            _logger.LogInformation("Xử lý thanh toán cho đơn hàng");

            var result = await _paymentWebService.UpdateTransactionAfterCreatePayPalAsync("", "", Core.Enum.PaymentStatus.Canceled);
            // Chỉ comment dòng quan trọng: Đây là điểm vào chính để xử lý thanh toán, sẽ gọi service tương ứng dựa trên phương thức thanh toán
            return Ok(new { Message = "Chức năng xử lý thanh toán đang được phát triển." });
        }
    }
}
