# Blazor WASM application for Epsilon Interview #



    Below is a list of all the website's usages:
• Customer.razor page hosting a grid with all customers with server side paging & CRUD Operations on the Customer Table
• A simple Class on the Client Side that displays the object's name in case the object's class is 'Employee' or 'Manager'
• Identity Authentication has been achieved using Jwt Tokens in HTTP headers (more in CustomAuthStateProvider.cs on the Client Side & the token can be found on the Server Side inside "appsettings.json").
• All Server calls are being handled by services on the Client Side making an http call to the Server Controller and all tasks are handled by the respective Server Services.
• MudBlazor has been used for most of the UI Components.
• .Net 6 (LTS) has been used as requested.
• SQL Server w/ Entity Framework Core has been used for the database.
• Bootstrap was included in MudBlazor.
