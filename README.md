# BmadTestApplication

A Clean Architecture Blazor Server application for managing an enterprise system landscape. Implemented workflows include systems and subsystems, configurable attribute definitions, contacts, documents, tenants, audit trails, and system logs. Product requirements, architecture, stories, and BMAD workflow artifacts are maintained in `docs/`.

## Build and test

Install the .NET SDK required by the repository, then run `dotnet build CleanArchitecture.Blazor.slnx` and `dotnet test CleanArchitecture.Blazor.slnx`. Run the UI from `src/Server.UI`.

Database providers and migrators are under `src/Migrators`. Configure database and identity settings outside source control.
