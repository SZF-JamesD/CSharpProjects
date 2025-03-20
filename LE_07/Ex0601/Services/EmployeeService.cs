using Ex0601.Models;
using System.Collections.Generic;
using ValidationLib;

namespace Ex0601.Services
{
    internal class EmployeeService
    {
        private readonly List<Employee> employees = new List<Employee>();

        public string AddEmployee(string nameInput, string ageInput)
        {
            var nameValidation = ValidationUtil.IsValidFullName(nameInput);
            if (!nameValidation.IsValid)
            {
                return nameValidation.ErrorMessage;
            }

            if (!int.TryParse(ageInput, out int age) || age <= 0)
            {
                return "Please enter a valid age";
            }

            Employee employee = new Employee
            {
                Name = nameValidation.Value,
                Age = age
            };

            employees.Add(employee);
            return $"Welcome, {employee.Name}! Your age is {employee.Age} years.";
        }
    }
}
