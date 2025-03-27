using System.Windows;
using Ex0602.Models;
using Ex0602.ViewModels;

namespace Ex0602.View
{
    public partial class NoteDetailWindow : Window
    {
        public NoteDetailWindow(Note note)
        {
            InitializeComponent();
            DataContext = new NoteDetailViewModel(note);
        }
    }
}
