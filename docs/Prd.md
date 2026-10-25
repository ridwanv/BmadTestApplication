📑 Product Requirements Document (PRD)

System Landscape Management Application

1. Goals & Background Context
Goals

The primary goals of this system are to:

Replace Excel with a Structured Repository – Eliminate inefficiencies and risks of manually tracking system attributes in spreadsheets.

Ensure Versioned System Landscape – Provide full historical visibility of changes to system attributes for audit and compliance purposes.

Enable Attribute Expansion – Allow new attributes to be introduced and consistently applied without requiring disruptive rework.

Improve Collaboration – Empower system owners, architects, and leadership to contribute to and maintain data accuracy with role-based access.

Support Strategic Decision-Making – Provide CIOs and IT leadership with reliable insights (e.g., cloud migration readiness, core vs non-core classification).

Strengthen Governance & Compliance – Ensure all data meets POPIA and internal audit requirements through structured validation and change history.

Scale for Growth – Support hundreds to thousands of systems with minimal administrative overhead, enabling future expansion.

Background Context

Currently, the system/application landscape is being tracked in a single Excel spreadsheet. While this sheet contains a wealth of information (system IDs, names, descriptions, hosting details, COTS/custom status, application type, ownership, etc.), it suffers from several critical limitations:

Data Integrity Issues: Manual updates cause inconsistencies, duplication, and outdated entries.

Limited Access Control: Excel lacks robust role-based permissions, making it unsuitable for collaborative upkeep.

Governance Gaps: Critical attributes such as hosting, insource vs outsource, and security classification are difficult to validate and prone to error.

Inefficient Insights: Generating reports or dashboards for CIOs and architects is slow and requires manual manipulation.

Scalability Constraints: As more systems and attributes are added, Excel becomes increasingly unmanageable and cannot support dependency mapping or advanced analytics.

This project addresses these issues by building a centralized, versioned, web-based application that integrates governance, reporting, and scalability from the start.

2. Functional Requirements
Core Functionalities

System Registry

Central catalog of all systems with unique identifiers.

Ability to add, update, and remove systems.

Hierarchical grouping (e.g., system → subsystem → application).

Attribute Management

Flexible schema to add new attributes without disrupting existing data.

Attributes include (but not limited to): hosting (current/future), COTS/custom, insource/outsource, application type, dependencies.

Validation rules to ensure completeness and accuracy (e.g., mandatory fields, standardized options).

Version Control & Change History

Every update logged with timestamp, user, and old vs. new values.

Ability to retrieve historical snapshots of the landscape (point-in-time views).

Support for rollbacks if incorrect data is entered.

Collaboration & Access Control

Role-based permissions (system owners vs. architects vs. IT leadership).

Workflow for system owners to update and architects/POs to validate.

Notifications/reminders for pending updates.

Reporting & Dashboards

Predefined reports (e.g., list of systems per hosting environment, systems due for cloud migration, core vs non-core systems).

Customizable dashboards for CIOs and architects.

Export to Excel/PDF for external distribution.

Dependency Mapping

Ability to record integrations and dependencies between systems.

Visual diagrams (graph/network view) to analyze impact of changes.

Compliance & Audit Support

Audit-ready logs for regulatory checks (e.g., POPIA, internal IT governance).

Evidence tracking of ownership and updates.

Scalability & Extensibility

Designed to handle 1000+ systems with hundreds of attributes.

API endpoints for integration with enterprise tools (e.g., Data API Builder, APIM).

Extensible for future use cases such as risk scoring, lifecycle management, or cost attribution.

3. User Experience
User Personas

CIO / IT Leadership – Needs strategic dashboards, reports, and insights.

Enterprise Architects – Needs system maps, dependencies, and point-in-time snapshots.

System Owners / Application Managers – Needs easy forms to update system details.

Change Managers / Release Managers – Needs impact analysis views and change history.

Compliance & Audit Teams – Needs audit logs and data integrity validation.

UX Design Goals

Simple onboarding, minimal training required.

Role-based interfaces tailored to persona.

Search & filter-centric navigation.

Graph-based dependency maps and visual dashboards.

Responsive design for desktop (primary) with mobile support.

Validation-driven data entry to reduce errors.

User Flows

System Owner Flow: Log in → View assigned systems → Update attributes → Submit.

Architect Flow: Browse/filter → Explore dependencies → Export report.

CIO Flow: Review dashboard → Export insights.

Compliance Flow: Retrieve version history → Export audit log.

4. Non-Goals & Success Metrics
Non-Goals (Out of Scope for MVP)

Not a full CMDB replacement (e.g., ServiceNow).

No automated system discovery.

No financial/cost allocation in MVP.

Dependency mapping is manual, not automated.

No AI-driven analytics in MVP.

Success Metrics

95%+ completeness of system attributes.

100% of changes logged and versioned.

New attributes added & populated in < 1 week.

≥70% of system owners update systems quarterly.

CIO reports generated in hours vs weeks.

≥80% user satisfaction score in MVP review.

5. Technical Architecture Considerations
Technology Stack

Frontend: Blazor Server (extendable to Blazor WASM).

Backend: .NET 8 Web API.

Database: Azure SQL Database.

Hosting: Azure Landing Zones.

Authentication: Azure AD B2C or IdentityServer4.

APIs: REST APIs exposed via Azure API Management.

Core Components

System Registry Module – CRUD + version history.

Attribute Management Engine – Dynamic schema with validations.

Access Control & Workflow – Role-based permissions and approval flows.

Reporting Layer – Dashboards, exports, and dependency visualization.

Compliance Logging – Immutable audit trail + integration with Azure Log Analytics.

Non-Functional Requirements

Scalability: Handle thousands of systems.

Performance: Optimized queries and caching.

Security: Key Vault, RBAC, defense in depth.

Extensibility: API-first, future integration with CMDB and analytics.

DevOps: Automated CI/CD pipelines.