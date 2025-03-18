using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex0501.Services
{
    public class FileReaderService
    {
        public async Task<string> ReadFileWithProgressAsync(string fileName)
        {
            StringBuilder fileContent = new StringBuilder();
            CancellationTokenSource cts = new CancellationTokenSource();
            string filePath = "../../Assets/" + fileName;

            Task progressTask = ShowProgressAsync(cts.Token);

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int lineCount = 0;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        fileContent.AppendLine(line);
                        lineCount++;
                        if (lineCount % 100 == 0)
                        {
                            await Task.Delay(1);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cts.Cancel();
                try
                {
                    await progressTask;
                }
                catch (OperationCanceledException)
                {
                    
                }
            }

            return fileContent.ToString();
        }

        private async Task ShowProgressAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("File is loading...");
                await Task.Delay(500, token);
            }
        }
    }
}
