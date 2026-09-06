# IT Help Desk System

The IT Help Desk System is an ASP.NET Core MVC application for creating, viewing, updating, tracking, and deleting technical support tickets.

The project uses the **Entity Framework Core Database-First approach**, where the database tables are created first and the models and `DbContext` are generated from the existing database.

## Features

* Create new support tickets
* View all submitted tickets
* Edit ticket information
* Delete tickets
* View complete ticket details
* Assign tickets to employees
* Select ticket category and priority
* Track ticket status:

  * Open
  * In Progress
  * Resolved
  * Closed
* Automatically record ticket creation time
* Automatically set the resolved date
* Display current weather in the navbar
* Detect the user's location with permission
* Responsive Bootstrap interface
* Priority and status badges

## Technologies Used

* ASP.NET Core MVC
* .NET 10
* C#
* Entity Framework Core
* Database-First Approach
* SQL Server
* Razor Views
* HTML and CSS
* Bootstrap
* JavaScript
* Open-Meteo Weather API
* Browser Geolocation API

## Database Tables

The application uses the following tables in the `Trainee_DB` database:

* `HD_Departments`
* `HD_Employees`
* `HD_Tickets`

The tables are connected using primary-key and foreign-key relationships.

## Database-First Workflow

1. The database and tables were created in SQL Server.
2. Entity Framework Core read the existing database schema.
3. Model classes and `HelpDeskDbContext` were generated using `Scaffold-DbContext`.
4. MVC scaffolding generated the initial CRUD controller and Razor views.
5. The generated pages were customized with dropdowns, validation and responsive styling.

## Weather API

The navbar displays current weather based on the user's location.

The application:

1. Requests location permission from the browser.
2. Gets the user's latitude and longitude.
3. Sends the coordinates to the Open-Meteo API.
4. Displays the returned temperature and weather condition.

No weather API key is required.

## Getting Started

### Prerequisites

* Visual Studio
* .NET 10 SDK
* SQL Server
* SQL Server Management Studio

### Configuration

Add your SQL Server connection string to `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "HelpDeskConnection": "Server=YOUR_SERVER_NAME;Database=Trainee_DB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name.

### Run the Project

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Configure the database connection.
4. Build the solution.
5. Run the application using `Ctrl + F5`.
6. Allow location permission to display current weather.

## Project Structure

```text
ITHelpDesk
├── Controllers
│   └── TicketsController.cs
├── Data
│   └── HelpDeskDbContext.cs
├── Models
│   ├── HdDepartment.cs
│   ├── HdEmployee.cs
│   └── HdTicket.cs
├── Views
│   ├── Shared
│   └── Tickets
├── wwwroot
│   ├── css
│   └── js
├── appsettings.json
└── Program.cs
```



## Author

**Titiksha**
