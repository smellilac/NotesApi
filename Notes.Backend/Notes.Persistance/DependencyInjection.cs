using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Notes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Notes.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<NotesDbContextPostgre>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<INotesDbContext>(provider =>
            provider.GetService<NotesDbContextPostgre>());

        services.AddScoped<INotesDbContext>(provider =>
        {
            var dbContext = provider.GetService<NotesDbContextPostgre>();
            if (dbContext == null)
            {
                throw new Exception("NotesDbContextPostgre service not found.");
            }
            return dbContext;
        });
        return services;
    }
}