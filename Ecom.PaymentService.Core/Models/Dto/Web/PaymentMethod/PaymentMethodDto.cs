using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Core.Models.Dto.Web.PaymentMethod
{
    public class PaymentMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
