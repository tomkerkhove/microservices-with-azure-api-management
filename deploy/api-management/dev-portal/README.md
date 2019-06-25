# Kubernetes deployment

Codito's customized Azure API Management developer portal.

This developer portal experience is currently in [public preview](https://azure.microsoft.com/en-us/updates/new-developer-portal-in-api-management-is-now-in-preview/), breaking changes might be applied.

More information:
- [Azure API Management developer portal GitHub repo](https://github.com/Azure/api-management-developer-portal)

## Self-Hosting the portal
You can run the developer portal yourself:
1. Change `<tenant>` in `config.json` with the name of your Azure API Management tenant
2. Configure an Azure Storage Account for static website ([docs](https://github.com/Azure/api-management-developer-portal/wiki/Self-hosting-the-portal#storage-account))
3. Upload this folder to `$web`