using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
using WebApplication1.Model.Entities;

namespace WebApplication1.Repository
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all employees
        public IEnumerable<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT EmpID, Name, Salary, DeptID FROM Employees";
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmpID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Salary = reader.GetDecimal(2),
                            DeptID = reader.GetInt32(3)
                        });
                    }
                }
            }
            return employees;
        }

        // Get an employee by Id
        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT EmpID, Name, Salary, DeptID FROM Employees WHERE EmpID = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                EmpID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Salary = reader.GetDecimal(2),
                                DeptID = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return employee;
        }

        // Add a new employee
        public void AddEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Employees (EmpID, Name, Salary, DeptID) VALUES (@EmpID, @Name, @Salary, @DeptID)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpID", employee.EmpID);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@DeptID", employee.DeptID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Update an existing employee
        public void UpdateEmployee(int Id, Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Employees SET Name = @Name, Salary = @Salary, DeptID = @DeptID WHERE EmpID = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@DeptID", employee.DeptID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete an employee
        public void DeleteEmployee(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Employees WHERE EmpID = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
