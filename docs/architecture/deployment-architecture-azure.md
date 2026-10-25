# Deployment Architecture (Azure)

Resource Group: rg-archsphere-prod (example)
Core Resources:

App Service Plan + Web App (blazor-api-ui)

Azure SQL (sql-archsphere)

Azure Key Vault (kv-archsphere)

API Management (apim-archsphere)

Application Insights (appi-archsphere)

Log Analytics Workspace (law-archsphere)

(Optional) Storage Account for exports/backups; Event Grid for future integrations

Network: Private endpoints for SQL & KV (prod), WAF on Front Door (optional), APIM external.
Environments: dev, test, prod with separate RGs and pipelines.