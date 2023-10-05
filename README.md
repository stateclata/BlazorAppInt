Blazor WebAssembly Application for Epsilon Interview

Below is a list of the website's functionalities:

    Customer Page: Displays a grid of all customers with server-side paging and supports CRUD operations on the Customer table.
    Client-Side Class: Implements logic to display object names based on their class, identifying whether they are instances of 'Employee' or 'Manager'.
    Identity Authentication: Implemented using JWT tokens sent in HTTP headers. For detailed implementation, refer to CustomAuthStateProvider.cs on the Client Side. The token is stored in the Server Side's appsettings.json.
    Server Calls: All server calls are handled by services on the Client Side. These services make HTTP requests to the Server Controller, and corresponding tasks are managed by the respective Server Services.
    UI Components: Utilizes MudBlazor, a component library, for most of the User Interface elements.
    Technology Stack: Developed with .NET 6 (LTS) as per the requested specifications.
    Database: Utilizes SQL Server with Entity Framework Core for database operations.
    Bootstrap Integration: Bootstrap framework is included within MudBlazor for styling and layout purposes.
