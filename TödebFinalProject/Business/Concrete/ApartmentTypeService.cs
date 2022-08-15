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
    public class ApartmentTypeService : IApartmentTypeService
    {
        private readonly IApartmentTypeDal _apartmentTypeDal;
        private readonly IMapper _mapper;

        public ApartmentTypeService(IApartmentTypeDal apartmentType, IMapper mapper)
        {
            _apartmentTypeDal = apartmentType;
            _mapper = mapper;
        }

        public IResult Add(CreateApartmentTypeDto createApartmentTypeDto)
        {
            var validator = new CreateApartmentTypeValidator();
            var valid = validator.Validate(createApartmentTypeDto);

            if (!valid.IsValid)
            {
                var message = string.Join(",",valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var apartmentType = _apartmentTypeDal.Get(a => a.Name == createApartmentTypeDto.Name);
            if(apartmentType is not null)
            {
                return new ErrorResult(ResultMessage.ApartmentAlreadyExists);
            }

            var newApartmentType = _mapper.Map<ApartmentType>(createApartmentTypeDto);
            _apartmentTypeDal.Add(newApartmentType);
            return new SuccessResult(ResultMessage.ApartmentTypeAdded);

        }

        public IResult Delete(int id)
        {
            var apartmentType = _apartmentTypeDal.Get(a => a.Id == id);
            if(apartmentType is null)
            {
                return new ErrorResult(ResultMessage.ApartmentTypeNotFound);
            }
            _apartmentTypeDal.Delete(apartmentType);
            return new SuccessResult(ResultMessage.ApartmentTypeDeleted);
        }

        public IDataResult<ApartmentType> Get(int id)
        {
            var apartmentType = _apartmentTypeDal.Get(a => a.Id == id);
            if(apartmentType is null)
            {
                return new ErrorDataResult<ApartmentType>(ResultMessage.ApartmentTypeNotFound);
            }
            return new SuccessDataResult<ApartmentType>(apartmentType);
        }

        public IDataResult<List<ApartmentType>> GetAll()
        {
            return new SuccessDataResult<List<ApartmentType>>(_apartmentTypeDal.GetAll());
        }

        public IResult Update(ApartmentType updateApartmentType)
        {
            var validator = new UpdateApartmentTypeValidator();
            var valid = validator.Validate(updateApartmentType);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }
            var apartmentType = _apartmentTypeDal.Get(a => a.Id == updateApartmentType.Id);
            if(apartmentType is null)
            {
                return new ErrorResult(ResultMessage.ApartmentTypeNotFound);
            }
            _apartmentTypeDal.Update(updateApartmentType);
            return new SuccessResult(ResultMessage.ApartmentTypeUpdated);
        }
    }
}
