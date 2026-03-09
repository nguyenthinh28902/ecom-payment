using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Dto.Cms
{
    public class TransactionLogManagerDto
    {
        public int Id { get; set; }
        public string LogContent { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

    }
}
