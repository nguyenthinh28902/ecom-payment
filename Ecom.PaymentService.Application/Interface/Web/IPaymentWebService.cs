using Ecom.PaymentService.Core.Enum;
using Ecom.PaymentService.Core.Models;
using Ecom.PaymentService.Core.Models.Dto.Web;
using Ecom.PaymentService.Core.Models.Dto.Web.Payment;
using Ecom.PaymentService.Core.Models.Dto.Web.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Application.Interface.Web
{
    public interface IPaymentWebService
    {
        Task<Result<List<PaymentMethodDto>>> GetActivePaymentMethodsAsync();
        Task<Result<PaymentResponse>> ProcessPaymentAsync(OrderRequestDto request);
        Task<bool> UpdateTransactionAfterCreatePayPalAsync(string transactionOrderCode, string payPalOrderId, PaymentStatus paymentStatus);
        Task<Result<TransactionDto>> GetTransactionByOrderIdAsync(int orderId);
    }
}
