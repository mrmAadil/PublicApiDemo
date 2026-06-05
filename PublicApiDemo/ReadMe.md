# PublicApiDemo — ASP.NET Core Web API with SQL Server Caching

An ASP.NET Core 8 Web API that fetches country data from the [REST Countries API](https://restcountries.com/) and caches it in a local MS SQL Server database.

## Frameworks & Libraries

| Package | Reason |
|---------|--------|
| `Microsoft.Data.SqlClient` | ADO.NET provider for SQL Server. Required for raw SQL access without an ORM. |
| `Swashbuckle.AspNetCore` | Provides Swagger/OpenAPI documentation for easy API exploration. |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or full edition)

## Setup & Run

### 1. Create the Database

Run the schema script against your SQL Server instance:

### 2. Build & Run the API
1. Go to the project directory using windows powershell
2. Default connection string is set to local host, if you want to change it, execute use this comand to point the server you wanted
(Make sure you have executed the schema script and created the DB and Table)
	`$env:ConnectionStrings__DefaultConnection = "Server=<add_your_server>;Database=PublicApiDemo;Trusted_Connection=True;TrustServerCertificate=True;"`
3. Run following commands to Build and Run the API
	`dotnet build` &
	`dotnet run`