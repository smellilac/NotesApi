using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Queries.GetNoteList;

public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
{
    private readonly INotesDbContext _context;
    private readonly IMapper _mapper;

    public GetNoteListQueryHandler(INotesDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);
    public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
    {
        var notesQuery = await _context.Notes
            .Where(note => note.UserId == request.UserId)
            .ProjectTo<NoteLookUpDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new NoteListVm { Notes = notesQuery };
    }
}
