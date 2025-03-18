using System.Windows.Forms;

namespace Ex0501.Utilities
{
    public static class FileSelector
    {
        public static string SelectFile(string filePath)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) 
            {
                openFileDialog.Title = "Select a text file";
        }
    }
}
