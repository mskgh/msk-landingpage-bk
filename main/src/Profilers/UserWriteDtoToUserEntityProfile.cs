using AutoMapper;

namespace main.src.Profilers
{
    public class UserWriteDtoToUserEntityProfile:Profile
    {
        public UserWriteDtoToUserEntityProfile() 
        {
            CreateMap<Dtos.WriteUserDto, Entities.User>();
        }

    }
}
