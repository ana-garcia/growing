namespace PeopleManagement.API.Model
{
    public class RegistrationModel
    {
        public int Id { get; set; }
        public DateTime Entry { get; set; }
        public string Motive { get; set; } = String.Empty;
        public int ExpectedDuration { get; set; }
        public DateTime? Exit { get; set; }
        public Boolean IsActive { get; set; } = true;


    }
}
