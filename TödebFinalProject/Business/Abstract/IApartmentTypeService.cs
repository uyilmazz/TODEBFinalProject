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
    public interface IApartmentTypeService
    {
        IDataResult<List<ApartmentType>> GetAll();
        IDataResult<ApartmentType> Get(int id);

        IResult Add(CreateApartmentTypeDto createApartmentTypeDto);
        IResult Update(ApartmentType updateApartmentType);
        IResult Delete(int id);
    }
}
