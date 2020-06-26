using AutoMapper;
using MVC_Life_Cycle.Dtos;
using MVC_Life_Cycle.Models;

namespace MVC_Life_Cycle.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();
        }
    }
}