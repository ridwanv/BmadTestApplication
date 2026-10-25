# Epic B — Attribute Management (P0)
### B1. Define Attribute
**Story:** As an Architect, I can create attribute definitions (datatype, mandatory, allowed values, regex).
**AC:**
- Given I'm on **/attributes/create**
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