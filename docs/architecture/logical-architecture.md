# Logical Architecture

## Modules

System Registry Service – CRUD for Systems/Subsystems/Applications; ownership & status.

Attribute Management Engine – AttributeDefinition + validators (regex/enums/required); EAV storage.

Version & Audit Service – Change capture, snapshots, rollback.

Dependency Service – Directed graph of system → system relationships.

Reporting/Dashboards – Read-optimized queries + export.

Workflow & Notifications – Requests/approvals; reminders (Hangfire/Azure WebJobs); email/Teams integration (future).

## Cross-Cutting

Authorization policies, caching, exception handling, telemetry, localization, multi-tenancy (optional).