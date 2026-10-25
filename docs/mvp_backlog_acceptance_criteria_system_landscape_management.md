# 📝 MVP Backlog & Acceptance Criteria
**Project:** System Landscape Management Application  
**Role:** Product Owner (Sarah)  
**Date:** 2025-08-25

This backlog turns the PRD + Architecture into **deliverable work** for the MVP. It’s grouped by epics, prioritized, and includes **user stories with acceptance criteria (Gherkin)** and **Definition of Done**.

---

## Legend
- **Priority:** P0 (launch critical), P1 (important), P2 (nice-to-have)
- **Estimate bucket:** S (≤1d), M (2–3d), L (4–6d)

---

## Epic A — System Registry (P0)
### A1. Create System
**Story:** As a System Owner, I can create a system with required fields so that it appears in the registry.
**AC (Gherkin):**
- Given I am authorized to create systems
  When I open **/systems/create** and submit Name, Category, Owner, Status
  Then the system is saved
  And a Version entry is created
  And I see the new system on **/systems**
**DoD:** Validation, audit log, unit tests, UI tests, error states.
**Size:** M

### A2. Edit System
**Story:** As a System Owner, I can edit a system’s details.
**AC:**
- Given I own the system
  When I edit details and save
  Then changes are persisted and versioned
  And unauthorized users cannot edit
**DoD:** Role checks, history captured, rollback link visible.
**Size:** M

### A3. List/Search Systems
**Story:** As an Architect, I can search/filter systems.
**AC:**
- Given the catalog
  When I filter by Owner/Category/Hosting/Status
  Then I see matching systems with paging and export
**DoD:** Server paging/sorting, export to CSV/XLSX, perf tested for 10k rows.
**Size:** M

---

## Epic B — Attribute Management (P0)
### B1. Define Attribute
**Story:** As an Architect, I can create attribute definitions (datatype, mandatory, allowed values, regex).
**AC:**
- Given I’m on **/attributes/create**
  When I set Name, DataType, Rules
  Then the definition is saved and active
**DoD:** Validation service integration, unit tests.
**Size:** M

### B2. Edit/Deactivate Attribute
**Story:** As an Architect, I can update or deactivate an attribute definition.
**AC:**
- Given an existing attribute
  When I change rules or deactivate
  Then changes apply to new edits and UI shows impact
**DoD:** Impact panel, migration note in audit.
**Size:** M

### B3. Backfill Wizard
**Story:** As an Architect, I can launch a wizard to backfill a new attribute across selected systems.
**AC:**
- Given a new attribute
  When I open Backfill Wizard and apply defaults/queries
  Then selected systems receive values and are versioned
**DoD:** Bulk command, progress UI, resumable jobs.
**Size:** L

---

## Epic C — Versioning & History (P0)
### C1. Version Log per Entity
**Story:** As Compliance, I can view change history for a system.
**AC:**
- Given a system
  When I open History tab
  Then I see timeline with who/when/what changed and diffs
**DoD:** Diff viewer, API query, export.
**Size:** M

### C2. Landscape Snapshot (as-of)
**Story:** As a CIO, I can view the landscape as of a past date.
**AC:**
- Given a date-time
  When I choose it in Snapshot Picker
  Then I see read-only values effective at that time
**DoD:** Snapshot query + UI, caching.
**Size:** M

### C3. Rollback
**Story:** As an Architect, I can restore prior values by creating a new corrective change.
**AC:**
- Given a historical version
  When I click Rollback
  Then a new forward change is created restoring values
**DoD:** Authorization, audit, confirmation flow.
**Size:** L

---

## Epic D — Dependencies (P1)
### D1. Add/Remove Dependency
**Story:** As an Architect, I can manage dependencies between systems.
**AC:**
- Given two systems
  When I add an edge in the Dependencies tab
  Then a relationship is saved and visible in graph
**DoD:** Validation for cycles/self-edge, graph update.
**Size:** M

### D2. Dependency Explorer
**Story:** As an Architect, I can explore a graph of system dependencies.
**AC:**
- Given the explorer
  When I filter by Domain/Owner/Criticality
  Then nodes/edges update and I can export PNG/SVG
**DoD:** JS interop, performance on 2k nodes.
**Size:** L

---

## Epic E — Reporting & Dashboards (P1)
### E1. CIO Dashboard KPIs
**Story:** As a CIO, I can see completeness %, migration readiness, core vs non-core.
**AC:**
- Given seeded data
  When I open /dashboards/cio
  Then KPIs render in < 2s and export works
**DoD:** Cached queries, tests.
**Size:** M

### E2. Saved Reports & Export
**Story:** As an Architect, I can save queries and export results.
**AC:**
- Given a filter set
  When I save it as a report
  Then it’s listed under /reports and exportable
**DoD:** Ownership, RBAC, CSV/XLSX.
**Size:** M

---

## Epic F — Workflow & Notifications (P1)
### F1. Request → Approve Change
**Story:** As a System Owner, I can request a sensitive change that an Architect approves.
**AC:**
- Given a locked attribute
  When I submit a change request
  Then an approver gets notified and can Approve/Reject
**DoD:** Policies, SignalR banner, email stub.
**Size:** L

### F2. Reminders
**Story:** As PO, I want reminders for stale ownership and pending updates.
**AC:**
- Given inactivity threshold
  When a system hasn’t been updated for 90 days
  Then owners receive reminders weekly
**DoD:** Scheduled job, throttling, audit.
**Size:** M

---

## Epic G — Security & RBAC (P0)
### G1. Roles & Policies
**Story:** As Admin, I can assign roles (CIO/Architect/SystemOwner/Compliance) and enforce policies.
**AC:**
- Given a user
  When I assign a role
  Then permissions reflect in UI and API
**DoD:** Policy tests, page guards.
**Size:** M

---

## Epic H — Excel Importer (P0)
### H1. Seed from SystemLandscape.xlsx
**Story:** As a Tech Lead, I can import the current spreadsheet to initialize the app.
**AC:**
- Given a mapping config
  When I run the importer
  Then Systems/Attributes/Values are created with Version: Seed
**DoD:** Dry-run mode, error report, idempotency.
**Size:** L

---

## Epic I — Observability (P0)
### I1. Telemetry & Logs
**Story:** As SRE, I can trace commands/queries and monitor KPIs.
**AC:**
- Given App Insights
  When traffic flows
  Then I see distributed traces, dashboards, and alerts
**DoD:** Correlation IDs, sampling, alert rules.
**Size:** M

---

## Epic J — CI/CD & IaC (P0)
### J1. Pipelines & Infra
**Story:** As DevOps, I can deploy dev/test/prod via pipelines and IaC.
**AC:**
- Given GitHub Actions/ADO
  When PR merges
  Then build/test/deploy runs; EF migrations applied; APIM imported
**DoD:** Rollback strategy, secrets in KV.
**Size:** L

---

## MVP Scope Summary
**P0 (Launch Critical):** A1, A2, A3, B1, B2, C1, C2, G1, H1, I1, J1  
**P1 (Important):** B3, D1, E1, E2, F1, F2, C3, D2  
**P2 (Later):** advanced analytics, automated discovery, CMDB integration

---

## Release Plan (Milestones)
- **M1 – Foundation (3–4 weeks):** G1, J1 (dev env), A1/A2/A3
- **M2 – Attributes & History (3–4 weeks):** B1/B2, C1/C2, I1
- **M3 – Import & Dashboards (3–4 weeks):** H1, E1/E2
- **M4 – Dependencies & Workflow (3–4 weeks):** D1, D2, F1/F2, C3

---

## Definition of Done (Global)
- Code reviewed, unit/integration tests passing; accessibility checks on UI.
- Telemetry added; feature flags where appropriate.
- RBAC enforced at API and UI; audit logs present for mutating actions.
- Documentation updated (README_DEV, admin guide, user help).

