using System.Windows;
using System.Windows.Input;
using Ex0602.Models;
using Ex0602.Services;
using Ex0602.ViewModels;

namespace Ex0602.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            DataContext = new MainViewModel(app.NoteService);
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
