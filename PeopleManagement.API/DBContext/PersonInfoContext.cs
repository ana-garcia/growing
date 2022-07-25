using Microsoft.EntityFrameworkCore;
using PeopleManagement.API.Entities;

namespace PeopleManagement.API.DBContext
{
    public class PersonInfoContext: DbContext
    {
        public DbSet<Person> People { get; set; } = null!;
        public DbSet<Registration> Registrations { get; set; } = null!;

        public PersonInfoContext(DbContextOptions<PersonInfoContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuiler)
        {
            modelBuiler.Entity<Person>()
                .HasData(
                new Person(12345678, "Ana", "Garcia")
                );
            modelBuiler.Entity<Registration>()
                .HasData(
                new Registration(DateTime.Now, "teste de entrada", 2)
                {
                    Id = 1,
                    PersonId = 12345678,
                },
                new Registration(DateTime.Now, "teste de entrada 2", 1)
                {
                    Id = 2,
                    PersonId = 12345678,
                    Exit = DateTime.Now,
                    IsActive = false,
                }
                );
            base.OnModelCreating(modelBuiler);
        }
    }
}
