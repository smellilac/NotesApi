using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Notes.WebApi.SwaggerConfiguration;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) 
        => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var descriptions in _provider.ApiVersionDescriptions)
        {
            var apiVersion = descriptions.ApiVersion.ToString();
            options.SwaggerDoc(descriptions.GroupName,
                new OpenApiInfo
                {
                    Version = apiVersion,
                    Title = $"Notes API {apiVersion}",
                    Contact = new OpenApiContact
                    {
                        Name = "Smellilac project",
                        Url = new Uri("https://t.me/buravgenius"),
                        Email = "dimatega@gmail.com"
                    }
                });
            options.AddSecurityDefinition($"Auth token {apiVersion}",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header, // token will be in header of request
                    Type = SecuritySchemeType.Http, // http scheme
                    BearerFormat = "Jwt",
                    Scheme = "bearer",
                    Name = "Authorization", // name for header which will be used for sending token 
                    Description = "Authorization token"
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = $"AuthToken {apiVersion}"
                            }
                        },
                        new string[] { }
                    }
                });
            // CustomOperationIds - settinigs of generating identifiers  
            // for operations for different endpoints
            options.CustomOperationIds(apiDescription =>
                    apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) 
                        ? methodInfo.Name
                        : null);
            // TryGetMethodInfo - gets information about method of controller 
            // methodInfo - gets metadata of method TryGetMethodInfo
            // he contains inforamarion about controller, if operation is success
            // the name of the controller method is used as the operation identifier
            // else - null
        }
    }
}
