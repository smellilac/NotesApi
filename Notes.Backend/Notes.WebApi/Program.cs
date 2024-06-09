using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistance;
using System.Reflection;
using Notes.Application;
using Notes.WebApi.Middleware;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<NotesDbContextPostgre>();
        DbInitializer.Initialize(context);
    }
    catch (Exception _)
    {
        throw new Exception(""+ _);
    }
} 

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCastomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();
app.Run();
