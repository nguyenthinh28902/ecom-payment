using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecom.PaymentService.Application.Interface.Web;
using Ecom.PaymentService.Core.Abstractions.Persistence;
using Ecom.PaymentService.Core.Entities;
using Ecom.PaymentService.Core.Enum;
using Ecom.PaymentService.Core.Models;
using Ecom.PaymentService.Core.Models.Dto.Web;
using Ecom.PaymentService.Core.Models.Dto.Web.Payment;
using Ecom.PaymentService.Core.Models.Dto.Web.PaymentMethod;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecom.PaymentService.Application.Service.Web
{
    public class PaymentWebService : IPaymentWebService
    {
        private readonly ILogger<PaymentWebService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentWebService(IUnitOfWork unitOfWork, IMapper mapper
            ,ILogger<PaymentWebService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<List<PaymentMethodDto>>> GetActivePaymentMethodsAsync()
        {
            // Chỉ comment dòng quan trọng: Dùng ProjectTo để Mapper tự sinh câu lệnh SELECT đúng các cột trong DTO
            var paymentMethods = await _unitOfWork.Repository<PaymentMethod>()
                .GetAll(x => x.IsActive == true)
                .ProjectTo<PaymentMethodDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<PaymentMethodDto>>.Success(paymentMethods, "Thành công.");
        }

        public async Task<Result<PaymentResponse>> ProcessPaymentAsync(OrderRequestDto request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(); // Bắt đầu transaction để đảm bảo tính toàn vẹn dữ liệu
                // 1. Chỉ comment dòng quan trọng: Tìm ID của phương thức thanh toán dựa trên Code (PAYPAL, CASH...)
                var paymentMethod = await _unitOfWork.Repository<PaymentMethod>()
                    .FirstOrDefaultAsNoTrackingAsync(x => x.Code == request.PaymentMethodCode);

                if (paymentMethod == null)
                    return Result<PaymentResponse>.Failure("Phương thức thanh toán không hợp lệ.");

                // 2. Chỉ comment dòng quan trọng: Khởi tạo bản ghi Transaction mới với trạng thái Pending (0)
                var transaction = new Transaction
                {
                    
                    OrderId = request.OrderId,
                    OrderCode = request.OrderCode,
                    PaymentMethodId = paymentMethod.Id,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    Status = (byte)PaymentStatus.Pending, // Trạng thái chờ xác nhận
                    CreatedAt = DateTime.Now,
                    PaymentMetadata = request.Description
                };
                // 3. Chỉ comment dòng quan trọng: Ghi log khởi tạo giao dịch vào bảng TransactionLog
                var log = new TransactionLog
                {
                    LogContent = $"Khởi tạo giao dịch cho đơn hàng {request.OrderCode}. PTTT: {request.PaymentMethodCode}",
                    CreatedAt = DateTime.Now
                };
                transaction.TransactionLogs = new List<TransactionLog> { log };
                await _unitOfWork.Repository<Transaction>().AddAsync(transaction);

                // Lưu toàn bộ vào DB thông qua UnitOfWork
                await _unitOfWork.CommitAsync();

                var paymentRequest = new PaymentRequest();
                paymentRequest.Amount = transaction.Amount;
                paymentRequest.Currency = transaction.Currency;
                paymentRequest.OrderCode = transaction.OrderId.ToString();
                var response = new PaymentResponse();
                if (paymentMethod.Code == "CASH")
                {
                    response = new PaymentResponse
                    {
                        ApprovalUrl = "",
                        OrderCode = transaction.OrderCode,
                        IsSuccess = true
                    };
                }
                return Result<PaymentResponse>.Success(response, "Giao dịch đã được khởi tạo thành công. Vui lòng thanh toán để hoàn tất đơn hàng.");

            }
            catch (Exception ex)
            {
                return Result<PaymentResponse>.Failure("Không thể thanh toán ngay lúc này.");
            }
        }

        public async Task<bool> UpdateTransactionAfterCreatePayPalAsync(string transactionOrderCode, string payPalOrderId, PaymentStatus paymentStatus)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // 1. Chỉ comment dòng quan trọng: Lấy thông tin Transaction để cập nhật mã đối soát từ PayPal
                var transaction = await _unitOfWork.Repository<Transaction>().FirstOrDefaultAsNoTrackingAsync(x => x.OrderCode == transactionOrderCode);

                if (transaction == null) return false;

                transaction.ExternalTransactionId = payPalOrderId;
                

                // 2. Chỉ comment dòng quan trọng: Ghi log lưu vết mã giao dịch ngoại sàn (PayPal Order ID)
                var log = new TransactionLog
                {
                    TransactionId = transaction.Id,
                    LogContent = $"Đã liên kết với PayPal Order: {payPalOrderId}",
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.Repository<TransactionLog>().AddAsync(log);
                _unitOfWork.Repository<Transaction>().Update(transaction);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi cập nhật ExternalTransactionId cho Transaction: {Id}", transactionOrderCode);
                return false;
            }
        }
        public async Task<Result<TransactionDto>> GetTransactionByOrderIdAsync(int orderId)
        {
            // 1. Chỉ comment dòng quan trọng: Tìm giao dịch theo OrderId và dùng ProjectTo để map thẳng sang DTO
            var transaction = await _unitOfWork.Repository<Transaction>()
                .GetAll(x => x.OrderId == orderId)
                .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (transaction == null)
            {
                return Result<TransactionDto>.Failure("Không tìm thấy thông tin giao dịch cho đơn hàng này.");
            }

            // 2. Chỉ comment dòng quan trọng: Bổ sung logic hiển thị tên trạng thái dựa trên byte Status
            if (transaction.Status.HasValue)
            {
                transaction.StatusName = ((PaymentStatus)transaction.Status.Value).ToString();
            }

            return Result<TransactionDto>.Success(transaction, "Lấy thông tin giao dịch thành công.");
        }
    }
}
