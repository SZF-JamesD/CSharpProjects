using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ex0501.Services;
using Ex0501.Utilities;

namespace Ex0501.UI
{
    internal class Menu
    {
        private FileReaderService _fileReaderService;
        private Dictionary<string, Func<Task>> menuActions;
        private bool exit = false;

        public Menu()
        {
            _fileReaderService = new FileReaderService();

            menuActions = new Dictionary<string, Func<Task>>
            {
                { "1", ReadFileAsync },
                { "2", CreateNewFileAsync },
                { "3", ExitAsync}
            };
        }

        public async Task ShowMenuAsync()
        {
            while (!exit) 
            {
                try
                {
                    Console.WriteLine("Select an option/\n1. Read a file\n2. Create a new file\n3. Exit");
                    Console.Write("Please select an option: ");

                    string choice = Console.ReadLine()?.Trim();
                    if (menuActions.TryGetValue(choice, out Func<Task> action))
                    {
                        await action();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        private Task ExitAsync()
        {
            Console.WriteLine("Exiting program.");
            exit = true;
            return Task.CompletedTask;
        }

        private async Task ReadFileAsync()
        {
            Console.Write("Enter the file name:");
            string fileName = Console.ReadLine()?.Trim();

            try
            {
                Console.WriteLine("Starting file read...");
                string content = await _fileReaderService.ReadFileWithProgressAsync(fileName);
                Console.WriteLine("\nFile reading completed.");

                Console.WriteLine("Loading complete. Press any key to display the file content...");
                Console.ReadKey();
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred: " + ex.Message);
            }
        }

        private async Task CreateNewFileAsync()
        {
            Console.Write("Enter the file name to create (e.g., test.txt): ");
            string fileName = Console.ReadLine()?.Trim();

            try
            {
                await GenerateTestFile.GenerateTestFileAsync(fileName);
                Console.WriteLine("File created successfully in the Assets folder: " + fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred while creating the file: " + ex.Message);
            }
        }
    }
}
