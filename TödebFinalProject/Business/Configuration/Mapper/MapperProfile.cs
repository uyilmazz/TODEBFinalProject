using AutoMapper;
using Dto.Concrete;
using Entity.Concrete;
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
            CreateMap<CreatePersonDto, Person>();
            CreateMap<CreateApartmentDto, Apartment>();
            CreateMap<CreateBillDto, Bill>();
            CreateMap<CreateMessageDto, Message>();
            CreateMap<UpdateApartmentDto, Apartment>();
            CreateMap<UpdatePersonDto, Person>();
            CreateMap<UpdateBillDto, Bill>();
            CreateMap<CreateFeeDto,Fee>();
            CreateMap<UpdateFeeDto, Fee>();
            CreateMap<CreateApartmentBlocDto, ApartmentBloc>();
            CreateMap<CreateApartmentTypeDto, ApartmentType>();
            CreateMap<CreatePersonTypeDto, PersonType>();
            CreateMap<UpdatePersonTypeDto, PersonType>();
            CreateMap<UpdateMessageDto, Message>();
            CreateMap<Fee, UpdateFeeDto>();
            CreateMap<Bill, UpdateBillDto>();
        }
    }
}
