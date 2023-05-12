# OpenAPI Specifications

Provides all OpenAPI specifications which are being exposed to Contoso's customers via Azure API Management.

- **Orders**
- **Products**
- **Shipments**
- **Shipment Webhooks**

During the migration Contoso changed their APIs so that Order service can initiate new shipments by calling a REST endpoint exposed by the Shipments service.

Given this is not a public operation they did not have to update their OpenAPI specification.