using AutoMapper;

namespace main.src.Profilers
{
    public class UpdateUserDtoToEntityUserWithoutPasswordProfile:Profile
    {
        public UpdateUserDtoToEntityUserWithoutPasswordProfile()
        {
            CreateMap<Dtos.UpdateUserDto, Entities.UserWithoutPassword>();
        }
    }
}
