using AutoMapper;
using Business.Abstract;
using Business.Configuration.Validator.FluentValidation;
using Business.Contants.Message;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Dto.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentDal _apartmenDal;
        private readonly IMapper _mapper;

        public ApartmentService(IApartmentDal apartmenDal, IMapper mapper)
        {
            _apartmenDal = apartmenDal;
            _mapper = mapper;
        }

        public IResult Add(CreateApartmentDto createApartmentDto)
        {
            var validator = new CreateApartmentValidator();
            var valid = validator.Validate(createApartmentDto);

            if (!valid.IsValid)
            {
                var messageText = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(messageText);
            }

            var result = _apartmenDal.Get(a => a.BlocId == createApartmentDto.BlocId && a.ApartmentNumber == createApartmentDto.ApartmentNumber);
            if(result is not null)
            {
                return new ErrorResult(ResultMessage.ApartmentAlreadyExists);
            }

            var apartment = _mapper.Map<Apartment>(createApartmentDto);
            if(createApartmentDto.PersonId > 0)
            {
                apartment.IsEmpty = false;
            }

            _apartmenDal.Add(apartment);
            return new SuccessResult(ResultMessage.ApartmentAdded);
        }

        public IResult Delete(int id)
        {
            var apartment = _apartmenDal.Get(a => a.Id == id);
            if(apartment is null)
            {
                return new ErrorResult(ResultMessage.ApartmentNotFound);
            }
            _apartmenDal.Delete(apartment);
            return new SuccessResult(ResultMessage.ApartmentDeleted);
        }

        public IDataResult<Apartment> Get(int id)
        {
            var apartment = _apartmenDal.Get(a => a.Id == id);
            if(apartment is null)
            {
                return new ErrorDataResult<Apartment>(ResultMessage.ApartmentNotFound);
            }
            return new SuccessDataResult<Apartment>(apartment);
        }

        public IDataResult<List<Apartment>> GetAll()
        {
            return new SuccessDataResult<List<Apartment>>(_apartmenDal.GetAll());
        }

        public IDataResult<List<ApartmentDetailDto>> GetAllDetail()
        {
            return new SuccessDataResult<List<ApartmentDetailDto>>(_apartmenDal.GetAllApartmentDetail());
        }

        public IDataResult<List<Apartment>> GetAllRented()
        {
            return new SuccessDataResult<List<Apartment>>(_apartmenDal.GetAll(a => a.IsEmpty == false));
        }

        public IDataResult<ApartmentDetailDto> GetDetail(int id)
        {
            var apartment = _apartmenDal.GetApartmentDetail(id);
            if (apartment is null)
            {
                return new ErrorDataResult<ApartmentDetailDto>(ResultMessage.ApartmentNotFound);
            }
            return new SuccessDataResult<ApartmentDetailDto>(apartment);
        }

        public IResult Update(UpdateApartmentDto updateApartmentDto)
        {
            var validator = new UpdateApartmentValidator();
            var valid = validator.Validate(updateApartmentDto);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var result = _apartmenDal.Get(a => a.Id == updateApartmentDto.Id);
            if(result is null)
            {
                return new ErrorResult(ResultMessage.ApartmentNotFound);
            }

            var apartment = _mapper.Map<Apartment>(updateApartmentDto);
            _apartmenDal.Update(apartment);
            return new SuccessResult(ResultMessage.ApartmentUpdated);
        }
    }
}
