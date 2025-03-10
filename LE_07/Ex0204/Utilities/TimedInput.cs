using System;
using System.Threading;

namespace Ex0204.Utilities
{
    public class TimedInput
    {
        public static string ReadInputWithTimeout(int seconds)
        {
            string input = string.Empty;
            var thread = new Thread(() =>
            {
                input = Console.ReadLine();
            });

            thread.Start();
            if (!thread.Join(TimeSpan.FromSeconds(seconds)))
            {
                thread.Abort();
                Console.WriteLine("Time's up!");
            }
            return input;
        }
    }
}