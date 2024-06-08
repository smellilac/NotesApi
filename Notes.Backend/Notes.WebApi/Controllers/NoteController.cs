using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.NotesCQRS.Commands.CreateNote;
using Notes.Application.NotesCQRS.Commands.DeleteNote;
using Notes.Application.NotesCQRS.Commands.UpdateNote;
using Notes.Application.NotesCQRS.Queries.GetNoteDetails;
using Notes.Application.NotesCQRS.Queries.GetNoteList;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers;

[Route("api/[controller]")]
public class NoteController : BaseController
{
    private readonly IMapper _mapper;
    public NoteController(IMapper mapper) => _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        var query = new GetNoteListQuery
        {
            UserId = UserId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpGet("{id}")]
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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = UserId;
        var vm = await Mediator.Send(command);
        return Ok(vm);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateNoteDto dto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(dto);
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
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
