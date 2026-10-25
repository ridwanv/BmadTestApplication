# User Experience

## User Personas

- CIO / IT Leadership – Needs strategic dashboards, reports, and insights.
- Enterprise Architects – Needs system maps, dependencies, and point-in-time snapshots.
- System Owners / Application Managers – Needs easy forms to update system and subsystem details, manage hierarchies.
- Change Managers / Release Managers – Needs impact analysis views and change history.
- Compliance & Audit Teams – Needs audit logs and data integrity validation.

## UX Design Goals

- Simple onboarding, minimal training required.
- Role-based interfaces tailored to persona.
- Search & filter-centric navigation.
- Graph-based dependency maps and visual dashboards.
- Hierarchical system management with tree/indentation UI for parent-child relationships.
- Responsive design for desktop (primary) with mobile support.
- Validation-driven data entry to reduce errors.

## User Flows

- **System Owner Flow:** Log in → View assigned systems → Manage subsystems (add/edit/delete) → Update attributes → Submit.
- **Architect Flow:** Browse/filter → Explore system and subsystem hierarchies → Export report.
- **QA Flow:** Run UI tests for CRUD, hierarchy, and accessibility scenarios.
- **CIO Flow:** Review dashboard → Export insights.
- **Compliance Flow:** Retrieve version history → Export audit log.

## Subsystem Management

System Owners and Application Managers can now:
- Add, edit, or delete subsystems under any system
- View and manage hierarchical relationships in a tree UI
- Perform bulk operations with cascade rules (e.g., deleting a system removes all subsystems)
- Benefit from validation that prevents circular references and enforces business rules

## Accessibility & Testing

- All new UI components (SubsystemFormDialog, SystemHierarchyTree) are accessible and tested with bUnit and Playwright.
- Error handling and validation feedback are provided inline.