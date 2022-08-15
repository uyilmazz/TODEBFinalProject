using AutoMapper;
using Business.Abstract;
using Business.Configuration.Validator.FluentValidation;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Absract;
using DTO.Concrete;
using Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaidTypeService : IPaidTypeService
    {
        private readonly IPaidTypeDal _paidTypeDal;
        private readonly IMapper _mapper;
        public PaidTypeService(IPaidTypeDal paidTypeDal, IMapper mapper)
        {
            _paidTypeDal = paidTypeDal;
            _mapper = mapper;
        }

        public IResult Add(CreatePaidTypeDto createPaidTypeDto)
        {
            var validator = new CreatePaidTypeValidator();
            var valid = validator.Validate(createPaidTypeDto);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var paidType = _paidTypeDal.Get(p => p.Name == createPaidTypeDto.Name);
            if(paidType is not null)
            {
                return new ErrorResult(ResultMessage.PaidTypeAlreayExists);
            }

            paidType = _mapper.Map<PaidType>(createPaidTypeDto);
            _paidTypeDal.Add(paidType);
            return new SuccessResult(ResultMessage.PaidTypeAdded);
        }

        public IResult Delete(ObjectId id)
        {
            var paidType = _paidTypeDal.Get(id);
            if (paidType is null)
            {
                return new ErrorResult(ResultMessage.PaidTypeNotFound);
            }
            _paidTypeDal.Delete(id);
            return new SuccessResult(ResultMessage.PaidTypeDeleted);
        }

        public IDataResult<PaidType> Get(ObjectId id)
        {
            var paidType = _paidTypeDal.Get(id);
            if (paidType is null)
            {
                return new ErrorDataResult<PaidType>(ResultMessage.PaidTypeNotFound);
            }
            return new SuccessDataResult<PaidType>(paidType);
        }

        public IDataResult<List<PaidType>> GetAll()
        {
            return new SuccessDataResult<List<PaidType>>(_paidTypeDal.GetAll().ToList());
        }

        public IDataResult<PaidType> GetByName(string name)
        {
            var paidType = _paidTypeDal.Get(p => p.Name == name);
            if (paidType is null)
                return new ErrorDataResult<PaidType>(ResultMessage.PaidTypeNotFound);
            return new SuccessDataResult<PaidType>(paidType);
        }

        public IResult Update(PaidType model)
        {
            var paidType = _paidTypeDal.Get(model.Id);
            if (paidType is null)
            {
                return new ErrorResult(ResultMessage.PaidTypeNotFound);
            }
            _paidTypeDal.Update(model);
            return new SuccessResult(ResultMessage.PaidTypeUpdated);
        }
    }
}
