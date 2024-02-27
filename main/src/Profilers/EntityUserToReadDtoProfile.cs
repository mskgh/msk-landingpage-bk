using AutoMapper;

namespace main.src.Profilers
{
    public class EntityUserToReadDtoProfile:Profile
    {
        public EntityUserToReadDtoProfile() 
        {
            CreateMap<Entities.User, Dtos.ReadUserDto>();
        }
    }
}
