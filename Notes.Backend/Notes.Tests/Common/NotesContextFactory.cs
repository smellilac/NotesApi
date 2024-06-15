using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Common;

public class NotesContextFactory
{
    public static Guid UserAId = Guid.NewGuid();
    public static Guid UserBId = Guid.NewGuid();

    public static Guid NoteIdForDelete = Guid.NewGuid();
    public static Guid NoteIdForUpdate = Guid.NewGuid();

    public static NotesDbContextPostgre Create()
    {
        var options = new DbContextOptionsBuilder<NotesDbContextPostgre>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new NotesDbContextPostgre(options);
        context.Database.EnsureCreated();

        context.Notes.AddRange(
            new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details1",
                EditDate = null,
                Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                Title = "Title1",
                UserId = UserAId
            },
            new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details2",
                EditDate = null,
                Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                Title = "Title2",
                UserId = UserBId
            },
            new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details3",
                EditDate = null,
                Id = NoteIdForDelete,
                Title = "Title3",
                UserId = UserAId
            },
            new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details4",
                EditDate = null,
                Id = NoteIdForUpdate,
                Title = "Title4",
                UserId = UserBId
            });
        context.SaveChanges();
        return context;
    }

    public static void Destroy(NotesDbContextPostgre context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
