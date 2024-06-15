using Notes.Application.Common.CustomExceptions;
using Notes.Application.NotesCQRS.Commands.CreateNote;
using Notes.Application.NotesCQRS.Commands.DeleteNote;
using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Tests.Commands;

public class DeleteNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteNoteCommandHandler_Success()
    {
        // Arrange
        var handler = new DeleteNoteCommandHandler(Context);

        // Act

        await handler.Handle(new DeleteNoteCommand
        {
            UserId = NotesContextFactory.UserAId,
            Id = NotesContextFactory.NoteIdForDelete
        },
        CancellationToken.None);

        // Assert

        Assert.Null(Context.Notes.SingleOrDefault(note => 
        note.Id == NotesContextFactory.NoteIdForDelete));
    }

    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new DeleteNoteCommandHandler(Context);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => 
        await handler.Handle(
            new DeleteNoteCommand
            {
                Id = Guid.NewGuid(),
                UserId = NotesContextFactory.UserAId
            },
            CancellationToken.None));
    }

    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
    {
        // Arrange

        var handlerForDeleting = new DeleteNoteCommandHandler(Context);
        var handlerForCreating = new CreateNoteCommandHandler(Context);

        // Act

        var noteId = await handlerForCreating.Handle(
            new CreateNoteCommand
            {
                UserId = NotesContextFactory.UserAId,
                Title = "Testing Delete_WrongUserId",
                Details = "f"
            },
            CancellationToken.None);

        // Assert

        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handlerForDeleting.Handle(
                new DeleteNoteCommand
                {
                    Id = noteId,
                    UserId = NotesContextFactory.UserBId
                },
                CancellationToken.None));
    }
}
