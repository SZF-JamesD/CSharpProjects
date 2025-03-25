using System.Windows;
using System.Windows.Input;
using Ex0602.Models;
using Ex0602.UI;

namespace Ex0602.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NotesListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (NotesListView.SelectedItem is Note selectedNote)
            {
                NoteDetailWindow detailWindow = new NoteDetailWindow(selectedNote);
                detailWindow.Show();
            }
        }
    }
}
