using AutoMapper;

namespace main.src.Profilers
{
    public class ModelToReadDtoProfile:Profile
    {
        public ModelToReadDtoProfile()
        {
            CreateMap<Models.User,Dtos.ReadUserDto>();
        }
    }
}
