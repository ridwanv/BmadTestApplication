# Epic J — CI/CD & IaC (P0)
### J1. Pipelines & Infra
**Story:** As DevOps, I can deploy dev/test/prod via pipelines and IaC.
**AC:**
- Given GitHub Actions/ADO
  When PR merges
  Then build/test/deploy runs; EF migrations applied; APIM imported
**DoD:** Rollback strategy, secrets in KV.
**Size:** L

---