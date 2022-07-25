using AutoMapper;

namespace PeopleManagement.API.Profiles
{
    public class RegistrationProfile: Profile
    {
        public RegistrationProfile()
        {
            CreateMap<Entities.Registration, Model.RegistrationModel>();
            CreateMap<Model.RegistrationForCreationModel, Entities.Registration>();
        }
    }
}
