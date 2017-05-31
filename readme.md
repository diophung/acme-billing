# ACME billing system
A sample enterprise application for creating / sending bill to customer.

# Requirements:
- Visual Studio 2015 with SP2
- Microsoft .NET Framework 4.6.1
- Connection to Internet (for Nuget package download)

# Application Architecture
- Multi-tier application with seperation of concern: seperate Domain Model, Business Logic, Application tier into different projects.
- Implemented with Domain-Driven-Design methodology and repository pattern. 
- Data storage is flexible since the Repository pattern can swap out the implementation. Currently using in-memory but can always switch to database / text file.
- Use Inversion of Control and Dependency Injection to maximize flexibility and testability.

# Entity Relationship Diagram
![ERD diagram](https://github.com/diophung/acme-billing/blob/master/ACME_Billing_ERD.png "ERD diagram")


# Design consideration:
- System must be fault tolerant and indempotent. Therefore, we keept track of the generated bill and email to avoid sending/generating duplicate items. Use proper error handling and logging to maximize tracability while still maintaining fault-tolerant.
- This application integrates with other meter system via REST API. A good REST client (RestSharp) is used for this task.

# To-do:
- Implementing data storage using proper ORM (Object Relational Mapping) such as Nhibernate or Entity Frameoworks.
- Complete unit tests and integration tests for the REST API. 
- Complete UI (MVC) and console interface.
- Handling concurrency, latency, async REST API call.