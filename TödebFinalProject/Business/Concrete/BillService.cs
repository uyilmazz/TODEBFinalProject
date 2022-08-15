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
    public class BillService : IBillService
    {
        private readonly IBillDal _billDal;
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public BillService(IBillDal billDal, IMapper mapper, IApartmentService apartmentService)
        {
            _billDal = billDal;
            _mapper = mapper;
            _apartmentService = apartmentService;
        }

        public IResult Add(CreateBillDto createBillDto)
        {
            var validator = new CreateBillValidator();
            var valid = validator.Validate(createBillDto);

            if (!valid.IsValid)
            {
                var messageText = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(messageText);
            }

            var bill = _mapper.Map<Bill>(createBillDto);
            var apartment = _apartmentService.Get(createBillDto.ApartmentId);
            if(apartment is null || apartment.Data.IsEmpty)
            {
                return new ErrorResult(ResultMessage.InvoiceCannot);
            }
            _billDal.Add(bill);
            return new SuccessResult(ResultMessage.BillCreated);
        }

        public IResult BulkdAdd(CreateBillDto createBillDto)
        {
            var validator = new CreateBillBulkValidator();
            var valid = validator.Validate(createBillDto);

            if (!valid.IsValid)
            {
                var messageText = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(messageText);
            }

            var bill = _mapper.Map<Bill>(createBillDto);

            var apartmentIds = _apartmentService.GetAllRented().Data.Select(p => p.Id);
            var amountPerApartment = createBillDto.Amount / apartmentIds.Count();

            foreach (var apartmentId in apartmentIds)
            {
                bill.Id = 0;
                bill.ApartmentId = apartmentId;
                bill.Amount = amountPerApartment;
                _billDal.Add(bill);
            }
 
            return new SuccessResult(ResultMessage.BulkBillCreated);
        }

        public IResult Delete(int id)
        {
            var bill = _billDal.Get(b => b.Id == id);
            if(bill is null)
            {
                return new ErrorResult(ResultMessage.BillNotFound);
            }

            _billDal.Delete(bill);  
            return new SuccessResult(ResultMessage.BillDeleted);
        }

        public IDataResult<Bill> Get(int id)
        {
            var bill = _billDal.Get(b => b.Id == id);
            if(bill is null)
            {
                return new ErrorDataResult<Bill>(ResultMessage.BillNotFound);
            }
            return new SuccessDataResult<Bill>(bill);
        }

        public IDataResult<List<Bill>> GetAll()
        {
            return new SuccessDataResult<List<Bill>>(_billDal.GetAll());
        }

        public IDataResult<List<BillDetailDto>> GetAllDetail()
        {
            return new SuccessDataResult<List<BillDetailDto>>(_billDal.GetAllBillDetailDto());
        }

        public IDataResult<List<BillDetailDto>> GetAllBillDetailDtoByUserId(int userId)
        {
            return new SuccessDataResult<List<BillDetailDto>>(_billDal.GetAllBillDetailDto(b => b.Apartment.PersonId == userId));
        }

        public IDataResult<BillDetailDto> GetBillDetailDtoById(int id)
        {
            var result = _billDal.GetBillDetailDto(b => b.Id == id);
            if(result is null)
            {
                return new ErrorDataResult<BillDetailDto>(ResultMessage.BillNotFound);
            }
            return new SuccessDataResult<BillDetailDto>(result);
        }

        public IResult Update(UpdateBillDto updateBillDto)
        {
            var validator = new UpdateBillValidator();
            var valid = validator.Validate(updateBillDto);

            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }

            var result = _billDal.Get(b => b.Id == updateBillDto.Id);
            if(result is null)
            {
                return new ErrorResult(ResultMessage.BillNotFound);
            }

            var bill = _mapper.Map<Bill>(updateBillDto);
            bill.CreateDate = result.CreateDate;
            _billDal.Update(bill);
            return new SuccessResult(ResultMessage.BillUpdated);
        }

        public IDataResult<UpdateBillDto> IsUnPaidBill(int id)
        {
            var unpaidBill = _billDal.Get(b => b.IsPaid == false && b.Id == id);
            if (unpaidBill is null)
            {
                return new ErrorDataResult<UpdateBillDto>(ResultMessage.UnPaidBillNotFound);
            }
            var updateBillDto = _mapper.Map<UpdateBillDto>(unpaidBill);
            return new SuccessDataResult<UpdateBillDto>(updateBillDto);
        }

        public IDataResult<List<BillDetailDto>> GetAllUnPaidBillDetailDto()
        {
            return new SuccessDataResult<List<BillDetailDto>>(_billDal.GetAllBillDetailDto(b => b.IsPaid == false));
        }
    }
}
