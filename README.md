# Employee Management API

This is an Employee Management API built using ASP.NET Core 6.0 and Blazor. The application allows you to manage employees and their assignments to construction sites. It includes features for adding, removing, and listing employees, as well as managing their roles and site assignments.

## Features

- **Employee Management**: Add, remove, and list employees.
- **Construction Site Management**: Manage construction sites and their assigned employees.
- **API Key Authorization**: Secure your API endpoints with API key authorization.
- **Swagger UI**: Test and explore the API using Swagger UI.
- **Blazor Components**: Manage employees through a Blazor frontend.

## Technologies Used

- ASP.NET Core 6.0
- Entity Framework Core
- MediatR for CQRS pattern
- blazor web server
- Swagger for API documentation
- AutoMapper for object-object mapping

## Getting Started

### Prerequisites

- .NET 6.0 SDK
- A database (e.g., SQL Server)

### Setup

1. **Clone the repository:**

    ```bash
    git clone https://github.com/Bakkar1/Mbark_For_Build_Software.git
    cd employee-management-api
    ```

2. **Configure the database connection:**

    Update the `appsettings.json` file with your database connection string:

    ```json
    {
        "ConnectionStrings": {
            "DefaultConnection": "YourDatabaseConnectionString"
        }
    }
    ```

3. **Run database migrations:**

    ```bash
    dotnet ef database update
    ```

4. **Run the application:**

    ```bash
    dotnet run
    ```

    The API will be available at `https://localhost:44332/`.

### Ensuring Database Creation

The application ensures that the database is created and migrations are applied when it runs for the first time. This is handled in the `Program.cs` file by invoking `context.Database.Migrate()`.

### API Documentation

You can explore and test the API using Swagger UI. Once the application is running, navigate to `https://localhost:44332/swagger` in your browser.

### API Key Authorization

To secure your endpoints, API key authorization has been implemented. Requests to protected endpoints must include an `api-key` header with a valid API key.

### Adding Employees

To add employees to a construction site, use the following API endpoint:

- **POST /api/construction-sites/{constructionSiteId}/employees**

    Request Body:
    ```json
    {
        "constructionSiteId": 1,
        "employeeIds": [1, 2, 3]
    }
    ```

### Removing Employees

To remove an employee from a construction site, use the following API endpoint:

- **DELETE /api/construction-sites/{constructionSiteId}/employees/{employeeId}**

    Request Parameters:
    - `constructionSiteId`: The ID of the construction site.
    - `employeeId`: The ID of the employee to remove.

### Future Enhancements
- **Here are some features and improvements that can be added in the future:**

    - Integration Testing: Add integration tests to ensure the different parts of the application work together as expected.
    - Secret Management: Use secrets instead of appsettings.json for sensitive data. Consider using Azure Key Vault for managing secrets securely.
    - Azure Active Directory Integration: Integrate with Azure AD for better identity management and secure access.
    - ASP.NET Core Identity Roles: Use the built-in ASP.NET Core Identity roles for managing employee roles more effectively.

### Blazor Frontend

The Blazor frontend provides a user interface for managing employees and construction sites. The frontend can be accessed at `https://localhost:44382/`.

#### Loading Employees from the API

The Blazor component to list employees is located at `/Pages/ApiEmployee.razor`. It loads employees from the API using the configured `IHttpClientFactory`.
```razor
@page "/api-employee"
@inject IMediator _mediator
@using EmployeeBlazorApp.Components;
@inject IHttpClientFactory HttpClientFactory


<h3 class="mt-4 text-center">Api Employees</h3>
<a class="btn btn-primary" href="add-employee">Add New Employee</a>

<EmployeeListComponent employees="employees" />

@code {
    private List<EmployeeDTO>? employees = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadEmployees();
    }

    private async Task LoadEmployees()
    {
        try
        {
            var client = HttpClientFactory.CreateClient("ApiHttpClient");
            employees = await client.GetFromJsonAsync<List<EmployeeDTO>>("api/EmployeeRead");
        }
        catch (Exception ex)
        {
            // Handle error (e.g., log the error, show a message to the user, etc.)
            Console.WriteLine($"Error loading employees: {ex.Message}");
        }
    }
}
