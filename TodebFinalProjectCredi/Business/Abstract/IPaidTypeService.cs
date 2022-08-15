using Core.Utilities.Results;
using DTO.Concrete;
using Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPaidTypeService
    {
        IResult Add(CreatePaidTypeDto createPaidTypeDto);
        IResult Update(PaidType model);
        IResult Delete(ObjectId id);
        IDataResult<PaidType> Get(ObjectId id);
        IDataResult<PaidType> GetByName(string name);
        IDataResult<List<PaidType>> GetAll();
    }
}
