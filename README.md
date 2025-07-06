# Employee Management System

A comprehensive web-based Employee Management System built with **ASP.NET Core 8** and **Blazor Server** technology. This application provides a modern, interactive interface for managing employees, departments, and locations within an organization.

## 🚀 Features

### Employee Management
- **Create, Read, Update, Delete (CRUD)** operations for employee records
- **Hierarchical Management** - Assign managers to employees
- **Employee Information Tracking**:
  - Personal details (First Name, Last Name)
  - Salary management
  - Department assignment
  - Location assignment
  - Manager relationships
![image](https://github.com/user-attachments/assets/08a661f9-95ec-4843-a4af-2d2c3bc4324e)
![image](https://github.com/user-attachments/assets/2ed870d4-8ff6-4f09-b494-d0bef20fd584)

### Department Management
- Complete CRUD operations for organizational departments
- Department-wise employee organization
- Real-time updates across the system
![image](https://github.com/user-attachments/assets/1fb1cd86-5a20-4e43-9570-0607a324fd64)

### Location Management
- Manage multiple office locations (City, Country)
- Location-based employee assignments
- Global workforce distribution tracking
![image](https://github.com/user-attachments/assets/9042b053-dc64-468f-9b2d-b99aa134a267)


### Technical Features
- **Real-time Updates** - Blazor Server components with interactive UI
- **Responsive Design** - Bootstrap-powered modern interface
- **Database Integration** - SQL Server with ADO.NET
- **Secure Operations** - Parameterized queries to prevent SQL injection
- **Error Handling** - Comprehensive error management
- **Clean Architecture** - Separation of concerns with services layer

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0
- **UI Technology**: Blazor Server Components
- **Database**: SQL Server with ADO.NET
- **Frontend**: HTML5, CSS3, Bootstrap
- **Architecture**: Clean Architecture with Service Layer Pattern
- **Development Environment**: Visual Studio 2022

## 📋 Prerequisites

Before running this application, ensure you have:

- **.NET 8.0 SDK** installed
- **SQL Server** (LocalDB, Express, or Full version)
- **Visual Studio 2022** or **Visual Studio Code**
- **Git** for version control

## ⚡ Quick Start

### 1. Clone the Repository
```bash
git clone https://github.com/Mamoonalatif/EmployeePortal.git
cd EmployeePortal
```

### 2. Database Setup
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EmployeeManagementDB;Trusted_Connection=True;Encrypt=False;"
  }
}
```

### 3. Create Database Schema
Execute the following SQL script to create the required tables:

```sql
-- Create Database
CREATE DATABASE EmployeeManagementDB;
USE EmployeeManagementDB;

-- Create Departments Table
CREATE TABLE Departments (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL
);

-- Create Locations Table
CREATE TABLE Locations (
    LocationID INT IDENTITY(1,1) PRIMARY KEY,
    City NVARCHAR(100) NOT NULL,
    Country NVARCHAR(100) NOT NULL
);

-- Create Employees Table
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Salary DECIMAL(10,2) NOT NULL,
    DepartmentID INT NOT NULL,
    LocationID INT NOT NULL,
    ManagerID INT NULL,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
    FOREIGN KEY (LocationID) REFERENCES Locations(LocationID),
    FOREIGN KEY (ManagerID) REFERENCES Employees(EmployeeID)
);

-- Insert Sample Data
INSERT INTO Departments (DepartmentName) VALUES 
('Human Resources'), ('Information Technology'), ('Finance'), ('Marketing');

INSERT INTO Locations (City, Country) VALUES 
('New York', 'USA'), ('London', 'UK'), ('Toronto', 'Canada'), ('Sydney', 'Australia');
```

### 4. Run the Application
```bash
cd EmployeeManagementSystem
dotnet restore
dotnet run
```

Visit `https://localhost:5001` or `http://localhost:5000` to access the application.

## 📁 Project Structure

```
EmployeeManagementSystem/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor      # Main application layout
│   │   └── NavMenu.razor         # Navigation menu component
│   └── Pages/
│       ├── Employees.razor       # Employee management page
│       ├── Departments.razor     # Department management page
│       ├── Locations.razor       # Location management page
│       └── Home.razor           # Landing page
├── Models/
│   ├── Employee.cs              # Employee data model
│   ├── Department.cs            # Department data model
│   └── Location.cs              # Location data model
├── Services/
│   ├── EmployeeService.cs       # Employee business logic
│   ├── DepartmentService.cs     # Department business logic
│   └── LocationService.cs      # Location business logic
├── wwwroot/                     # Static files (CSS, images)
├── appsettings.json            # Application configuration
└── Program.cs                  # Application entry point
```

## 🎯 Usage Guide

### Managing Employees
1. Navigate to the **Employees** page
2. Fill in employee details in the form
3. Select department and location from dropdowns
4. Optionally assign a manager
5. Click **Add Employee** to save

### Editing Employee Information
1. Click the **Edit** button next to any employee
2. Modify the required fields
3. Click **Save** to confirm changes or **Cancel** to discard

### Managing Departments & Locations
- Similar CRUD operations available for departments and locations
- Real-time updates reflect across all employee assignments

## 🔧 Configuration

### Database Connection
Modify the connection string in `appsettings.json` to match your SQL Server setup:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=EmployeeManagementDB;Trusted_Connection=True;Encrypt=False;"
  }
}
```

### Development vs Production
- Development settings in `appsettings.Development.json`
- Production configurations should be managed through environment variables or Azure Key Vault

## 🚀 Deployment

### Local IIS Deployment
1. Publish the application: `dotnet publish -c Release`
2. Copy published files to IIS wwwroot
3. Configure IIS application pool for .NET 8

### Azure App Service
1. Create Azure App Service instance
2. Configure connection strings in Azure portal
3. Deploy using Visual Studio publish profile or GitHub Actions

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Mamoona Latif**
- GitHub: [@Mamoonalatif](https://github.com/Mamoonalatif)
- LinkedIn: [Connect with me](https://linkedin.com/in/mamoona-latif)

## 🙏Acknowledgments

- ASP.NET Core Team for the excellent framework
- Blazor community for inspiration and best practices
- Bootstrap team for the responsive UI components

## 📞 Support

If you encounter any issues or have questions:
1. Check the [Issues](https://github.com/Mamoonalatif/EmployeePortal/issues) page
2. Create a new issue with detailed description
3. Contact the author through GitHub

---

