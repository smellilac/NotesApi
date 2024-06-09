using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Queries.GetNoteDetails;

public class GetNoteDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
    public GetNoteDetailsQueryValidator()
    {
        RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(getNoteDetailsQuery => getNoteDetailsQuery.Id)
            .NotEqual(Guid.Empty);
    }
}
