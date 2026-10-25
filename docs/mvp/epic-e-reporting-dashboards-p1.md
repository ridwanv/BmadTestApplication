# Epic E — Reporting & Dashboards (P1)
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
  Then it's listed under /reports and exportable
**DoD:** Ownership, RBAC, CSV/XLSX.
**Size:** M

---