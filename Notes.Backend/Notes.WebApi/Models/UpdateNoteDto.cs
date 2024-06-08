using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.NotesCQRS.Commands.UpdateNote;

namespace Notes.WebApi.Models;

public class UpdateNoteDto : IMapWith<UpdateNoteCommand>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
            .ForMember(command => command.Id, opt =>
            opt.MapFrom(dto => dto.Id))
            .ForMember(command => command.Title, opt =>
            opt.MapFrom(dto => dto.Title))
            .ForMember(command => command.Details, opt =>
            opt.MapFrom(dto => dto.Details));
    }
}
