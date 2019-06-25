# Meet Codito
Codito provides industry-leading APIs for purchasing Microsoft Products.

Their customers can fully automate:
- Listing the product catalog
- Ordering products to be delivered at home
- Getting information about a shipment

In order to deliver all orders they have partnered with multiple 3rd parties to deliver shipments.
These 3rd party service providers are in charge providing updates about package deliveries.

This is handled by pushing status updates to a Codito webhook endpoint.

![Codito](./../media/codito.jpg)

## Transition to microservices
Codito wants to transition to microservices:
- Provide capability to easily ship new features
- Allow services to run on specialized compute
- Increase service ownership

But, we need to ensure that
- Customer experience does not change
- Developers have the capability to experiment with new approaches with A/B testing
- Customers have one central gateway for all microservices

## How do we get there?
Use a phased-migration approach to reduce complexity and risk

Microservices are a journey:
Split the monolith into multiple smaller services
Ship them as individual containers
Use as much PaaS as you can, until you need more control.

Azure App Services & Azure Integration Services are a good starting point.

Easily port your application to Azure Kubernetes Service, if you need more control.
