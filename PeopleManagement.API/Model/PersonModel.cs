namespace PeopleManagement.API.Model
{
    public class PersonModel
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public int NumberOfRegistrations
        { 
            get
            {
                return Registrations.Count;
            } 
        }
        public ICollection<RegistrationModel> Registrations { get; set; }
            = new List<RegistrationModel>();

    }
}
