using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class TODEBFinalProjectContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TodebFinalProject;Trusted_Connection=true", options => options.EnableRetryOnFailure());
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<PersonPassword> PersonPasswords { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<ApartmentType> ApartmentTypes { get; set; }
        public DbSet<ApartmentBloc> ApartmentBlocs { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Fee> Fees { get; set; }
    }
}
