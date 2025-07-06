-- Employee Management System Database Setup Script
-- Run this script to create the database schema and sample data

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'EmployeeManagementDB')
BEGIN
    CREATE DATABASE EmployeeManagementDB;
END
GO

USE EmployeeManagementDB;
GO

-- Create Departments Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Departments' AND xtype='U')
BEGIN
    CREATE TABLE Departments (
        DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
        DepartmentName NVARCHAR(100) NOT NULL UNIQUE
    );
END
GO

-- Create Locations Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Locations' AND xtype='U')
BEGIN
    CREATE TABLE Locations (
        LocationID INT IDENTITY(1,1) PRIMARY KEY,
        City NVARCHAR(100) NOT NULL,
        Country NVARCHAR(100) NOT NULL
    );
END
GO

-- Create Employees Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
BEGIN
    CREATE TABLE Employees (
        EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Salary DECIMAL(10,2) NOT NULL CHECK (Salary > 0),
        DepartmentID INT NOT NULL,
        LocationID INT NOT NULL,
        ManagerID INT NULL,
        CreatedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
        FOREIGN KEY (LocationID) REFERENCES Locations(LocationID),
        FOREIGN KEY (ManagerID) REFERENCES Employees(EmployeeID)
    );
END
GO

-- Insert Sample Departments
IF NOT EXISTS (SELECT * FROM Departments)
BEGIN
    INSERT INTO Departments (DepartmentName) VALUES 
    ('Human Resources'),
    ('Information Technology'),
    ('Finance'),
    ('Marketing'),
    ('Operations'),
    ('Sales'),
    ('Customer Service'),
    ('Research & Development');
END
GO

-- Insert Sample Locations
IF NOT EXISTS (SELECT * FROM Locations)
BEGIN
    INSERT INTO Locations (City, Country) VALUES 
    ('New York', 'USA'),
    ('London', 'UK'),
    ('Toronto', 'Canada'),
    ('Sydney', 'Australia'),
    ('Dubai', 'UAE'),
    ('Singapore', 'Singapore'),
    ('Mumbai', 'India'),
    ('Berlin', 'Germany');
END
GO

-- Insert Sample Employees
IF NOT EXISTS (SELECT * FROM Employees)
BEGIN
    -- Senior Management (No Managers)
    INSERT INTO Employees (FirstName, LastName, Salary, DepartmentID, LocationID, ManagerID) VALUES
    ('John', 'Smith', 120000.00, 1, 1, NULL),      -- HR Director
    ('Sarah', 'Johnson', 135000.00, 2, 1, NULL),   -- IT Director
    ('Michael', 'Brown', 125000.00, 3, 2, NULL),   -- Finance Director
    ('Emily', 'Davis', 110000.00, 4, 3, NULL);     -- Marketing Director

    -- Mid-level Management
    INSERT INTO Employees (FirstName, LastName, Salary, DepartmentID, LocationID, ManagerID) VALUES
    ('David', 'Wilson', 85000.00, 2, 1, 2),        -- IT Manager (reports to Sarah)
    ('Lisa', 'Anderson', 80000.00, 1, 1, 1),       -- HR Manager (reports to John)
    ('Robert', 'Taylor', 90000.00, 3, 2, 3),       -- Finance Manager (reports to Michael)
    ('Jessica', 'Martinez', 75000.00, 4, 3, 4);    -- Marketing Manager (reports to Emily)

    -- Staff Members
    INSERT INTO Employees (FirstName, LastName, Salary, DepartmentID, LocationID, ManagerID) VALUES
    ('Mark', 'Thompson', 65000.00, 2, 1, 5),       -- Software Developer
    ('Amanda', 'Garcia', 60000.00, 2, 4, 5),       -- Software Developer
    ('James', 'Rodriguez', 55000.00, 1, 1, 6),     -- HR Specialist
    ('Maria', 'Lopez', 70000.00, 3, 2, 7),         -- Financial Analyst
    ('Christopher', 'Lee', 62000.00, 4, 3, 8),     -- Marketing Specialist
    ('Jennifer', 'White', 58000.00, 4, 5, 8),      -- Marketing Coordinator
    ('Daniel', 'Harris', 68000.00, 2, 6, 5);       -- Systems Administrator
END
GO

-- Create Indexes for Performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Employees_DepartmentID')
BEGIN
    CREATE INDEX IX_Employees_DepartmentID ON Employees(DepartmentID);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Employees_LocationID')
BEGIN
    CREATE INDEX IX_Employees_LocationID ON Employees(LocationID);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Employees_ManagerID')
BEGIN
    CREATE INDEX IX_Employees_ManagerID ON Employees(ManagerID);
END
GO

-- Create Views for Common Queries
IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_EmployeeDetails')
BEGIN
    EXEC('CREATE VIEW vw_EmployeeDetails AS
    SELECT 
        e.EmployeeID,
        e.FirstName + '' '' + e.LastName AS FullName,
        e.FirstName,
        e.LastName,
        e.Salary,
        d.DepartmentName,
        l.City + '', '' + l.Country AS Location,
        m.FirstName + '' '' + m.LastName AS ManagerName,
        e.CreatedDate
    FROM Employees e
    INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
    INNER JOIN Locations l ON e.LocationID = l.LocationID
    LEFT JOIN Employees m ON e.ManagerID = m.EmployeeID');
END
GO

PRINT 'Database setup completed successfully!';
PRINT 'Tables created: Departments, Locations, Employees';
PRINT 'Sample data inserted for testing';
PRINT 'Views created: vw_EmployeeDetails';
PRINT 'Indexes created for optimal performance';
GO
