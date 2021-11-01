# Project structure

## ".Data" project

### File structure

-   Contracts - Contains all the interfaces for our project.
-   Enums - Contains all enums for our project.
-   Models
-   Repositories
-   Validation

### Key concepts:

Repositories:

-   Get (\[externalData\], id, cancellationToken) -> entity
-   GetMany (\[externalData\], cancellationToken) -> entity
-   Create (entity, cancellationToken) -> void
-   Update (entity, cancellationToken) -> void

## ".Core" project

### File structure

-   Contracts - Contains all the interfaces for our project.
-   Authentication - Contains components related to the authentication process (e.g. Authentication context)
-   OperativeModels - Contains the implementations of all prototypes and layouts.
-   Services - Contains the implementations of all service interfaces.
-   Validation

### Key concepts:

Service:

-   Get (\[externalData\], id, cancellationToken) -> layout
-   GetMany (\[externalData\], cancellationToken) -> layout[]
-   Create (\[externalData\], prototype, cancellationToken) -> layout
-   Update (\[externalData\], id, prototype, cancellationToken) -> layout
