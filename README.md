# CustomersManagement API

## Source Code

### CustomersManagement.Presentation

> Contains the web api project

### CustomersManagement.Application

> Contains the business logic and the customers entity

### CustomersManagement.Infrastructure

> Contains the database implementation using entity framework core

## Tests

### CustomersManagement.CustomersServiceUnitTestWithMoqing

> Contains the unit tests for the customer service without using a real database with moq techniques

### CustomersManagement.DatabaseIntegrationTests

> Contains the integration tests for the connection with the sql server database

### CustomersManagement.PresentationIntegrationTests

> Contains the integration tests for the /customers endpoint
