using AutoMapper;
using DTO.Concrete;
using Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCreditCardDto, CreditCard>();
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<CreatePaidTypeDto, PaidType>();
        }
    }
}
