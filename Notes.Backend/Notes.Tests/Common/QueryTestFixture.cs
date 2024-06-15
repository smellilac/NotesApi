using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Common;

public class QueryTestFixture
{
    public NotesDbContextPostgre Context { get; set; }
    public IMapper Mapper { get; set; }
    public QueryTestFixture()
    {
        Context = NotesContextFactory.Create();

        var configurationBuilder = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile
                (typeof(INotesDbContext).Assembly));
        });
        Mapper = configurationBuilder.CreateMapper();
    }

    public void Dispose(NotesDbContextPostgre context)
    {
        NotesContextFactory.Destroy(context);
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
