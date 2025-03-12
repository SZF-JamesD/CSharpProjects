using Ex0401.Interfaces;
using System;

namespace Ex0401.Services
{
    internal class EmailNotifier : IEventNotifier
    {
        public void Notify(string message)
        {
            Console.WriteLine($"Email notification sent: {message}");
        }
    }
}
