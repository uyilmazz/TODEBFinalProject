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
    public interface IPersonTypeService
    {
        IDataResult<List<PersonType>> GetAll();
        IDataResult<PersonType> Get(int id);

        IResult Add(CreatePersonTypeDto createPersonTypeDto);
        IResult Update(UpdatePersonTypeDto updatePersonTypeDto);
        IResult Delete(int id);
    }
}
