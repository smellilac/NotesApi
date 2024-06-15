using AutoMapper;
using Notes.Application.NotesCQRS.Queries.GetNoteList;
using Notes.Persistance;
using Notes.Tests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Tests.Queries;

[Collection("QueryCollection")]
public class GetNoteListQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly NotesDbContextPostgre _context;

    public GetNoteListQueryHandlerTests(QueryTestFixture fixture) =>
        (_mapper, _context) = (fixture.Mapper, fixture.Context);

    [Fact]
    public async Task GetNoteListQueryHandlerTests_Success()
    {
        // Arrange
        var handler = new GetNoteListQueryHandler(_context, _mapper);

        // Act
        var result = await handler.Handle(
           new GetNoteListQuery
           {
               UserId = NotesContextFactory.UserBId
           },
           CancellationToken.None);

        // Assert
        result.ShouldBeOfType<NoteListVm>();
        result.Notes.Count.ShouldBe(2);
    }
}
