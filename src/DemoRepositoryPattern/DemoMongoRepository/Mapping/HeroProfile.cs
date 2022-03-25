using AutoMapper;
using DemoMongoRepository.Model;

namespace DemoMongoRepository.Mapping;

internal class HeroProfile : Profile
{
    public HeroProfile()
    {
        // create a two way mapping
        CreateMap<MongoHero, MongoHeroRequest>().ReverseMap();
    }
}
