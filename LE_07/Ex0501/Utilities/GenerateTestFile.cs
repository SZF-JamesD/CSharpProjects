using System;
using System.IO;

namespace Ex0501.Utilities
{
    class GenerateTestFile
    {
        static void Main()
        {
            string filePath = "Assets/test.txt";
            Directory.CreateDirectory("Assets");

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.CreateNew))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 1; i <= 10000; i++)
                    {
                        writer.WriteLine($"This is line {i}");
                    }
                }
                Console.WriteLine("File created successfully: " + filePath);
            }
            catch (IOException)
            {
                Console.WriteLine("File already exists: " + filePath);
            }
        }
    }
}
