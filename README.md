# Employee Management API

This is an Employee Management API built using ASP.NET Core 6.0 and Blazor. The application allows you to manage employees and their assignments to construction sites. It includes features for adding, removing, and listing employees, as well as managing their roles and site assignments.

## Features

- **Employee Management**: Add, remove, and list employees.
- **Construction Site Management**: Manage construction sites and their assigned employees.
- **API Key Authorization**: Secure your API endpoints with API key authorization.
- **Swagger UI**: Test and explore the API using Swagger UI.
- **Blazor Components**: Manage employees through a Blazor WebAssembly frontend.

## Technologies Used

- ASP.NET Core 6.0
- Entity Framework Core
- MediatR for CQRS pattern
- Blazor WebAssembly
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

    The API will be available at `https://localhost:5001`.

### API Documentation

You can explore and test the API using Swagger UI. Once the application is running, navigate to `https://localhost:5001/swagger` in your browser.

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

### Blazor Frontend

The Blazor frontend provides a user interface for managing employees and construction sites. The frontend can be accessed at `https://localhost:5001`.

#### Loading Employees from the API

The Blazor component to list employees is located at `/Pages/EmployeeList.razor`. It loads employees from the API using the configured `HttpClient`.

```razor
@page "/api-employee"
@inject HttpClient Http
@using EmployeeBlazorApp.Components

<h3 class="mt-4 text-center">Manage Employee</h3>
<a class="btn btn-primary" href="add-employee">Add New Employee</a>

<h3 class="mt-4 text-center">Employees</h3>

@if (employees != null && employees.Any())
{
    <EmployeeListComponent employees="employees" />
}
else
{
    <p>No employees found.</p>
}

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
            employees = await Http.GetFromJsonAsync<List<EmployeeDTO>>("api/employees");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading employees: {ex.Message}");
        }
    }
}
