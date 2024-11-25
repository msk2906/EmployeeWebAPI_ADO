1. SqlServer Database Employees Table

CREATE TABLE Employees (
    EmpID INT PRIMARY KEY,          -- EmpID is the primary key
    Name NVARCHAR(100) NOT NULL,    -- Name is a required string field, you can adjust length as needed
    Salary DECIMAL(18, 2) NOT NULL, -- Salary is a decimal type (with 18 digits in total and 2 after the decimal point)
    DeptID INT NOT NULL             -- DeptID is an integer and is required
);

2. Change Connection String in appsettings.json
