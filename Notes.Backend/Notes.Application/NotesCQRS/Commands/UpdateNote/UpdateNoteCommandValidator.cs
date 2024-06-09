using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(updateNoteCommand => updateNoteCommand.Title)
            .NotEmpty().MaximumLength(250);
        RuleFor(updateNoteCommand => updateNoteCommand.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(updateNoteCommand => updateNoteCommand.Id)
            .NotEqual(Guid.Empty);
    }
}
