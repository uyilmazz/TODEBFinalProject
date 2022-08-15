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
    public interface IBillService
    {
        IDataResult<List<BillDetailDto>> GetAllDetail();
        IDataResult<List<BillDetailDto>> GetAllBillDetailDtoByUserId(int userId);
        IDataResult<BillDetailDto> GetBillDetailDtoById(int id);
        IDataResult<List<BillDetailDto>> GetAllUnPaidBillDetailDto();

        IDataResult<List<Bill>> GetAll();
        IDataResult<Bill> Get(int id);
        IDataResult<UpdateBillDto> IsUnPaidBill(int id);
        IResult Add(CreateBillDto createBillDto);
        IResult BulkdAdd(CreateBillDto createBillDto);
        IResult Update(UpdateBillDto updatebillDto);
        IResult Delete(int id);
    }
}
