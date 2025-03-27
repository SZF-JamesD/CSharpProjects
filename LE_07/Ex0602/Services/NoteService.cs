using Ex0602.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Ex0602.Services
{
    public class NoteService
    {
        private readonly List<Note> notes = new List<Note>();
        private readonly DBService _dbService;

        public NoteService(DBService dBService)
        {
            _dbService = dBService ?? throw new ArgumentNullException(nameof(dBService));
        }

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

                int newNoteId = (notes.Count > 0) ? (notes[notes.Count - 1].NoteID + 1) : 1;

                Note note = new Note
                {
                    NoteID = newNoteId,
                    Content = content,
                    DateCreated = DateTime.Now,
                    EmployeeID = employee_id
                };

                notes.Add(note);

                await _dbService.AddNoteAsync(content, employee_id);

                MessageBox.Show("New note added successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                
            }
            catch (Exception ex)
            {   
                Console.WriteLine($"An error occured while adding the note: {ex.Message}");

                MessageBox.Show($"Error: An error occured while adding the note: {ex.Message}.",
                    "Note Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            return await _dbService.GetAllNotesAsync();
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            try
            {
                var noteToRemove = notes.Find(n => n.NoteID == noteId);
                if (noteToRemove != null)
                {
                    notes.Remove(noteToRemove);
                }

                await _dbService.DeleteNoteAsync(noteId);

                MessageBox.Show("Note deleted successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: An error occured while deleting the note: {ex.Message}",
                    "Note error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
