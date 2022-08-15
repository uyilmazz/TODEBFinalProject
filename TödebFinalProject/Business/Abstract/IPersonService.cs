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
    public  interface IPersonService
    {
        // Person
        IDataResult<List<Person>> GetAll();
        IDataResult<Person> Get(int id);
        IDataResult<Person> GetByEmail(string email);

        // PersonDetailDto
        IDataResult<List<PersonDetailDto>> GetAllDetail();
        IDataResult<PersonDetailDto> GetDetail(int id);
       

        IDataResult<String> Add(CreatePersonDto createPersonDto);
        IResult Update(UpdatePersonDto updatePersonDto);
        IResult Delete(int id);
    }
}
