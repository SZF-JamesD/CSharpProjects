using Ex0401.Services;
using System;
using System.Collections.Generic;

namespace Ex0401.UI
{
    internal class Menu
    {
        private EventManager eventManager;
        private EmailNotifier emailNotifier;
        private Dictionary<string, Action> menuActions;
        private InputHandler inputHandler;
        private bool exit = false;

        public Menu()
        {
            eventManager = new EventManager();
            emailNotifier = new EmailNotifier();

            eventManager.ParticipantAdded += (sender, participant) =>
            {
                emailNotifier.Notify($"New particpant registered : {participant.FullName}, {participant.Email}");
            };

            eventManager.EventAdded += (sender, newEvent) =>
            {
                emailNotifier.Notify($"New event added: {newEvent.Name} on {newEvent.Date:dd.MM.yyyy} at {newEvent.Location}");
            };

            inputHandler = new InputHandler(eventManager);

            menuActions = new Dictionary<string, Action>
            {
                { "1", inputHandler.HandleAddParticipant },
                { "2", eventManager.DisplayParticipants },
                { "3", eventManager.DisplayParticipant },
                { "4", inputHandler.HandleAddEvent },
                { "5", eventManager.DisplayEvents },
                { "6", eventManager.DisplayEvent },
                { "7", Exit }
            };
        }

        public void DisplayMenu()
        {
            while (!exit)
            {
                try
                {
                    Console.WriteLine("\n-- Event Management System --");
                    Console.WriteLine("1. Add Participant\n2. Display All Participants\n3. Display a Participant\n4. Add Event\n5. Display All Events\n6. Display an Event\n7. Exit");
                    Console.Write("Please select an option: ");

                    string choice = Console.ReadLine()?.Trim();
                    if (menuActions.TryGetValue(choice, out Action action))
                    {
                        action.Invoke();
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

        private void Exit()
        {
            Console.WriteLine("Exiting program.");
            exit = true;
        }
    }
}
