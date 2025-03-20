using System.ComponentModel;
using System.Windows.Media;


namespace Ex0601.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string name;
        private string age;
        private string message;
        private Brush messageColor;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value; OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value; OnPropertyChanged(nameof(Age));
                }
            }
        }

        public string Message
        {
            get => message;
            set
            {
                if (message != value)
                {
                    message = value; OnPropertyChanged(nameof(Message));
                }
            }
        }

        public Brush MessageColor
        {
            get => messageColor;
            set
            {
                if (messageColor != value)
                {
                    messageColor = value; OnPropertyChanged(nameof(MessageColor));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
