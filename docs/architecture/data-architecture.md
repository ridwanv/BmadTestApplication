# Data Architecture

## Core Tables (Azure SQL)

Systems (Id PK, Name, Description, Category, Type, CotsCustom, HostingCurrent, HostingTarget, InsourceOutsource, OwnerId FK Users, Status, TenantId?, CreatedOn, UpdatedOn)

Subsystems (Id PK, SystemId FK, Name, Type)

AttributeDefinitions (Id PK, Name, DataType [string,int,decimal,bool,date,enum,json], IsMandatory, AllowedValuesJson, Regex, Category, IsActive)

SystemAttributeValues (SystemId FK, AttributeId FK, Value nvarchar(max), EffectiveFrom, EffectiveTo, VersionId FK, CreatedBy, CreatedOn)

Dependencies (SystemId FK, DependsOnSystemId FK, RelationshipType, Notes)

Versions (Id PK, Timestamp, UserId, ChangeType, PayloadJson, CorrelationId)

ChangeLogs (Id PK, EntityName, EntityId, Property, OldValue, NewValue, UserId, Timestamp, VersionId FK)

Pattern: EAV with typed validation on input. Use computed views for reporting (e.g., vw_System_CurrentAttributes).

## Indexing & Performance

Covering indexes on (AttributeId, SystemId, EffectiveFrom DESC); filtered views for current records (EffectiveTo IS NULL).

Graph queries: index (SystemId, DependsOnSystemId).

## Snapshots & Rollback

Snapshot = materialized query using latest values <= asOf.

Rollback = produce a new forward change that restores prior values (keeps audit linear and immutable).