using Ecom.PaymentService.Application.Interface.Web;
using Ecom.PaymentService.Core.Models;
using Ecom.PaymentService.Core.Models.Auth;
using Ecom.PaymentService.Core.Models.Dto.Web.PaymentMethod;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.PaymentService.Api.Controller.Web
{
    [Route("api/thanh-toan")]
    [ApiController]
    [Authorize(PolicyNames.PaymentReadWeb)]
    public class PaymentWebController : ControllerBase
    {
        private readonly IPaymentWebService _paymentService;
        private readonly ILogger<PaymentWebController> _logger;

        public PaymentWebController(IPaymentWebService paymentService, ILogger<PaymentWebController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        /// <summary>
        /// Lấy danh sách các phương thức thanh toán đang hoạt động (PayPal, VNPAY, COD...)
        /// </summary>
        /// <returns>Danh sách phương thức thanh toán dưới dạng DTO</returns>
        /// <response code="200">Trả về danh sách thành công</response>
        /// <response code="400">Có lỗi xảy ra trong quá trình lấy dữ liệu</response>
        [HttpGet("phuong-thuc-thanh-toan")]
        [ProducesResponseType(typeof(Result<List<PaymentMethodDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetActiveMethods()
        {
            _logger.LogInformation("Lấy danh sách phương thức thanh toán đang hoạt động");

            // Chỉ comment dòng quan trọng: Gọi service lấy dữ liệu đã được ProjectTo sang DTO để tối ưu Query
            var result = await _paymentService.GetActivePaymentMethodsAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
