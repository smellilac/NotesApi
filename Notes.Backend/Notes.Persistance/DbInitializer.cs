using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistance;

public class DbInitializer
{
    public static void Initialize(NotesDbContextPostgre context)
    {
        context.Database.EnsureCreated();
    }
}
