using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes.Application.NotesCQRS.Queries.GetNoteDetails;
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
public class GetNoteDetailsQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly NotesDbContextPostgre _context;

    public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture) =>
        (_mapper, _context) = (fixture.Mapper, fixture.Context);

    [Fact]
    public async Task GetNoteDetailsQueryHandlerTests_Success()
    {
        // Arrange
        var handler = new NoteDetailsQueryHandler(_context, _mapper);
        // Act
        var result = await handler.Handle(
                new GetNoteDetailsQuery
                {
                    UserId = NotesContextFactory.UserBId,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084")
                },
                CancellationToken.None);

        // Assert
        result.ShouldBeOfType<NoteDetailsVm>();
        result.Title.ShouldBe("Title2");
        result.CreationDate.ShouldBe(DateTime.Today);
    }
}
