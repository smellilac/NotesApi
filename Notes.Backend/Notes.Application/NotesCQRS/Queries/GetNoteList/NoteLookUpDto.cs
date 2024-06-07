using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Queries.GetNoteList;

public class NoteLookUpDto : IMapWith<Note>
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Note, NoteLookUpDto>()
            .ForMember(noteDto => noteDto.Id, 
            opt => opt.MapFrom(note => note.Id))
            .ForMember(noteDto => noteDto.Title,
            opt => opt.MapFrom(note => note.Title));
    }
}
