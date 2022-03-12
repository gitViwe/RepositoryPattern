using DemoAPI.Configuration;
using DemoAPI.Shared;
using DemoMongoRepository;
using DemoMongoRepository.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DemoAPI.Controllers
{
    /// <summary>
    /// Demo API controller to illustrate repository pattern
    /// </summary>
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IMongoRepository<MongoHero> _repository;

        public HeroController(IMongoRepository<MongoHero> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Use this end point to query the Hero collection
        /// </summary>
        /// <response code="200">Returns a collection of MongoHero documents from the database</response>
        [HttpGet]
        [Route(nameof(Get))]
        [ProducesResponseType(typeof(Result<IEnumerable<MongoHero>>), 200)]
        public IActionResult Get()
        {
            var data = _repository.AsQueryable().ToList();

            return Ok(Result<IEnumerable<MongoHero>>.Success(data));
        }

        /// <summary>
        /// Use this end point to add to the Hero collection
        /// </summary>
        /// <response code="200">Returns a response message</response>
        [HttpPost]
        [Route(nameof(Add))]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<IActionResult> Add([FromBody] MongoHeroRequest request)
        {
            if (ModelState.IsValid)
            {
                var hero = new MongoHero()
                {
                    Alias = request.Alias,
                    Avatar = request.Avatar,
                    Elementals = request.Elementals,
                    Morality = request.Morality,
                };

                await _repository.InsertOneAsync(hero);

                return Ok(Result.Success("Hero created!"));
            }

            return Ok(Result.Fail(ModelState.GetModelErrors()));
        }
    }
}
