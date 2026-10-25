🏗️ Fullstack Architecture Document

Project: System Landscape Management Application
Template Baseline: CleanArchitectureWithBlazorServer
Author: Architect (Winston)
Date: 2025-08-25

Note on UX Scope: This document includes a dedicated Frontend & UX Architecture section (Section 10). If you prefer UX to live in a separate Front-End Spec, we can extract Section 10 into its own document.

1. Purpose & Scope

This document defines the technical architecture for replacing the Excel-based system register with a versioned, web-based application. It maps the PRD into a concrete design aligned to CleanArchitectureWithBlazorServer. The scope includes logical/physical architecture, data model, security, API/CQRS, deployment, observability, migration, and UX architecture integration.

Objectives

Deliver a scalable, auditable, collaborative substitute for the Excel register.

Provide dynamic attribute management, version history, dependency mapping, and governed updates.

Align to enterprise standards: Azure, .NET 8, Blazor, Azure SQL, Azure AD (B2C/Entra), APIM.

Out of Scope (MVP)

Automated discovery or network scanning.

Full CMDB replacement.

Advanced cost analytics and AI-driven rationalization.

2. Architecture Principles

Clean Architecture – UI independent of application, application independent of infrastructure.

API-First & CQRS – Clear separation of commands (writes) and queries (reads).

Auditability by Design – All changes versioned and attributable.

Schema Extensibility – Add new attributes without schema churn.

Defense in Depth – AuthN/AuthZ at every layer; least privilege.

Operational Excellence – First-class observability, automated CI/CD.

Cost-Conscious Scalability – Start simple, scale predictably.

3. Solution Overview

Frontend: Blazor Server (option to extend with WASM module later).
Backend: .NET 8 Web API with MediatR (CQRS), FluentValidation.
Data: Azure SQL (normalized + EAV for dynamic attributes).
Identity: Azure AD B2C or Entra ID; role claims for RBAC.
Integration: Azure API Management (APIM) as gateway.
Hosting: Azure App Service; optional AKS for future multi-tenant SaaS.

Key Capabilities:

System Registry, Attribute Management (dynamic), Versioning & History, Dependency Graph, Reporting/Dashboards, Governance Workflow (request → approve → publish), Audit Trail.

4. CleanArchitecture Mapping (Template Alignment)
Server.UI (Blazor)     → Razor pages/components, MudBlazor, auth UI, views
Application            → CQRS (Handlers/Requests), DTOs, Validation, Policies
Application.Core*      → Entities/Enums/DTOs (project preference)
Domain                 → (Optional thin domain)
Infrastructure         → EF Core, Repositories, Configs, Interceptors, External services
Migrators              → EF migrations per provider (MSSQL)


Project preference: New entities/enums live in Application.Core. EF configurations reside in Infrastructure. Domain can remain thin or evolve later.

Feature Folders (Application)

Systems: Create/Update/Delete/Get/Search

Attributes: Create/Update/List attribute definitions

SystemAttributes: SetValue, BulkBackfill, GetValues

Dependencies: Add/Remove/List, GetGraph

Versioning: GetHistory, GetSnapshot, Rollback

Ownership/Workflow: AssignOwner, RequestChange, ApproveChange, RemindPending

5. Logical Architecture
5.1 Modules

System Registry Service – CRUD for Systems/Subsystems/Applications; ownership & status.

Attribute Management Engine – AttributeDefinition + validators (regex/enums/required); EAV storage.

Version & Audit Service – Change capture, snapshots, rollback.

Dependency Service – Directed graph of system → system relationships.

Reporting/Dashboards – Read-optimized queries + export.

Workflow & Notifications – Requests/approvals; reminders (Hangfire/Azure WebJobs); email/Teams integration (future).

5.2 Cross-Cutting

Authorization policies, caching, exception handling, telemetry, localization, multi-tenancy (optional).

6. Data Architecture
6.1 Core Tables (Azure SQL)

Systems (Id PK, Name, Description, Category, Type, CotsCustom, HostingCurrent, HostingTarget, InsourceOutsource, OwnerId FK Users, Status, TenantId?, CreatedOn, UpdatedOn)

Subsystems (Id PK, SystemId FK, Name, Type)

AttributeDefinitions (Id PK, Name, DataType [string,int,decimal,bool,date,enum,json], IsMandatory, AllowedValuesJson, Regex, Category, IsActive)

SystemAttributeValues (SystemId FK, AttributeId FK, Value nvarchar(max), EffectiveFrom, EffectiveTo, VersionId FK, CreatedBy, CreatedOn)

Dependencies (SystemId FK, DependsOnSystemId FK, RelationshipType, Notes)

Versions (Id PK, Timestamp, UserId, ChangeType, PayloadJson, CorrelationId)

ChangeLogs (Id PK, EntityName, EntityId, Property, OldValue, NewValue, UserId, Timestamp, VersionId FK)

Pattern: EAV with typed validation on input. Use computed views for reporting (e.g., vw_System_CurrentAttributes).

6.2 Indexing & Performance

Covering indexes on (AttributeId, SystemId, EffectiveFrom DESC); filtered views for current records (EffectiveTo IS NULL).

Graph queries: index (SystemId, DependsOnSystemId).

6.3 Snapshots & Rollback

Snapshot = materialized query using latest values <= asOf.

Rollback = produce a new forward change that restores prior values (keeps audit linear and immutable).

7. API & CQRS Design
7.1 Commands

CreateSystem, UpdateSystem, DeleteSystem

CreateAttributeDefinition, UpdateAttributeDefinition

SetAttributeValue, BulkBackfillAttribute

AddDependency, RemoveDependency

AssignOwner, RequestChange, ApproveChange

7.2 Queries

GetSystemById, SearchSystems(filter/paging)

ListAttributeDefinitions

GetSystemAttributes(systemId)

GetGraphForSystem(systemId) (nodes/edges)

GetSystemHistory(systemId), GetLandscapeSnapshot(asOf)

7.3 Validation & Policies

FluentValidation per command; policies: CanEditOwnSystems, CanApprove, CanViewAudit.

8. Security & Compliance

AuthN: Azure AD B2C/Entra ID (OIDC); SPA cookies for Blazor Server.

AuthZ: Role + policy-based; page & handler filters in UI; row-level tenant check (if MT).

Data Protection: Connection strings & secrets in Azure Key Vault.

Audit: Version + ChangeLogs persisted; optional stream to Azure Log Analytics.

Privacy/POPIA: Minimize PII; access logs; retention policies.

9. Deployment Architecture (Azure)

Resource Group: rg-archsphere-prod (example)
Core Resources:

App Service Plan + Web App (blazor-api-ui)

Azure SQL (sql-archsphere)

Azure Key Vault (kv-archsphere)

API Management (apim-archsphere)

Application Insights (appi-archsphere)

Log Analytics Workspace (law-archsphere)

(Optional) Storage Account for exports/backups; Event Grid for future integrations

Network: Private endpoints for SQL & KV (prod), WAF on Front Door (optional), APIM external.
Environments: dev, test, prod with separate RGs and pipelines.

10. Frontend & UX Architecture
10.1 Information Architecture & Navigation

Dashboards: CIO, Architect, Compliance views with KPIs, reports, audit summaries.

Catalog: Systems list/details, attributes, reports.

Administration: user/roles, policies, settings.

10.2 Route Map

(see detailed list in spec.md — includes /systems, /attributes, /dependencies, /reports, /dashboards/*, /audit).

10.3 Component Tree

Systems List Page → DataGrid, filters, bulk actions

System Details Page → Tabs: Overview, Attributes, Dependencies, History, Ownership

Attribute Definition Edit Page → DefinitionForm + PreviewValidator

Dependency Explorer → GraphCanvas + Inspector

Audit Log Page → FilterBar + AuditGrid

10.4–10.13

Detailed states (loading/empty/error/editing), validation, accessibility, responsiveness, theming, localization, workflow, reusable components, graph visualization, authorization UX, and Definition of Done quality bars.

11. Observability

Tracing/logs: Application Insights

Metrics: request rate, latency, error %, job success

Dashboards: ops + audit

Alerts: SLO violations, DB load, APIM errors

12. CI/CD & Infrastructure as Code

Pipelines: GitHub Actions/Azure DevOps

IaC: Bicep/Terraform for RG, App Service, SQL, KV, APIM

Database: EF migrations + automated backups

13. Data Migration (Excel → App)

Console importer to seed Systems + Attributes + Values

Mapping: Excel columns → AttributeDefinitions

History: initial import tagged as Version: Seed

14. Risks & Mitigations

EAV complexity → use validators & views

Over-permissive access → strict policies + audit dashboards

Performance → indexes, caching, pagination

Adoption → simple UX, training aids, reminders

15. Open Decisions

UX packaging

Identity provider choice (B2C vs Entra)

Multi-tenancy readiness

Graph library selection

16. Next Steps

Confirm UX scope preference

Approve data model & policies

Deploy dev env (IaC)

Scaffold feature slices

Run Excel importer + validate KPIs