using System.ComponentModel.DataAnnotations;

namespace PeopleManagement.API.Model
{
    public class RegistrationForCreationModel
    {
        public string Motive { get; set; } = String.Empty;

        public int ExpectedDuration { get; set; }


    }
}
