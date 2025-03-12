using System;
using System.Collections.Generic;
using System.Text;

namespace Ex0401.Models
{
    public class Event
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public List<Participant> Participants { get; private set; } = new List<Participant>();

        public Event(string name, string location, DateTime date)
        {
            Name = name;
            Location = location;
            Date = date;
        }

        public string DisplayInfo()
        {
            string formattedParticipants;
            if (Participants.Count == 0)
            {
                formattedParticipants = "No participants registered for this event.";
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var participant in Participants)
                {
                    sb.AppendLine(participant.DisplayInfo());
                }
                formattedParticipants = sb.ToString();
            }
            return $"Event Name: {Name}\nEvent Location: {Location}\nEvent Date {Date.ToString("dd.MM.YYYY")}\nParticipants: {formattedParticipants}";
        }
    }
}
