using Core.DataAccess;
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
    public class EfPersonDal : EfEntityRepository<Person, TODEBFinalProjectContext>, IPersonDal
    {
        public List<PersonDetailDto> GetAllPersonDetail()
        {
            using(var context = new TODEBFinalProjectContext())
            {
                var persons = from p in context.Persons
                              join t in context.PersonTypes on
                              p.TypeId equals t.Id
                              select new PersonDetailDto { Id = p.Id,FirstName = p.FirstName,LastName = p.LastName,Email = p.Email, PhoneNumber = p.PhoneNumber,PlakaNo = p.PlakaNo ,TCNo = p.TCNo,TypeName = t.Type};

                return persons.ToList();

            }
        }

        public PersonDetailDto GetPersonDetail(int id)
        {
            using (var context = new TODEBFinalProjectContext())
            {
                var person = from p in context.Persons
                              join t in context.PersonTypes on
                              p.TypeId equals t.Id
                             where p.Id == id
                              select new PersonDetailDto { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Email = p.Email, PhoneNumber = p.PhoneNumber, PlakaNo = p.PlakaNo, TCNo = p.TCNo, TypeName = t.Type };

                return person.FirstOrDefault();

            }
        }

        public Person GetPersonWithPassword(Expression<Func<Person, bool>> filter)
        {
            using(var context = new TODEBFinalProjectContext())
            {
                return context.Persons.Include(p => p.Password).Where(filter).FirstOrDefault();
               
            }
        }
    }
}
