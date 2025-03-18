using Ex0501.Services;
using System;
using System.Threading.Tasks;

namespace Ex0501
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string filePath = "Assets/text.txt";

                FileReaderService fileReader = new FileReaderService();

                string content = await fileReader.ReadFileWithProgressAsync(filePath);

                Console.WriteLine("File reading completed. Here is the file content: ");
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured: " + ex.ToString());
            }
        }
    }
}
