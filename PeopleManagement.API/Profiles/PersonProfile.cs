using AutoMapper;

namespace PeopleManagement.API.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Entities.Person, Model.PersonWithoutRegistrationsModel>();
            CreateMap<Entities.Person, Model.PersonModel>();
            CreateMap<Model.PersonModel, Entities.Person>();
        }
    }
}
