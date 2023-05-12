# Contoso Tomorrow

At BUILD 2019, the Azure API Management team announced **Azure API Management Gateway** which is a federated gateway which allows you to deploy the same Azure API Management gateway wherever you want!

This allows your consumers to communicate with a local gateway which is managed in the cloud allowing them to stay on the same network which reduces latency.

![Azure API Management Gateway](./../media/api-management-gateway.png)

This is good news for Contoso because they can now not only decouple external customers from their application, it brings the same benefits for internal customers.

When they've migrated to Kubernetes, the Order service was communicating to the Shipment service directly.

This means that the Shipment service has no control over how it's called, enforce throttling or route traffic to a new version for A/B testing.

![Contoso migration to Azure Web App for Containers](./../media/codito-phase-II-internals.png)

With Azure API Management Gateway, however, they can deploy the gateway inside the cluster and expose the Shipment service to their internal customers with the same API Gateway experience:

![Contoso using Azure API Management Gateway inside the cluster](./../media/codito-future.png)