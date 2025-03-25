using Ex0602.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex0602.Services
{
    internal class NoteService
    {
        private readonly List<Note> notes = new List<Note>();

        public async Task<string> AddNoteAsync(string nameInput, string ageInput)
        {
            return await Task.Run(async () =>
            {
                await Task.Delay(4000);

                try
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
                catch (Exception ex)
                {
                    return $"An error occured while adding the employee: {ex.Message}";
                }
            });
        }
    }
}
