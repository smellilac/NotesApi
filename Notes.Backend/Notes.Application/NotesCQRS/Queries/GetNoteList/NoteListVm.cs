using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesCQRS.Queries.GetNoteList;

public class NoteListVm
{
    public IList<NoteLookUpDto> Notes { get; set; }
}
