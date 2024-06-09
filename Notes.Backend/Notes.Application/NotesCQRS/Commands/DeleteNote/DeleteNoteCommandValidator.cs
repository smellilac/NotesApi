using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Commands.DeleteNote;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(deleteNoteCommand => deleteNoteCommand.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(deleteNoteCommand => deleteNoteCommand.Id)
            .NotEqual(Guid.Empty);
    }
}
