using PeopleManagement.API.Entities;

namespace PeopleManagement.API.Services
{
    public interface IPersonInfoRepository
    {
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task<IEnumerable<Person?>> GetPeopleInTheBuildingAsync();
        Task<Person?> GetPersonAsync(int personId, bool includRegistrations);
        Task<bool> PersonExistsAsync(int personId);
        Task<IEnumerable<Registration?>> GetRegistrationsForPersonAsync(int personId);
        Task<Registration?> GetResistrationForPersonAsync(int personId,
            int registrationId);
        Task<bool>  PersonHasActiveRegistretionAsync(int personId);
        
        Task AddRegistrationForPersonAsync(int personId, Registration registration);
        Task AddPersonForPersonAsync(Person person);
        Task<bool> SaveChangesAsync();

    }
}
