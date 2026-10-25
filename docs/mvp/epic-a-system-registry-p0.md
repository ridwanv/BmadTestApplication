# Epic A — System Registry (P0)
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
**Story:** As a System Owner, I can edit a system's details.
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