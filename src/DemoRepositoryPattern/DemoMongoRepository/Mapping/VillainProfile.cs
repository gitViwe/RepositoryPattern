using AutoMapper;
using DemoMongoRepository.Model;

namespace DemoMongoRepository.Mapping;

public class VillainProfile : Profile
{
    public VillainProfile()
    {
        CreateMap<MongoVillain, MongoVillainRequest>().ReverseMap();
    }
}
