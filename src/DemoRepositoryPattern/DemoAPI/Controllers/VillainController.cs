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
public class VillainController : ControllerBase
{
    private readonly IMongoRepository<MongoVillain> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor injection
    /// </summary>
    public VillainController(IMongoRepository<MongoVillain> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Use this end point to query the Villain collection
    /// </summary>
    /// <response code="200">Returns a collection of MongoVillain documents from the database</response>
    [HttpGet]
    [Route(nameof(Get))]
    [ProducesResponseType(typeof(Result<IEnumerable<MongoVillainRequest>>), 200)]
    public IActionResult Get()
    {
        var data = _repository.AsQueryable().ToList();

        var response = _mapper.Map<IEnumerable<MongoVillainRequest>>(data);

        return Ok(Result<IEnumerable<MongoVillainRequest>>.Success(response));
    }

    /// <summary>
    /// Use this end point to add to the Villain collection
    /// </summary>
    /// <response code="200">Returns a response message</response>
    [HttpPost]
    [Route(nameof(Add))]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> Add([FromBody] MongoVillainRequest request)
    {
        if (ModelState.IsValid)
        {
            var Villain = _mapper.Map<MongoVillain>(request);

            await _repository.InsertOneAsync(Villain);

            return Ok(Result.Success("Villain created!"));
        }

        return Ok(Result.Fail(ModelState.GetModelErrors()));
    }

    /// <summary>
    /// Use this end point to update a Villain
    /// </summary>
    /// <response code="200">Returns a response message</response>
    [HttpPut]
    [Route(nameof(Update))]
    [ProducesResponseType(typeof(Result), 200)]
    public async Task<IActionResult> Update([FromBody] MongoVillainRequest request)
    {
        if (ModelState.IsValid)
        {
            var document = await _repository.FindByIdAsync(request.Id);

            if (document is null)
            {
                return Ok(Result.Fail("Could not locate document"));
            }

            var newDocunment = _mapper.Map<MongoVillain>(request);

            await _repository.ReplaceOneAsync(newDocunment);

            return Ok(Result.Success("Villain Updated!"));
        }

        return Ok(Result.Fail(ModelState.GetModelErrors()));
    }

    /// <summary>
    /// Use this end point to delete a Villain
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

        return Ok(Result.Success("Villain Deleted!"));
    }
}
