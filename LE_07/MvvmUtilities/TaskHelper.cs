using System;
using System.Threading.Tasks;

namespace MvvmUtilities
{
    public static class TaskHelper
    {
        public static async Task RunSafeAsync(Func<Task> taskFunc, Action<Exception> errorHandler = null)
        {
            try
            {
                await taskFunc();
            }
            catch (Exception ex)
            {
                errorHandler?.Invoke(ex);
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }   
}
