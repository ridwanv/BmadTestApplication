# Epic C — Versioning & History (P0)
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