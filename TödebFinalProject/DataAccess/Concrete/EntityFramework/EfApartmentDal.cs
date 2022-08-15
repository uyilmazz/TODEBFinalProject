using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Dto.Concrete;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfApartmentDal : EfEntityRepository<Apartment, TODEBFinalProjectContext>, IApartmentDal
    {
        public List<ApartmentDetailDto> GetAllApartmentDetail()
        {
            using (var context = new TODEBFinalProjectContext())
            {

                return context.Apartments.Include(a => a.ApartmentBloc).Include(a => a.ApartmentType).Include(a => a.Person).Select(apartments => new ApartmentDetailDto
                {
                    Id = apartments.Id,
                    ApartmentNumber = apartments.ApartmentNumber,
                    BlocName = apartments.ApartmentBloc.Name,
                    TypeName = apartments.ApartmentType.Name,
                    Floor = apartments.Floor,
                    IsEmpty = apartments.IsEmpty,
                    UserName = apartments.Person.FirstName != null ? apartments.Person.FirstName + " " + apartments.Person.LastName : null
                }).ToList();

            }
        }

        public ApartmentDetailDto GetApartmentDetail(int id)
        {
            using (var context = new TODEBFinalProjectContext())
            {
                return context.Apartments.Where(a => a.Id == id).Include(a => a.ApartmentBloc).Include(a => a.ApartmentType).Include(a => a.Person).Select(apartments => new ApartmentDetailDto
                {
                    Id = apartments.Id,
                    ApartmentNumber = apartments.ApartmentNumber,
                    BlocName = apartments.ApartmentBloc.Name,
                    TypeName = apartments.ApartmentType.Name,
                    Floor = apartments.Floor,
                    IsEmpty = apartments.IsEmpty,
                    UserName = apartments.Person.FirstName != null ? apartments.Person.FirstName + " " + apartments.Person.LastName : null
                }).FirstOrDefault();

            }
        }
    }
}
