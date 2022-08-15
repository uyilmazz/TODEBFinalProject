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
    public interface IApartmentBlocService
    {
        IDataResult<List<ApartmentBloc>> GetAll();
        IDataResult<ApartmentBloc> Get(int id);

        IResult Add(CreateApartmentBlocDto createApartmentBlocDto);
        IResult Update(ApartmentBloc updateApartmentBloc);
        IResult Delete(int id);
    }
}
