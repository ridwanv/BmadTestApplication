# Purpose & Scope

This document defines the technical architecture for replacing the Excel-based system register with a versioned, web-based application. It maps the PRD into a concrete design aligned to CleanArchitectureWithBlazorServer. The scope includes logical/physical architecture, data model, security, API/CQRS, deployment, observability, migration, and UX architecture integration.

## Objectives

Deliver a scalable, auditable, collaborative substitute for the Excel register.

Provide dynamic attribute management, version history, dependency mapping, and governed updates.

Align to enterprise standards: Azure, .NET 8, Blazor, Azure SQL, Azure AD (B2C/Entra), APIM.

## Out of Scope (MVP)

Automated discovery or network scanning.

Full CMDB replacement.

Advanced cost analytics and AI-driven rationalization.