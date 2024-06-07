using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Commands.DeleteNote;

public class DeleteNoteCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}
