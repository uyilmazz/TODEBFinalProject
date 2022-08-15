using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Dto.Concrete;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFeeDal : EfEntityRepository<Fee, TODEBFinalProjectContext>, IFeeDal
    {
        public List<FeeDetailDto> GetAllFeeDetailDto(Expression<Func<Fee, bool>> filter = null)
        {
            using(var context = new TODEBFinalProjectContext())
            {
                return (filter != null ? context.Fees.Where(filter) : context.Fees).Include(f => f.Apartment).ThenInclude(a => a.Person).Select(fee => new FeeDetailDto()
                {
                    Id = fee.Id,
                    Amount = fee.Amount,
                    CreatedDate = fee.CreatedDate,
                    IsPaid = fee.IsPaid,
                    ApartmentId = fee.ApartmentId,
                    PersonName = (fee.Apartment.Person.FirstName != null && fee.Apartment.Person.LastName != null) ? 
                    fee.Apartment.Person.FirstName  + " " + fee.Apartment.Person.LastName : ""
                }).ToList();
            }
        }

        public FeeDetailDto GetFeeDetailDto(Expression<Func<Fee, bool>> filter)
        {
            using(var context = new TODEBFinalProjectContext())
            {
                return  context.Fees.Where(filter).Include(f => f.Apartment).ThenInclude(a => a.Person).Select(fee => new FeeDetailDto()
                {
                    Id = fee.Id,
                    Amount = fee.Amount,
                    CreatedDate = fee.CreatedDate,
                    IsPaid = fee.IsPaid,
                    ApartmentId = fee.ApartmentId,
                    PersonName = (fee.Apartment.Person.FirstName != null && fee.Apartment.Person.LastName != null) ?
                    fee.Apartment.Person.FirstName + " " + fee.Apartment.Person.LastName : ""
                }).FirstOrDefault();
            }
        }
    }
}
