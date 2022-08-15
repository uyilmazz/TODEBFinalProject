using Core.Utilities.Results;
using Dto.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFeeService
    {
        
        IDataResult<List<FeeDetailDto>> GetAllDetail();
        IDataResult<List<FeeDetailDto>> GetAllFeeDetailDtoByUserId(int userId);
        IDataResult<FeeDetailDto> GetFeeDetailDtoById(int id);

        IDataResult<List<FeeDetailDto>> GetAllUnPaidFeeDetail();

        IDataResult<UpdateFeeDto> IsUnPaidFee(int id);
        IDataResult<List<Fee>> GetAll();
        IDataResult<Fee> Get(int id);
        IResult Add(CreateFeeDto createFeeDto);
        IResult BulkdAdd(CreateFeeDto createFeeDto);
        IResult Update(UpdateFeeDto updateFeeDto);
        IResult Delete(int id);
    }
}
