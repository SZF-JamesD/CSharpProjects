using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ex0602.Models;
using Ex0602.Services;

namespace Ex0602.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly NoteService _noteService;

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        private string _addNote;
        public string AddNote
        {
            get => _addNote;
            set
            {
                _addNote = value;
                OnPropertyChanged(nameof(AddNote));
                AddNoteCommandCanExecuteChanged();
            }
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
                DeleteNoteCommandCanExecuteChanged();
            }
        }

        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }

        public MainViewModel(NoteService noteService)
        {
            _noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));

            AddNoteCommand = new RelayCommand(async() => await AddNoteAsync(), () => !string.IsNullOrWhiteSpace(AddNote));
            DeleteNoteCommand = new RelayCommand(async () => await DeleteNoteAsync(), () => SelectedNote != null);

            Task.Run(async () => await LoadNotesAsync());
        }

        private async Task AddNoteAsync()
        {
            try
            {
                var app = (App)Application.Current;
                var employeeId = app.LoggedEmployeeId;
                await _noteService.AddNoteAsync(AddNote, employeeId);
                AddNote = string.Empty;
                await LoadNotesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding note: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async Task DeleteNoteAsync()
        {
            if (SelectedNote != null)
            {
                try
                {
                    await _noteService.DeleteNoteAsync(SelectedNote.NoteID);
                    await LoadNotesAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting note: {ex.Message}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private async Task LoadNotesAsync()
        {
            var notesList = await _noteService.GetNotesAsync();
            Application.Current.Dispatcher.Invoke(() =>
            {
                Notes.Clear();
                foreach (var note in notesList)
                {
                    Notes.Add(note);
                }
            });
        }

        private void AddNoteCommandCanExecuteChanged()
        {
            if (AddNoteCommand is RelayCommand cmd)
            {
                cmd.RaiseCanExecuteChanged();
            }
        }

        private void DeleteNoteCommandCanExecuteChanged()
        {
            if (DeleteNoteCommand is RelayCommand cmd)
            {
                cmd.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
