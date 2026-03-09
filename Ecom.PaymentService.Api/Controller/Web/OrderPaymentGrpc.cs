using AutoMapper;
using Ecom.PaymentService.Application.Interface.Cms;
using Ecom.PaymentService.Application.Interface.Web;
using Ecom.PaymentService.Core.Models.Dto.Web.Payment;
using Ecom.PaymentService.Grpc;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ecom.PaymentService.Api.Controller.Web
{
    public class OrderPaymentGrpc : PaymentGrpc.PaymentGrpcBase
    {
        private readonly ITransactionManagerService _transactionManagerService;
        private readonly IPaymentWebService _paymentWebService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderPaymentGrpc> _logger; // Khai báo logger

        public OrderPaymentGrpc(IPaymentWebService paymentWebService, ILogger<OrderPaymentGrpc> logger,
            ITransactionManagerService transactionManagerService,
            IMapper mapper)
        {
            _paymentWebService = paymentWebService;
            _logger = logger;
            _transactionManagerService = transactionManagerService;
            _mapper = mapper;
        }

        public override async Task<PaymentGrpcResponse> ProcessPayment(PaymentGrpcRequest request, ServerCallContext context)
        {
            // 1. Chỉ comment dòng quan trọng: Log bắt đầu xử lý thanh toán từ Order Service gọi sang
            _logger.LogInformation("gRPC: Bắt đầu xử lý thanh toán đơn hàng {OrderCode} ({Amount} {Currency})", request.OrderCode, request.Amount, request.Currency);

            var dto = new OrderRequestDto
            {
                OrderId = request.OrderId,
                OrderCode = request.OrderCode,
                Amount = (decimal)request.Amount,
                Currency = request.Currency,
                PaymentMethodCode = request.PaymentMethodCode,
                Description = request.Description
            };

            var result = await _paymentWebService.ProcessPaymentAsync(dto);

            if (!result.IsSuccess)
            {
                // 2. Chỉ comment dòng quan trọng: Log lỗi nếu xử lý thanh toán thất bại
                _logger.LogWarning("gRPC: Xử lý thanh toán thất bại cho đơn hàng {OrderCode}. Lý do: {Message}", request.OrderCode, result.Noti);
            }
            else
            {
                _logger.LogInformation("gRPC: Xử lý thanh toán thành công cho đơn hàng {OrderCode}", request.OrderCode);
            }

            return new PaymentGrpcResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Noti,
                ApprovalUrl = result.Data?.ApprovalUrl ?? "",
                OrderCode = result.Data?.OrderCode ?? ""
            };
        }

        public override async Task<TransactionGrpcResponse> GetTransactionByOrderId(GetTransactionRequest request, ServerCallContext context)
        {
            // 3. Chỉ comment dòng quan trọng: Log truy vấn thông tin giao dịch
            _logger.LogInformation("gRPC: Truy vấn thông tin giao dịch cho OrderId: {OrderId}", request.OrderId);

            var result = await _paymentWebService.GetTransactionByOrderIdAsync(request.OrderId);

            if (!result.IsSuccess || result.Data == null)
            {
                _logger.LogWarning("gRPC: Không tìm thấy giao dịch cho OrderId: {OrderId}", request.OrderId);
                return new TransactionGrpcResponse
                {
                    IsSuccess = false,
                    Message = result.Noti ?? "Không tìm thấy giao dịch."
                };
            }

            var data = result.Data;
            _logger.LogInformation("gRPC: Đã tìm thấy giao dịch {TransactionId} cho đơn hàng {OrderCode}", data.ExternalTransactionId, data.OrderCode);

            return new TransactionGrpcResponse
            {
                IsSuccess = true,
                Message = result.Noti,
                Id = data.Id,
                OrderId = data.OrderId,
                OrderCode = data.OrderCode,
                Amount = (double)data.Amount,
                Currency = data.Currency ?? "",
                PaymentMethodName = data.PaymentMethodName ?? "",
                ExternalTransactionId = data.ExternalTransactionId ?? "",
                StatusName = data.StatusName ?? ""
            };
        }
        public override async Task<TransactionManagerGrpcResponse> GetTransactionByOrderIdManager(OrderTransactionGrpcRequest request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation("gRPC: Truy vấn thông tin giao dịch cho OrderId: {OrderId}", JsonSerializer.Serialize(request.OrderId));
                var result = await _transactionManagerService.GetTransactionManagerByOrderIdAsync(request.OrderId);
                if (result == null || !result.IsSuccess || result.Data == null)
                {
                    _logger.LogInformation("gRPC: Trả về: data không có dữ liệu và lỗi ");
                    return new TransactionManagerGrpcResponse();
                }
                var response = _mapper.Map<TransactionManagerGrpcResponse>(result.Data);
                return response;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }        
        }
    }
}