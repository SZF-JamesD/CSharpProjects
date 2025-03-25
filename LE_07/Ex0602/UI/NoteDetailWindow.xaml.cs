using System.Windows;
using Ex0602.Models;

namespace Ex0602.UI
{
    public partial class NoteDetailWindow : Window
    {
        public NoteDetailWindow(Note note)
        {
            InitializeComponent();
            DataContext = note;
        }
    }
}
