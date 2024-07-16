using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.NotesCQRS.Commands.CreateNote;
using Notes.Application.NotesCQRS.Commands.DeleteNote;
using Notes.Application.NotesCQRS.Commands.UpdateNote;
using Notes.Application.NotesCQRS.Queries.GetNoteDetails;
using Notes.Application.NotesCQRS.Queries.GetNoteList;
using Notes.WebApi.Models;
using Serilog;

namespace Notes.WebApi.Controllers;


[ApiVersion("1.0")]
[ApiVersion("2.0")]
//[ApiVersionNeutral]
[Produces("application/json")]
[Route("api/[controller]")]
[Authorize]
public class NoteController : BaseController
{
    private readonly IMapper _mapper;
    public NoteController(IMapper mapper) => _mapper = mapper;

    /// <summary>
    /// Gets the list of the notes
    /// </summary>
    /// <remarks>Sample request: Get /note</remarks>
    /// <returns>Returns NoteListVm</returns>
    /// <response code = "200">Success</response>
    /// <response code = "401">If the user is unauthorized</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        var query = new GetNoteListQuery
        {
            UserId = UserId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Gets the note by id
    /// </summary>
    /// <remarks>
    /// Sapmle request: 
    /// Get /note/db92dc35-728e-4e2f-910e-777cfdd86ecb
    /// </remarks>
    /// <param name="id">Note ID</param>
    /// <returns>Returns NoteDetailsVm</returns>
    /// <response code = "200">Success</response>
    /// <response code = "401">If the user is unauthorized</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
    {
        var query = new GetNoteDetailsQuery
        {
            Id = id,
            UserId = UserId
        };
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Creates the note
    /// </summary>
    /// <remarks>
    /// Sample request: 
    /// Post note/
    /// {
    ///     title = "new title",
    ///     details = "note`s details"
    /// }
    /// </remarks>
    /// <param name="createNoteDto">Note model - CreateNoteDto</param>
    /// <returns>Note`s ID</returns>
    /// <response code = "201">Success creating</response>
    /// <response code = "401">If the user is unauthorized</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        Log.Information("Received CreateNoteDto: {@CreateNoteDto}", createNoteDto);
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = UserId;
        var vm = await Mediator.Send(command);
        return Ok(vm);
    }

    /// <summary>
    /// Updates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// Put note/
    /// {
    ///     title = "title after updating"
    ///     id = db92dc35-728e-4e2f-910e-777cfdd86ecb
    /// }
    /// </remarks>
    /// <param name="UpdateNoteDto">UpdateNoteDto model</param>
    /// <returns>NoContent</returns>
    /// <response code = "204">Success updating</response>
    /// <response code = "401">If the user is unauthorized</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Update([FromBody] UpdateNoteDto dto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(dto);
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Delete the note by id 
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /note/db92dc35-728e-4e2f-910e-777cfdd86ecb
    /// </remarks>
    /// <param name="id">Note`s ID</param>
    /// <returns>NoContent</returns>
    /// <response code = "204">Success deleting</response>
    /// <response code = "401">If the user is unauthorized</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteNoteCommand 
        { 
            Id = id,
            UserId = UserId
        };
        await Mediator.Send(command);
        return NoContent();
    }
}
