using AutoMapper;

namespace main.src.Profilers
{
    public class EntityToModelProfile:Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<Entities.User, Models.User>();
        }
    }
}
