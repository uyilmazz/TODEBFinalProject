using Core.DataAccess;
using Dto.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IApartmentDal : IEntityRepository<Apartment>
    {
        ApartmentDetailDto GetApartmentDetail(int id);
        List<ApartmentDetailDto> GetAllApartmentDetail();
    }
}
