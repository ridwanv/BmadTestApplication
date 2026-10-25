# Epic D — Dependencies (P1)
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