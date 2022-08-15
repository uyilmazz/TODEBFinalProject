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
    public class EfBillDal : EfEntityRepository<Bill, TODEBFinalProjectContext>, IBillDal
    {
        public List<BillDetailDto> GetAllBillDetailDto(Expression<Func<Bill, bool>> filter = null)
        {
            using (var context = new TODEBFinalProjectContext())
            {
                return (filter != null ? context.Bills.Where(filter) : context.Bills).Include(f => f.Apartment).ThenInclude(a => a.Person).Select(bill => new BillDetailDto()
                {
                    Id = bill.Id,
                    Amount = bill.Amount,
                    CreatedDate = bill.CreateDate,
                    IsPaid = bill.IsPaid,
                    ApartmentId = bill.ApartmentId,
                    PersonName = (bill.Apartment.Person.FirstName != null && bill.Apartment.Person.LastName != null) ?
                    bill.Apartment.Person.FirstName + " " + bill.Apartment.Person.LastName : ""
                }).ToList();
            }
        }

        public BillDetailDto GetBillDetailDto(Expression<Func<Bill, bool>> filter)
        {
            using (var context = new TODEBFinalProjectContext())
            {
                return context.Bills.Where(filter).Include(f => f.Apartment).ThenInclude(a => a.Person).Select(bill => new BillDetailDto()
                {
                    Id = bill.Id,
                    Amount = bill.Amount,
                    CreatedDate = bill.CreateDate,
                    IsPaid = bill.IsPaid,
                    ApartmentId = bill.ApartmentId,
                    PersonName = (bill.Apartment.Person.FirstName != null && bill.Apartment.Person.LastName != null) ?
                    bill.Apartment.Person.FirstName + " " + bill.Apartment.Person.LastName : ""
                }).FirstOrDefault();
            }
        }
    }
}
