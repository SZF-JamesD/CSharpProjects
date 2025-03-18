using System;
using System.IO;
using System.Threading.Tasks;

namespace Ex0501.Utilities
{
    class GenerateTestFile
    {
        public static async Task GenerateTestFileAsync(string fileName)
        {
            string assetDirectory = "../../Assets/";

            if (!Directory.Exists(assetDirectory))
            {
                Directory.CreateDirectory(assetDirectory);
            }

            string filePath = Path.Combine(assetDirectory, fileName);
            Console.WriteLine(filePath);

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 1; i <= 10000; i++)
                    {
                        await writer.WriteLineAsync($"This is line {i}");
                    }
                }
                Console.WriteLine("File created successfully: " + filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error creating file", ex);
            }
        }
    }
}
