using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistance;
using System.Reflection;
using Notes.Application;
using Notes.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = 
        JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:44325/";
        options.Audience = "NotesWebAPI";
        options.RequireHttpsMetadata = false;
    });
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
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
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
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "Notes API");
});
app.UseCastomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
