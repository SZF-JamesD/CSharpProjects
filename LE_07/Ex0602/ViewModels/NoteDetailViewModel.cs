using Ex0602.Models;
using System;
using System.ComponentModel;

namespace Ex0602.ViewModels
{
    public class NoteDetailViewModel : INotifyPropertyChanged
    {
        private Note _note;
        
        public NoteDetailViewModel(Note note)
        {
            _note = note;
        }

        public string Title => $"Note #{_note.NoteID} - {_note.DateCreated.ToShortDateString()}";

        public string Content
        {
            get => _note.Content;
            set
            {
                if (_note.Content != value)
                {
                    _note.Content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }

        public DateTime DateCreated => _note.DateCreated;
        public int EmployeeID => _note.EmployeeID; 

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
