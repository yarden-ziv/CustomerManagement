Customer Management System (ASP.NET MVC + SQL + XML Tools)

A multi-project solution built with ASP.NET MVC (.NET 10), Entity Framework Core, SQL Server, and XML serialization.
The system includes:

A full MVC Web Application for creating and managing customers

A Customer Export console tool to generate an XML backup

A Customer Import console tool to read and display the XML data

Integration with an external API for Bank and Branch data

A SQL Server Stored Procedure for loading customers efficiently

This project was designed as a real-world style assignment demonstrating backend development, database architecture, external API consumption, XML tooling, and UI implementation.

## 📁 Project Structure

```
CustomerManagement/
│
├── MVC WebApp/                # Main ASP.NET MVC application
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── Data/
│   ├── Migrations/
│   └── CustomerManagement.csproj
│
├── CustomerExport/            # Console app: Exports customers to XML
│   ├── Program.cs
│   └── CustomerExport.csproj
│
└── CustomerImport/            # Console app: Imports XML and prints data
    ├── Program.cs
    └── CustomerImport.csproj
```


Features
ASP.NET MVC Web Application

Create new customers

Validate user input (Hebrew/English names, ID number, date of birth, etc.)

Dynamic dropdowns for banks and branches via external API

City list retrieved from SQL Server

Success/error messages and UI feedback

Full RTL/Hebrew support in the UI

SQL Server + Entity Framework Core

Database-first or code-first structure

Customers and Cities tables

Configurable connection string (appsettings.json)


Stored Procedure:

GetAllCustomers ->

used to load customer data efficiently

External API Integration

Banks and branches are loaded live using:

https://www.xnes.co.il/ClosedSystemMiddlewareApi/api/generalinformation

This keeps the bank/branch list always up to date.


XML Export Tool (Console App):

CustomerExport loads all customers from the database and generates:

customers.xml

The file is saved next to the console app and includes:

All customer fields

Related city data


XML Import Tool (Console App):

CustomerImport reads the customers.xml file and prints all entries to the console.

customers.xml need to placed inside the root folder of "CustomerImport"

Note: Hebrew prints reversed in Windows console — this is a known limitation of cmd/PowerShell, not of the data.


**running instructions**

1. Prerequisites

You need:

.NET 10 SDK or latest (.NET 8 also works)

SQL Server or LocalDB

Visual Studio 2022 (recommended)

Git

2. Clone the Repository
git clone https://github.com/yarden-ziv/CustomerManagement.git


Open the solution:

CustomerManagement.sln

3. Configure the Database

In MVC WebApp/appsettings.json, set your connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CustomerManagementDb;Trusted_Connection=True;"
}


Run migrations (optional):

Update-Database

4. Running Each Project
▶ Run the MVC Web Application

In Visual Studio:

Right-click CustomerManagement (under Web)

Select Set as Startup Project

Press F5

▶ Run the XML Export Tool

Generates customers.xml.

CustomerExport → Set as Startup Project → F5

▶ Run the XML Import Tool

Reads and prints customers.xml.

CustomerImport → Set as Startup Project → F5


Testing

Create customers in the Web App

Run CustomerExport → verify customers.xml

Run CustomerImport → verify printed output

Confirm Stored Procedure returns correct data

Test invalid inputs (future dates, incorrect ID format)

Simulate API failures (banks/branches unavailable)


Technologies Used

ASP.NET MVC (.NET 10)

Entity Framework Core

SQL Server / LocalDB

C#

XML Serialization

Bootstrap (UI)

External REST API

Stored Procedures

📌 Notes

Console apps do not modify the database

Hebrew text printed in a Windows terminal may appear reversed

The project is structured to be fully clone-and-run
