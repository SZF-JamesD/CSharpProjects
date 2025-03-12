using Ex0401.Models;
using System;
using System.Linq;
using ValidationLib;

namespace Ex0401.Services
{
    public class InputHandler
    {
        private EventManager eventManager;

        public InputHandler(EventManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public void HandleAddParticipant()
        {
            Event eventName = CheckEventName();
            if (eventName == null)
            {
                return;
            }
            string fullName = GetValidatedName();
            string emailAddress = GetValidatedEmail();
            var participant = new Participant(fullName, emailAddress);
            eventManager.AddParticipant(eventName, participant);
        }

        public void HandleAddEvent()
        {
            string name = GetEventName();
            string location = GetEventLocation();
            DateTime date = GetEventDate();
            var newEvent = new Event(name, location, date);
            eventManager.AddEvent(newEvent);
        }

        public Event CheckEventName()
        {
            while (true)
            {
                Console.Write("To which event would you like to add a participant?: ");
                string eventNameInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(eventNameInput))
                {
                    Console.WriteLine("Error: Event name cannot be left blank, please try again.");
                    continue;
                }
                Event targetEvent = eventManager.Events.FirstOrDefault(e => e.Name.Equals(eventNameInput, StringComparison.OrdinalIgnoreCase));
                if (targetEvent == null)
                {
                    Console.WriteLine("Event not found.");
                    return null;
                }
                else
                {
                    return targetEvent;
                }
            }
        }

        public string GetValidatedName()
        { 
            while (true)
            {
                Console.WriteLine("\n-- Add New Participant --");

                Console.Write("Enter full participant name: ");
                string fullNameInput = Console.ReadLine()?.Trim();
                var fullNameValidation = ValidationUtil.IsValidFullName(fullNameInput);
                if (fullNameValidation.IsValid)
                {
                    return fullNameValidation.Value;
                }
                else
                {
                    Console.WriteLine("Error: " + fullNameValidation.ErrorMessage);
                }
            }
        }

        public string GetValidatedEmail()
        {
            while (true)
            {
                Console.Write("Enter email address: ");
                string emailInput = Console.ReadLine()?.Trim();
                var emailValidation = ValidationUtil.IsValidEmail(emailInput);
                if (emailValidation.IsValid)
                {
                    return emailValidation.Value;
                }
                else
                {
                    Console.WriteLine("Error: " + emailValidation.ErrorMessage);
                }
            }
        }

        public string GetEventName()
        {
            while (true)
            {
                Console.Write("Enter event name: ");
                string eventNameInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(eventNameInput))
                {
                    Console.WriteLine("Error: Event name cannot be left blank, please try again.");
                    continue;
                }
                return eventNameInput;
            }
        }

        public string GetEventLocation()
        {
            while (true)
            {
                Console.Write("Enter event address(Street Nr. PostCode City): ");
                string eventLocationInput = Console.ReadLine()?.Trim();
                var eventLocationValidation = ValidationUtil.IsValidAddress(eventLocationInput);
                if (eventLocationValidation.IsValid)
                {
                    string address = $"{eventLocationValidation.Value.Street} {eventLocationValidation.Value.Number}" + (string.IsNullOrWhiteSpace(eventLocationValidation.Value.Apartment) ? "" : $" {eventLocationValidation.Value.Apartment}") + $" {eventLocationValidation.Value.PostalCode} {eventLocationValidation.Value.City}";
                    return address;
                }
                else
                {
                    Console.WriteLine("Error: " + eventLocationValidation.ErrorMessage);
                }
            }
        }

        public DateTime GetEventDate()
        {
            while (true)
            {
                Console.Write("Enter event date: ");
                string eventDateInput = Console.ReadLine()?.Trim();
                var eventDateValidation = ValidationUtil.ValidateAndFormatDate(eventDateInput);
                if (eventDateValidation.IsValid)
                {
                    DateTime eventDate = DateTime.Parse(eventDateValidation.Value);
                    return eventDate;
                }
                else
                {
                    Console.WriteLine("Error: " + eventDateValidation.ErrorMessage);
                }
            }
        }
    }
}
