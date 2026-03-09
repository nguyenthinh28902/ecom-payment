using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Application.Interface.Auth
{
    public interface IBaseService
    {
        void EnsurePermission(string permission);
    }
}
