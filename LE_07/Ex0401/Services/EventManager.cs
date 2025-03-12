using Ex0401.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex0401.Services
{
    public delegate void ParticipantAddedHandler(object sender, Participant participant);
    public delegate void EventAddedHandler(object sender, Event newEvent);

    public class EventManager
    {
        public event ParticipantAddedHandler ParticipantAdded;
        public event EventAddedHandler EventAdded;


        public List<Event> Events { get; private set; } = new List<Event>();

        public void AddParticipant(Event eventName, Participant participant)
        {
            eventName.Participants.Add(participant);
            ParticipantAdded?.Invoke(this, participant);
        }

        public void AddEvent(Event newEvent) 
        { 
            Events.Add(newEvent); 
            EventAdded?.Invoke(this, newEvent);
        }

        public void DisplayEvents()
        {
            if (Events.Count == 0)
            {
                Console.WriteLine("No events found.");
                return;
            }

            foreach (var eventObj in Events)
            {
                Console.WriteLine(eventObj.DisplayInfo());
            }
        }

        public void DisplayEvent()
        {
            while (true)
            {
                Console.Write("Enter event name or 0 to exit: ");
                string eventName = Console.ReadLine();
                if (eventName == "0")
                {
                    break;
                }
                Event eventFound = Events.FirstOrDefault(e => e.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase));
                if (Events.Count == 0 || eventFound == null)
                {
                    Console.WriteLine("Event not Found.");
                }
                else
                {
                    Console.WriteLine(eventFound.DisplayInfo());
                    break;
                }
            }
        }

        public void DisplayParticipants()
        {
            if (Events.Count == 0)
            {
                Console.WriteLine("No events registered.");
                return;
            }

            foreach (var ev in Events)
            {
                Console.WriteLine(ev.Name);
                if (ev.Participants.Count == 0)
                {
                    Console.WriteLine($"No participants registered for this event");
                }
                else
                {
                    foreach (var participant in ev.Participants)
                    {
                        Console.WriteLine(participant.DisplayInfo());
                    }
                }
            }  
        }

        public void DisplayParticipant()
        {
            while (true)
            {
                Console.Write("Enter participant name or 0 to exit: ");
                string partName = Console.ReadLine();
                if (partName == "0")
                {
                    break;
                }
                bool found = false;
                foreach (var ev in Events)
                {
                    var participant = ev.Participants?.FirstOrDefault(p => p.FullName.Equals(partName, StringComparison.OrdinalIgnoreCase));
                    if (participant != null)
                    {
                        Console.WriteLine($"Participant found: {participant.DisplayInfo()}");
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Participant not found.");
                }
            }
        }
    }
}
