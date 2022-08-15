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
    public class PersonTypeService : IPersonTypeService
    {
        private readonly IPersonTypeDal _personTypeDal;
        private readonly IMapper _mapper;

        public PersonTypeService(IPersonTypeDal personTypeDal, IMapper mapper)
        {
            _personTypeDal = personTypeDal;
            _mapper = mapper;
        }

        public IResult Add(CreatePersonTypeDto createPersonTypeDto)
        {
            var validator = new CreatePersonTypeValidator();
            var valid = validator.Validate(createPersonTypeDto);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var personType = _personTypeDal.Get(p => p.Type == createPersonTypeDto.Name);
            if(personType is not null)
            {
                return new ErrorResult(ResultMessage.UserTypeAlreadyExits);
            }
            personType = _mapper.Map<PersonType>(createPersonTypeDto);
            _personTypeDal.Add(personType);
            return new SuccessResult(ResultMessage.UserTypeAdded);
        }

        public IResult Delete(int id)
        {
            var personType = _personTypeDal.Get(p => p.Id == id);   
            if(personType is null)
            {
                return new ErrorResult(ResultMessage.UserTypeNotFound);
            }
            _personTypeDal.Delete(personType);
            return new SuccessResult(ResultMessage.UserTypeDeleted);
        }

        public IDataResult<PersonType> Get(int id)
        {
            var personType = _personTypeDal.Get(p => p.Id == id);
            if(personType is null)
            {
                return new ErrorDataResult<PersonType>(ResultMessage.UserTypeNotFound);
            }
            return new SuccessDataResult<PersonType>(personType);
        }

        public IDataResult<List<PersonType>> GetAll()
        {
            return new SuccessDataResult<List<PersonType>>(_personTypeDal.GetAll());
        }

        public IResult Update(UpdatePersonTypeDto updatePersonTypeDto)
        {
            var validator = new UpdatePersonTypeValidator();
            var valid = validator.Validate(updatePersonTypeDto);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var personType = _personTypeDal.Get(p => p.Id == updatePersonTypeDto.Id);
            if(personType is null)
            {
                return new ErrorResult(ResultMessage.UserTypeNotFound);
            }

            personType = _mapper.Map<PersonType>(updatePersonTypeDto);
            _personTypeDal.Update(personType);
            return new SuccessResult(ResultMessage.UserTypeUpdated);
        }
    }
}
