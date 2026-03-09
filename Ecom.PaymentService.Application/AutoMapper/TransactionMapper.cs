using AutoMapper;
using Ecom.PaymentService.Core.Entities;
using Ecom.PaymentService.Core.Models.Dto.Cms;
using Ecom.PaymentService.Core.Models.Dto.Web;
using Ecom.PaymentService.Grpc;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.PaymentService.Application.AutoMapper
{
    public class TransactionMapper : Profile
    {
        public TransactionMapper()
        {

            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name));
            //TransactionManagerDto 
            CreateMap<Transaction, TransactionManagerDto>()
                .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name));
            CreateMap<TransactionLog, TransactionLogManagerDto>();

            CreateMap<TransactionLogManagerDto, TransactionLogManagerGrpc>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src =>
                    src.CreatedAt.HasValue ? Timestamp.FromDateTime(DateTime.SpecifyKind(src.CreatedAt.Value, DateTimeKind.Utc)) : null));

            CreateMap<TransactionManagerDto, TransactionManagerGrpcResponse>()
              .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => (double)src.Amount))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? 0))
              .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName ?? string.Empty))
              .ForMember(dest => dest.ExternalTransactionId, opt => opt.MapFrom(src => src.ExternalTransactionId ?? string.Empty))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src =>
                  src.CreatedAt.HasValue
                      ? Timestamp.FromDateTime(DateTime.SpecifyKind(src.CreatedAt.Value, DateTimeKind.Utc))
                      : new Timestamp()))
              .ForMember(dest => dest.FinishedAt, opt => opt.MapFrom(src =>
                  src.FinishedAt.HasValue
                      ? Timestamp.FromDateTime(DateTime.SpecifyKind(src.FinishedAt.Value, DateTimeKind.Utc))
                      : new Timestamp()))
              .ForMember(dest => dest.TransactionLog, opt => opt.MapFrom(src => src.TransactionLog));
        }
    }
}
