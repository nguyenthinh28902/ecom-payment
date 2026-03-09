using AutoMapper;
using Ecom.PaymentService.Core.Entities;
using Ecom.PaymentService.Core.Models.Dto.Web.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Application.AutoMapper
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<PaymentMethod, PaymentMethodDto>();
        }
    }
}
