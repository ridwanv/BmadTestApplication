# CleanArchitecture Mapping (Template Alignment)

Server.UI (Blazor)     → Razor pages/components, MudBlazor, auth UI, views
Application            → CQRS (Handlers/Requests), DTOs, Validation, Policies
Application.Core*      → Entities/Enums/DTOs (project preference)
Domain                 → (Optional thin domain)
Infrastructure         → EF Core, Repositories, Configs, Interceptors, External services
Migrators              → EF migrations per provider (MSSQL)


Project preference: New entities/enums live in Application.Core. EF configurations reside in Infrastructure. Domain can remain thin or evolve later.

## Feature Folders (Application)

Systems: Create/Update/Delete/Get/Search

Attributes: Create/Update/List attribute definitions

SystemAttributes: SetValue, BulkBackfill, GetValues

Dependencies: Add/Remove/List, GetGraph

Versioning: GetHistory, GetSnapshot, Rollback

Ownership/Workflow: AssignOwner, RequestChange, ApproveChange, RemindPending