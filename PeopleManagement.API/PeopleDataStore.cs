using PeopleManagement.API.Model;

namespace PeopleManagement.API
{
    public class PeopleDataStore
    {
        public List<PersonModel> People { get; set; }

        // public static PeopleDataStore Current { get; } = new PeopleDataStore();

        public PeopleDataStore()
        {
            People = new List<PersonModel>()
            {
                new PersonModel()
                {
                    Id = 14003368,
                    Name = "Ana",
                    Surname = "Garcia",
                    Registrations = new List<RegistrationModel>()
                    {
                        new RegistrationModel()
                        {
                            Id = 1,
                            Motive = "teste1",
                            Entry = DateTime.Now,
                            Exit = DateTime.Now,
                            ExpectedDuration = 1,
                            IsActive = false,
                        },
                        new RegistrationModel()
                        {
                            Id = 2,
                            Motive = "teste2",
                            Entry = DateTime.Now,
                            ExpectedDuration = 2,
                            IsActive = true
                        }
                    }
                },
                new PersonModel()
                {
                    Id = 12345678,
                    Name = "Name",
                    Surname = "Surname",
                    Registrations = new List<RegistrationModel>()
                    {
                        new RegistrationModel()
                            {
                                Id = 3,
                                Motive = "teste3",
                                Entry = DateTime.Now,
                                Exit = DateTime.Now,
                                ExpectedDuration = 1,
                                IsActive = false
                            }
                    }
                }
            };
        }
    }
}
