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
    public class ApartmentBlocService : IApartmentBlocService
    {
        private readonly IApartmentBlocDal _apartmentBlocDal;
        private readonly IMapper _mapper;
        public ApartmentBlocService(IApartmentBlocDal apartmentBlocDal, IMapper mapper)
        {
            _apartmentBlocDal = apartmentBlocDal;
            _mapper = mapper;
        }

        public IResult Add(CreateApartmentBlocDto createApartmentBlocDto)
        {
            var validator = new CreateApartmentBlocValidator();
            var valid = validator.Validate(createApartmentBlocDto);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var apartmentBloc = _apartmentBlocDal.Get(a => a.Name == createApartmentBlocDto.Name);
            if(apartmentBloc is not null)
            {
                return new ErrorResult(ResultMessage.ApartmentBlocAlreadyExists);
            }

            apartmentBloc = _mapper.Map<ApartmentBloc>(createApartmentBlocDto);
            _apartmentBlocDal.Add(apartmentBloc);
            return new SuccessResult(ResultMessage.ApartmentBlocAdded);
        }

        public IResult Delete(int id)
        {
            var apartmentBloc = _apartmentBlocDal.Get(a => a.Id == id);
            if(apartmentBloc is null)
            {
                return new ErrorResult(ResultMessage.ApartmentBlocNotFound);
            }
            _apartmentBlocDal.Delete(apartmentBloc);
            return new SuccessResult(ResultMessage.ApartmentBlocDeleted);
        }

        public IDataResult<ApartmentBloc> Get(int id)
        {
            var apartmentBloc = _apartmentBlocDal.Get(a => a.Id == id);
            if(apartmentBloc is null)
            {
                return new ErrorDataResult<ApartmentBloc>(ResultMessage.ApartmentBlocNotFound);
            }
            return new SuccessDataResult<ApartmentBloc>(apartmentBloc);
        }

        public IDataResult<List<ApartmentBloc>> GetAll()
        {
            return new SuccessDataResult<List<ApartmentBloc>>(_apartmentBlocDal.GetAll());
        }

        public IResult Update(ApartmentBloc updateApartmentBloc)
        {
            var validator = new UpdateApartmentBlocValidator();
            var valid = validator.Validate(updateApartmentBloc);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var  apartmentBloc = _apartmentBlocDal.Get(a => a.Id == updateApartmentBloc.Id);
            if(apartmentBloc is null)
            {
                return new ErrorResult(ResultMessage.ApartmentBlocNotFound);
            }

            _apartmentBlocDal.Update(updateApartmentBloc);
            return new SuccessResult(ResultMessage.ApartmentBlocUpdated);

        }
    }
}
