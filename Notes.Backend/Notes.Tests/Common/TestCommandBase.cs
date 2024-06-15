using Notes.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Common;

public abstract class TestCommandBase : IDisposable
{
    protected readonly NotesDbContextPostgre Context;
    public TestCommandBase()
    {
        Context = NotesContextFactory.Create();
    }
    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }
}
