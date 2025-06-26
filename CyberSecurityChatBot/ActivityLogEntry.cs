using System;

namespace CyberSecurityChatBot
{
    public class ActivityLogEntry
    {
        public DateTime Timestamp { get; private set; }
        public string Description { get; private set; }

        public ActivityLogEntry(string description)
        {
            Timestamp = DateTime.Now;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Timestamp:g}: {Description}";
        }
    }
}