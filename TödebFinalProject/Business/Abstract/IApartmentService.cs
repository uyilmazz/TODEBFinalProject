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
    public interface IApartmentService
    {
        IDataResult<List<Apartment>> GetAll();
        IDataResult<List<Apartment>> GetAllRented();

        IDataResult<List<ApartmentDetailDto>> GetAllDetail();
        IDataResult<ApartmentDetailDto> GetDetail(int id);
        IDataResult<Apartment> Get(int id);

        IResult Add(CreateApartmentDto createApartmentDto);
        IResult Update(UpdateApartmentDto updateApartmentDto);
        IResult Delete(int id);
    }
}
