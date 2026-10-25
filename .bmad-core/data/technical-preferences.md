<!-- Powered by BMAD™ Core -->

# User-Defined Preferred Patterns and Preferences

# ⚙️ Technical Preferences

**Project:** System Landscape Management Application
**Date:** 2025-08-25

This document records agreed **technical preferences, conventions, and defaults** for the project. It supplements the Architecture and PRD, ensuring consistency across development teams.

---

## 1. Language & Frameworks

* **.NET 8** for backend and APIs.
* **Blazor Server** for frontend (with option to extend to Blazor WASM later).
* **EF Core 8** for ORM, with interceptors for audit/version logging.
* **MediatR** for CQRS pattern.
* **FluentValidation** for validation.
* **MudBlazor** as the primary component library.

---

## 2. Project Template & Structure

* Base: **CleanArchitectureWithBlazorServer** template.
* Layering:

  * `Server.UI`: Blazor UI, pages, components, auth UI.
  * `Application`: CQRS handlers, policies, DTOs.
  * `Application.Core`: **all new entities/enums/DTOs**.
  * `Domain`: remains thin; may evolve later.
  * `Infrastructure`: EF configurations, repositories, external services.
  * `Migrators`: EF migrations, one per DB provider.

---

## 3. Database & Data Model

* **Azure SQL Database** primary store.
* **Entity-Attribute-Value (EAV)** model for dynamic attributes.
* **Naming conventions**:

  * Tables: PascalCase (e.g., `SystemAttributeValues`).
  * Columns: PascalCase.
* **Snapshots & Versioning**: Append-only `Versions` + `ChangeLogs`.
* **Tenancy**: include `TenantId` column on all tables (future SaaS).

---

## 4. API & Integration

* **REST APIs** exposed via Azure API Management.
* JSON camelCase payloads.
* Pagination default: 50 items, max 200.
* **Commands vs Queries**: separated via MediatR.
* **Error handling**: ProblemDetails (RFC 7807).

---

## 5. Authentication & Authorization

* **AuthN**: Azure AD B2C or Entra ID (OIDC).
* **AuthZ**: Role + policy-based (e.g., `CanEditOwnSystems`).
* Roles: CIO, Architect, SystemOwner, Compliance.
* Secrets stored in **Azure Key Vault**.

---

## 6. Coding Standards

* **C# 12 features** allowed.
* Nullable reference types enabled.
* Async/await required for all I/O.
* LINQ for queries; avoid raw SQL unless performance critical.
* Unit tests: xUnit.
* Integration tests: bUnit for Blazor, Testcontainers for EF.

---

## 7. UX & Frontend Conventions

* **MudBlazor** theme with light/dark toggle.
* Route paths: lowercase (`/systems/{id}`).
* Responsive breakpoints: desktop ≥1280px, tablet 768–1279px, mobile <768px.
* Validation: powered by central `ValidationService`.
* Accessibility: WCAG AA.

---

## 8. CI/CD & Infrastructure as Code

* Pipelines: GitHub Actions or Azure DevOps.
* Stages: build → test → publish → migrate DB → deploy.
* IaC: Bicep (preferred) or Terraform.
* Branch strategy: trunk-based, PR reviews required.

---

## 9. Observability

* **Application Insights** for tracing, metrics, logs.
* Correlation IDs passed through all layers.
* Dashboards for request rate, latency, error %, job success.
* Alerts: SLO breaches, DB DTU spikes, APIM 5xx.

---

## 10. Data Migration & Import

* **Excel Importer**: Console app with ClosedXML.
* Dry-run mode mandatory.
* Idempotent (safe to re-run).
* Initial import tagged as `Version: Seed`.

---

## 11. Documentation & Collaboration

* Architecture: `spec.md`.
* Product requirements: `prd.md`.
* Backlog: `mvp-backlog.md`.
* Preferences: `technical-preferences.md`.
* Dev setup: `README_DEV.md`.

---

## 12. Open Preferences (To Decide)

* Graph visualization: D3 via JS interop vs commercial component.
* Identity: AD B2C vs Entra ID.
* SaaS tenancy model: per-tenant DB vs shared DB + TenantId.

---

*End of document.*

