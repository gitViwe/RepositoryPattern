using AutoMapper;
using DemoAPI.Configuration;
using DemoAPI.Shared;
using DemoMongoRepository;
using DemoMongoRepository.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DemoAPI.Controllers;

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
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor injection
    /// </summary>
    public HeroController(IMongoRepository<MongoHero> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Use this end point to query the Hero collection
    /// </summary>
    /// <response code="200">Returns a collection of MongoHero documents from the database</response>
    [HttpGet]
    [Route(nameof(Get))]
    [ProducesResponseType(typeof(Result<IEnumerable<MongoHeroRequest>>), 200)]
    public IActionResult Get()
    {
        var data = _repository.AsQueryable().ToList();

        var response = _mapper.Map<IEnumerable<MongoHeroRequest>>(data);

        return Ok(Result<IEnumerable<MongoHeroRequest>>.Success(response));
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
            var hero = _mapper.Map<MongoHero>(request);

            await _repository.InsertOneAsync(hero);

            return Ok(Result.Success("Hero created!"));
        }

        return Ok(Result.Fail(ModelState.GetModelErrors()));
    }

    /// <summary>
    /// Use this end point to update a Hero
    /// </summary>
    /// <response code="200">Returns a response message</response>
    [HttpPut]
    [Route(nameof(Update))]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> Update([FromBody] MongoHeroRequest request)
    {
        if (ModelState.IsValid)
        {
            var document = await _repository.FindByIdAsync(request.Id);

            if (document is null)
            {
                return Ok(Result.Fail("Could not locate document"));
            }

            var newDocunment = _mapper.Map<MongoHero>(request);

            await _repository.ReplaceOneAsync(newDocunment);

            return Ok(Result.Success("Hero Updated!"));
        }

        return Ok(Result.Fail(ModelState.GetModelErrors()));
    }

    /// <summary>
    /// Use this end point to delete a Hero
    /// </summary>
    /// <response code="200">Returns a response message</response>
    [HttpDelete]
    [Route(nameof(Delete))]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Ok(Result.Fail("No ID provided."));
        }

        var document = await _repository.FindByIdAsync(id);

        if (document is null)
        {
            return Ok(Result.Fail("Could not locate document"));
        }

        await _repository.DeleteByIdAsync(id);

        return Ok(Result.Success("Hero Deleted!"));
    }
}
