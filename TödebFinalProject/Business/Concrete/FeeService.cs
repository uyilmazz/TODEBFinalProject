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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FeeService : IFeeService
    {
        private readonly IFeeDal _feeDal;
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;


        public FeeService(IFeeDal feeDal, IMapper mapper, IApartmentService apartmentService)
        {
            _feeDal = feeDal;
            _mapper = mapper;
            _apartmentService = apartmentService;
        }

        public IResult Add(CreateFeeDto createFeeDto)
        {
            var validator = new CreateFeeValidator();
            var valid = validator.Validate(createFeeDto);

            if (!valid.IsValid)
            {
                var messageText = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(messageText);
            }

            var fee = _mapper.Map<Fee>(createFeeDto);
            var apartment = _apartmentService.Get(createFeeDto.ApartmentId);
            if(apartment is null || apartment.Data.IsEmpty)
            {
                return new ErrorResult(ResultMessage.InvoiceCannot);
            }

            _feeDal.Add(fee);
            return new SuccessResult(ResultMessage.FeeCreated);
        }

        public IResult BulkdAdd(CreateFeeDto createFeeDto)
        {
            var validator = new CreateFeeBulkValidator();
            var valid = validator.Validate(createFeeDto);

            if (!valid.IsValid)
            {
                var messageText = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(messageText);
            }

            var fee = _mapper.Map<Fee>(createFeeDto);

            var apartmentIds = _apartmentService.GetAllRented().Data.Select(p => p.Id);
            var amountPerApartment = createFeeDto.Amount / apartmentIds.Count();

            foreach (var apartmentId in apartmentIds)
            {
                fee.Id = 0;
                fee.ApartmentId = apartmentId;
                fee.Amount = amountPerApartment;
                _feeDal.Add(fee);
            }

            return new SuccessResult(ResultMessage.BulkFeeCreated);
        }

        public IResult Delete(int id)
        {
            var fee = _feeDal.Get(b => b.Id == id);
            if (fee is null)
            {
                return new ErrorResult(ResultMessage.FeeNotFound);
            }

            _feeDal.Delete(fee);
            return new SuccessResult(ResultMessage.FeeDeleted);
        }

        public IDataResult<Fee> Get(int id)
        {
            var fee = _feeDal.Get(b => b.Id == id);
            if (fee is null)
            {
                return new ErrorDataResult<Fee>(ResultMessage.FeeNotFound);
            }
            return new SuccessDataResult<Fee>(fee);
        }

        public IDataResult<List<Fee>> GetAll()
        {
            return new SuccessDataResult<List<Fee>>(_feeDal.GetAll());
        }

        public IDataResult<List<FeeDetailDto>> GetAllDetail()
        {
            return new SuccessDataResult<List<FeeDetailDto>>(_feeDal.GetAllFeeDetailDto());
        }

        public IDataResult<List<FeeDetailDto>> GetAllFeeDetailDtoByUserId(int userId)
        {
            return new SuccessDataResult<List<FeeDetailDto>>(_feeDal.GetAllFeeDetailDto(f => f.Apartment.PersonId == userId));
        }

        public IDataResult<List<FeeDetailDto>> GetAllUnPaidFeeDetail()
        {
            return new SuccessDataResult<List<FeeDetailDto>>(_feeDal.GetAllFeeDetailDto(f => f.IsPaid == false));
        }

        public IDataResult<FeeDetailDto> GetFeeDetailDtoById(int id)
        {
            var result = _feeDal.GetFeeDetailDto(f => f.Id == id);
            if(result is null)
            {
                return new ErrorDataResult<FeeDetailDto>(ResultMessage.FeeNotFound);
            }
            return new SuccessDataResult<FeeDetailDto>(result);
        }

        public IDataResult<UpdateFeeDto> IsUnPaidFee(int id)
        {
            var unpaidFee = _feeDal.Get(f => f.IsPaid == false && f.Id == id);
            if(unpaidFee is null)
            {
                return new ErrorDataResult<UpdateFeeDto>(ResultMessage.UnPaidFeeNotFound);
            }
            var updateFeeDto = _mapper.Map<UpdateFeeDto>(unpaidFee);
            return new SuccessDataResult<UpdateFeeDto>(updateFeeDto);
        }

        public IResult Update(UpdateFeeDto updateFeeDto)
        {
            var validator = new UpdateFeeValidator();
            var valid = validator.Validate(updateFeeDto);

            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var result = _feeDal.Get(f => f.Id == updateFeeDto.Id);
            if (result is null)
            {
                return new ErrorResult(ResultMessage.FeeNotFound);
            }

            var fee = _mapper.Map<Fee>(updateFeeDto);
            fee.CreatedDate = result.CreatedDate;
            _feeDal.Update(fee);
            return new SuccessResult(ResultMessage.FeeUpdated);
        }
    }
}

