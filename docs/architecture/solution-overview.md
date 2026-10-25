# Solution Overview

Frontend: Blazor Server (option to extend with WASM module later).
Backend: .NET 8 Web API with MediatR (CQRS), FluentValidation.
Data: Azure SQL (normalized + EAV for dynamic attributes).
Identity: Azure AD B2C or Entra ID; role claims for RBAC.
Integration: Azure API Management (APIM) as gateway.
Hosting: Azure App Service; optional AKS for future multi-tenant SaaS.

Key Capabilities:

System Registry, Attribute Management (dynamic), Versioning & History, Dependency Graph, Reporting/Dashboards, Governance Workflow (request → approve → publish), Audit Trail.