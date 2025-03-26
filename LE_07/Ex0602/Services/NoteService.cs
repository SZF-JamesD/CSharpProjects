using Ex0602.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Ex0602.Services
{
    internal class NoteService
    {
        private readonly List<Note> notes = new List<Note>();
        private readonly DBService DbService;
        public async Task AddNoteAsync(string content, int employee_id)
        {       
            await Task.Delay(2000);
            try
            {
                if (String.IsNullOrEmpty(content))
                {
                    Console.WriteLine("Note content cannot be empty");

                    MessageBox.Show("Error: Note content cannot be empty.",
                    "Note Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    return;
                }

                Note note = new Note();
                {
                    note.NoteID = (notes[-1].NoteID + 1);
                    note.Content = content;
                    note.DateCreated = DateTime.Now;
                    note.EmployeeID = employee_id;
                };

                notes.Add(note);
                await DbService.AddNoteAsync(content, employee_id);
                MessageBox.Show("New note added successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            catch (Exception ex)
            {   
                Console.WriteLine($"An error occured while adding the note: {ex.Message}");

                MessageBox.Show($"Error: An error occured while adding the note: {ex.Message}.",
                    "Note Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
        }
    }
}
