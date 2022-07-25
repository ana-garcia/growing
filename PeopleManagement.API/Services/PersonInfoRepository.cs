using Microsoft.EntityFrameworkCore;
using PeopleManagement.API.DBContext;
using PeopleManagement.API.Entities;

namespace PeopleManagement.API.Services
{
    public class PersonInfoRepository : IPersonInfoRepository
    {
        private readonly PersonInfoContext _context;

        public PersonInfoRepository(PersonInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddRegistrationForPersonAsync(int personId, Registration registration)
        {
            var person = await GetPersonAsync(personId, false);
            if(person != null)
            {
                person.Registrations.Add(registration);
            }
        }
        public async Task AddPersonForPersonAsync(Person person)
        {
            var personaux = await GetPersonAsync(person.Id, false);
            if (personaux != null)
            {
                _context.People.Add(person);
            }
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<IEnumerable<Person?>> GetPeopleInTheBuildingAsync()
        {
            return await _context.People
                    .Where(p => p.Registrations.Any(r => r.IsActive)).ToListAsync();
        }

        public async Task<Person?> GetPersonAsync(int personId, bool includRegistrations)
        {
            if (includRegistrations)
            {
                return await _context.People.Include(p => p.Registrations)
                    .Where(p => p.Id == personId).FirstOrDefaultAsync();
            }
            return await _context.People
                    .Where(p => p.Id == personId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Registration?>> GetRegistrationsForPersonAsync(int personId)
        {
            return await _context.Registrations
                    .Where(r => r.PersonId == personId)
                    .ToListAsync();
        }

        public async Task<Registration?> GetResistrationForPersonAsync(int personId, int registrationId)
        {
            return await _context.Registrations
                    .Where(r => r.PersonId == personId && r.Id == registrationId)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> PersonExistsAsync(int personId)
        {
            return await _context.People.AnyAsync(p => p.Id == personId);
        }

        public async Task<bool> PersonHasActiveRegistretionAsync(int personId)
        {
            return await _context.People.AnyAsync(p => p.Id == personId && p.Registrations.Any(r => r.IsActive));
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
