using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityChatBot
{
    public static class ActivityLogger
    {
        private static readonly List<ActivityLogEntry> _log = new List<ActivityLogEntry>();
        private const int MaxLogCount = 50; // To prevent memory issues

        public static void Log(string description)
        {
            if (_log.Count >= MaxLogCount)
            {
                _log.RemoveAt(0); // Remove the oldest entry if the log is full
            }
            _log.Add(new ActivityLogEntry(description));
        }

        public static List<ActivityLogEntry> GetRecentLogs(int count = 5)
        {
            // Return the last 'count' items in the correct order (oldest to newest)
            return _log.Skip(Math.Max(0, _log.Count - count)).ToList();
        }
    }
}