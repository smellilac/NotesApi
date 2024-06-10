using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Queries.GetNoteList;

public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
{
    public GetNoteListQueryValidator()
    {
        RuleFor(getNoteListQuery => getNoteListQuery.UserId)
            .NotEqual(Guid.Empty);
    }
}