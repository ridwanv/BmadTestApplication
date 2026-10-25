# Functional Requirements

## Core Functionalities

### System Registry

Central catalog of all systems with unique identifiers.

Ability to add, update, and remove systems.

Hierarchical grouping (e.g., system → subsystem → application).

### Attribute Management

Flexible schema to add new attributes without disrupting existing data.

Attributes include (but not limited to): hosting (current/future), COTS/custom, insource/outsource, application type, dependencies.

Validation rules to ensure completeness and accuracy (e.g., mandatory fields, standardized options).

### Version Control & Change History

Every update logged with timestamp, user, and old vs. new values.

Ability to retrieve historical snapshots of the landscape (point-in-time views).

Support for rollbacks if incorrect data is entered.

### Collaboration & Access Control

Role-based permissions (system owners vs. architects vs. IT leadership).

Workflow for system owners to update and architects/POs to validate.

Notifications/reminders for pending updates.

### Reporting & Dashboards

Predefined reports (e.g., list of systems per hosting environment, systems due for cloud migration, core vs non-core systems).

Customizable dashboards for CIOs and architects.

Export to Excel/PDF for external distribution.

### Dependency Mapping

Ability to record integrations and dependencies between systems.

Visual diagrams (graph/network view) to analyze impact of changes.

### Compliance & Audit Support

Audit-ready logs for regulatory checks (e.g., POPIA, internal IT governance).

Evidence tracking of ownership and updates.

### Scalability & Extensibility

Designed to handle 1000+ systems with hundreds of attributes.

API endpoints for integration with enterprise tools (e.g., Data API Builder, APIM).

Extensible for future use cases such as risk scoring, lifecycle management, or cost attribution.