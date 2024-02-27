using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using main.src.Services.User;
using main.src.Dtos;


namespace main.Test.UnitTest.Services.Users
{
    public class UserServicesTest
    {
        IMapper mapper;
        IUserServices userServices;
        public UserServicesTest()
        {
            mapper = A.Fake<IMapper>();
            userServices = A.Fake<IUserServices>();
        }

        [Fact]
        public void UserServices_AssignIds_ReturnAUserWithIdandTernantId()
        {
            //Arrange
            var userWriteDtoWithoutIds = A.Fake<WriteUserDto>();
            var Id = Guid.NewGuid();
            var TernantId = Guid.NewGuid();
            
            var user = mapper.Map<src.Entities.User>(userWriteDtoWithoutIds);
            user.Id = Id;
            user.TernantId = TernantId;

            A.CallTo(() => userServices.AssignIds(userWriteDtoWithoutIds)).Returns(user);


            //Assert
            Id.Should().NotBeEmpty();
            TernantId.Should().NotBeEmpty();
            user.Id.Should().NotBeEmpty();
            user.TernantId.Should().NotBeEmpty();
            user.Id.Should().Be(Id);
            user.TernantId.Should().Be(TernantId);
        }
    }
}
