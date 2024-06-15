using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.CustomExceptions;
using Notes.Application.NotesCQRS.Commands.CreateNote;
using Notes.Application.NotesCQRS.Commands.UpdateNote;
using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Tests.Commands;

public class UpdateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateNoteCommandHandlerTests_Seccess()
    {
        // Arrange
        var handler = new UpdateNoteCommandHandler(Context);
        var newNoteTitle = "NEW title after success update";

        // Act
        await handler.Handle(
            new UpdateNoteCommand
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                UserId = NotesContextFactory.UserBId,
                Title = newNoteTitle
            },
            CancellationToken.None);
        // Assert
        Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(
            note => note.Id == NotesContextFactory.NoteIdForUpdate &&
            note.UserId == NotesContextFactory.UserBId &&
            note.Title == newNoteTitle));
    }

    [Fact]
    public async Task UpdateNoteCommandHandlerTests_FailOnWrongId()
    {
        // Arrange
        var handler = new UpdateNoteCommandHandler(Context);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpdateNoteCommand
            {
                Id = Guid.NewGuid(),
                UserId = NotesContextFactory.UserAId,
                Title = "FailIdUpdate"
            },
            CancellationToken.None));
    }

    [Fact]
    public async Task UpdateNoteCommandHandlerTests_FailOnWrongUserId()
    {
        // Arrange 
        var handlerForUpdate = new UpdateNoteCommandHandler(Context);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => 
            await handlerForUpdate.Handle(
                new UpdateNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = Guid.NewGuid(),
                }, 
                CancellationToken.None));
    }
}
