using AutoMapper;

namespace main.src.Models
{
    public class TestProfile:Profile
    {
        public TestProfile()
        {
            CreateMap<TestModel,ATestModel>();
            CreateMap<ATestModel,TestModel>();
        }
    }
}
