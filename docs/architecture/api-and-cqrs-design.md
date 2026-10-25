# API & CQRS Design

## Commands

- CreateSystem, UpdateSystem, DeleteSystem
- CreateSubsystem, UpdateSubsystem, DeleteSubsystem
- CreateAttributeDefinition, UpdateAttributeDefinition
- SetAttributeValue, BulkBackfillAttribute
- AddDependency, RemoveDependency
- AssignOwner, RequestChange, ApproveChange

## Queries

- GetSystemById, SearchSystems(filter/paging)
- GetSubsystemById, GetSubsystemsBySystem
- GetSystemHierarchy (returns full parent-child tree)
- ListAttributeDefinitions
- GetSystemAttributes(systemId)
- GetGraphForSystem(systemId) (nodes/edges)
- GetSystemHistory(systemId), GetLandscapeSnapshot(asOf)

## API Endpoints (Subsystems)

- `POST /api/systems/{systemId}/subsystems` - Create subsystem
- `GET /api/systems/{systemId}/subsystems` - List subsystems
- `PUT /api/subsystems/{id}` - Update subsystem
- `DELETE /api/subsystems/{id}` - Delete subsystem
- `GET /api/systems/{systemId}/hierarchy` - Get full hierarchy

## Validation & Policies

- FluentValidation per command (e.g., prevent circular references, enforce hierarchy rules)
- Policies: CanEditOwnSystems, CanApprove, CanViewAudit, CanManageSubsystems

## Notes

- Hierarchical queries are indexed for performance.
- Cascade delete/update is handled at the database and application layers.