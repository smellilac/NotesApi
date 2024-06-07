using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.CustomExceptions;
using Notes.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INotesDbContext _context;
    public DeleteNoteCommandHandler(INotesDbContext _context) => this._context = _context;

    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _context.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(entity), request.Id);
        }

        _context.Notes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
