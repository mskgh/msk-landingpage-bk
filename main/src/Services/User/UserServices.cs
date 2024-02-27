
using AutoMapper;
using main.src.Dtos;

namespace main.src.Services.User
{
    public class UserServices : IUserServices
    {
        IMapper mapper;
    
        public UserServices( IMapper mapper) 
        {
            this.mapper = mapper;
           
        }

        public Entities.User AssignIds(WriteUserDto userDto)
        {
            var Id = Guid.NewGuid();
            var TernantId = Guid.NewGuid();

            var user = mapper.Map<Entities.User>(userDto);
            user.Id = Id;
            user.TernantId = TernantId;
            return user;
        }
    }
}
