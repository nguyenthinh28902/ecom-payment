using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecom.PaymentService.Application.Interface.Auth;
using Ecom.PaymentService.Application.Interface.Cms;
using Ecom.PaymentService.Core.Abstractions.Persistence;
using Ecom.PaymentService.Core.Entities;
using Ecom.PaymentService.Core.Enum;
using Ecom.PaymentService.Core.Models;
using Ecom.PaymentService.Core.Models.Auth;
using Ecom.PaymentService.Core.Models.Dto.Cms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Ecom.PaymentService.Application.Service.Cms
{
    public class TransactionManagerService : ITransactionManagerService
    {
        private readonly ILogger<TransactionManagerService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBaseService _baseService;
        public TransactionManagerService(ILogger<TransactionManagerService> logger
            ,ICurrentUserService currentUserService
            ,IUnitOfWork unitOfWork
            ,IMapper mapper
            ,IBaseService baseService) {
            _logger = logger;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _baseService = baseService;


        }

        public async Task<Result<TransactionManagerDto>> GetTransactionManagerByOrderIdAsync(int orderId)
        {
            _baseService.EnsurePermission(PaymentPermission.PaymentRead);
            // 1. Chỉ comment dòng quan trọng: Tìm giao dịch theo OrderId và dùng ProjectTo để map thẳng sang DTO
            var transaction = await _unitOfWork.Repository<Transaction>()
                .GetAll(x => x.OrderId == orderId)
                .ProjectTo<TransactionManagerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (transaction == null) return Result<TransactionManagerDto>.Failure("Không có dữ liệu");
            return Result<TransactionManagerDto>.Success(transaction, "Lấy thông tin giao dịch thành công.");
        }
    }
}
