﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Queries.GetNoteDetails;

public class GetNoteDetailsQuery : IRequest<NoteDetailsVm>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
