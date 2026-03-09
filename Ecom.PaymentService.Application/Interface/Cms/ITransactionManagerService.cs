using Ecom.PaymentService.Core.Models;
using Ecom.PaymentService.Core.Models.Dto.Cms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Application.Interface.Cms
{
    public interface ITransactionManagerService
    {
        Task<Result<TransactionManagerDto>> GetTransactionManagerByOrderIdAsync(int orderId);
    }
}
