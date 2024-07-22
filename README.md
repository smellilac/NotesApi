<p align="center">
      <img src="https://i.ibb.co/hHsb7Dk/img-244368.png" width="726">
</p>

<p align="center">
   <img src="https://img.shields.io/badge/.Net%20Core-8.0.200-green" alt=".Net Core Version">
   <img src="https://img.shields.io/badge/Duende.IdentityServer-7.0.5-blueviolet" alt="Duende Identity Server Version">
   <img src="https://img.shields.io/badge/EntityFrameworkCore-8.0.6-firebrick" alt="Entity Framework Core Version">
   <img src="https://img.shields.io/badge/License-MIT-lime" alt="MIT License">
</p>

## About

This project is composed of two ASP.NET Core APIs and a React application built with TypeScript. The first API handles note management functionality and leverages technologies such as .NET Core 8.0, ASP.NET Core 8.0, AutoMapper, MediatR with CQRS, Serilog, FluentValidation, and follows a clean architecture with established design patterns. The second API, using Duende IdentityServer, is dedicated to client access and resource protection. The third component is a React application that serves as the client interface for interacting with these APIs. This project is essential for developers looking for a robust example of modern web application architecture, offering both secure authentication and efficient note management features.

### Technologies, Patterns, Architecture

- .NET Core 8.0.200
- ASP.NET Core 8.0
- AutoMapper 13.0.1
- MediatR 12.3.0 + CQRS
- Serilog 4.0.0
- FluentValidation 11.9.1
- Duende.IdentityServer 7.0.5
- XUnit 2.5.3
- Entity Framework Core 6.0
- PostgreSQL
- ApiVersioning
- Swagger
- Clean Architecture
- React
- Xml Documetation

### What`s includes

```
Notes.Backend.sln
│
├───Notes.Application
│   │   DependencyInjections.cs
│   │   Notes.Application.csproj
│   │      
│   ├───Common
│   │   ├───Behaviors
│   │   │       LoggingBehavior.cs
│   │   │       ValidationBehavior.cs
│   │   │
│   │   ├───CustomExceptions
│   │   │       NotFoundException.cs
│   │   │
│   │   └───Mappings
│   │           AssemblyMappingProfile.cs
│   │           IMapWith.cs
│   │
│   ├───Interfaces
│   │       ICurrentUserService.cs
│   │       INotesDbContext.cs
│   │
│   ├───NotesCQRS
│   │   ├───Commands
│   │   │   ├───CreateNote
│   │   │   │       CreateNoteCommand.cs
│   │   │   │       CreateNoteCommandHandler.cs
│   │   │   │       CreateNoteCommandValidator.cs
│   │   │   │
│   │   │   ├───DeleteNote
│   │   │   │       DeleteNoteCommand.cs
│   │   │   │       DeleteNoteCommandHandler.cs
│   │   │   │       DeleteNoteCommandValidator.cs
│   │   │   │
│   │   │   └───UpdateNote
│   │   │           UpdateNoteCommand.cs
│   │   │           UpdateNoteCommandHandler.cs
│   │   │           UpdateNoteCommandValidator.cs
│   │   │
│   │   └───Queries
│   │       ├───GetNoteDetails
│   │       │       GetNoteDetailsQuery.cs
│   │       │       GetNoteDetailsQueryValidator.cs
│   │       │       NoteDetailsQueryHandler.cs
│   │       │       NoteDetailsVm.cs
│   │       │
│   │       └───GetNoteList
│   │               GetNoteListQuery.cs
│   │               GetNoteListQueryHandler.cs
│   │               GetNoteListQueryValidator.cs
│   │               NoteListVm.cs
│   │               NoteLookUpDto.cs
├───Notes.Domain
│   │   Note.cs
│   │  
├───Notes.Persistance
│   │   DbInitializer.cs
│   │   DependencyInjection.cs
│   │   Notes.Persistance.csproj
│   │   NotesDbContextPostgre.cs
│   │
│   ├───EntityTypeConfiguration
│   │       NoteConfiguration.cs
│   │
├───Notes.Tests
│   │   Notes.Tests.csproj
│   │
│   │
│   ├───Common
│   │       NotesContextFactory.cs
│   │       QueryTestFixture.cs
│   │       TestCommandBase.cs
│   │
│   │
│   └───Tests
│       ├───Commands
│       │       CreateNoteCommandHandlerTests.cs
│       │       DeleteNoteCommandHandlerTests.cs
│       │       UpdateNoteCommandHandlerTests.cs
│       │
│       └───Queries
│               GetNoteDetailsQueryHandlerTests.cs
│               GetNoteListQueryHandlerTests.cs
│
└───Notes.WebApi
    │   appsettings.Development.json
    │   appsettings.json
    │   Notes.WebApi.csproj
    │   Notes.WebApi.csproj.user
    │   Program.cs
    │
    ├───Controllers
    │       BaseController.cs
    │       NoteController.cs
    │
    ├───Logs
    │       NotesWebAppLog-20240623.txt
    │       NotesWebAppLog-20240625.txt
    │       NotesWebAppLog-20240626.txt
    │       NotesWebAppLog-20240627.txt
    │       NotesWebAppLog-20240704.txt
    │       NotesWebAppLog-20240706.txt
    │       NotesWebAppLog-20240714.txt
    │       NotesWebAppLog-20240715.txt
    │       NotesWebAppLog-20240716.txt
    │
    ├───Middleware
    │       CustomExceptionHandlerMiddleware.cs
    │       CustomExceptionHandlerMiddlewareExtensions.cs
    │
    ├───Models
    │       CreateNoteDto.cs
    │       UpdateNoteDto.cs
    │
    │
    ├───Services
    │       CurrentUserService.cs
    │
    └───SwaggerConfiguration
            ConfigureSwaggerOptions.cs
```

```
Notes.Identity
    │   Notes.Identity.sln
    │
    └───Notes.Identity
        │   appsettings.Development.json
        │   appsettings.json
        │   Configuration.cs
        │   Notes.Identity.csproj
        │   Notes.Identity.csproj.user
        │   Program.cs
        │
        ├───Controllers
        │       AuthController.cs
        │
        ├───Data
        │       AppUserConfiguration.cs
        │       AuthDbContext.cs
        │       DbInitializer.cs
        │
        ├───DebugModelProperties
        │       DebugAppUserRegisterProccess.cs
        │
        ├───keys
        │       is-signing-key-6C3D662A8C8A65FB655DDAD9DF2AE1A4.json
        │
        ├───Logs
        │       NotesWebIdentityLog-20240716091708.txt
        │       NotesWebIdentityLog-20240716091826.txt
        │       NotesWebIdentityLog-20240716091918.txt
        │       NotesWebIdentityLog-20240716094250.txt
        │       NotesWebIdentityLog-20240716094700.txt
        │       NotesWebIdentityLog-20240716094737.txt
        │       NotesWebIdentityLog-20240716110947.txt
        │       NotesWebIdentityLog-20240716112235.txt
        │       NotesWebIdentityLog-20240716112307.txt
        │       NotesWebIdentityLog-20240716113245.txt
        │
        ├───Models
        │       AppUser.cs
        │       LoginViewModel.cs
        │       RegisterViewModel.cs
        │
        ├───Properties
        │       launchSettings.json
        │
        ├───Styles
        │       app.css
        │
        └───Views
            │   _ViewImports.cshtml
            │
            └───Auth
                    Login.cshtml
                    Register.cshtml
```

```
Notes.Frontend
└───src
    │   App.css
    │   App.test.tsx
    │   App.tsx
    │   index.css
    │   index.tsx
    │   logo.svg
    │   react-app-env.d.ts
    │   reportWebVitals.ts
    │   setupTests.ts
    │
    ├───api
    │       api.ts
    │       client-base.ts
    │
    ├───auth
    │       auth-headers.ts
    │       auth-provider.ts
    │       SignInOidc.tsx
    │       SignOutOidc.tsx
    │       user-service.ts
    │
    └───notes
            NoteList.tsx
```

### Demonstration of the application

#### Main Page
<p align="center">
  <img src="https://i.ibb.co/sqB2BVr/MainStr.png">
</p>

#### Login Page
<p align="center">
  <img src="https://i.ibb.co/XY8pLBc/Login.png" >
</p>

#### Sign Up Page
<p align="center">
  <img src="https://i.ibb.co/GJQHq38/Signup.png" >
</p>

#### Success Token
<p align="center">
  <img src="https://i.ibb.co/zJjZG2L/Succes-Token.png" >
</p>

#### Success Note
<p align="center">
  <img src="https://i.ibb.co/Fw8ftHq/Succes-Note.png" >
</p>

