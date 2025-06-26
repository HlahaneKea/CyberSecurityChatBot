using System;
using System.Text.RegularExpressions;

namespace CyberSecurityChatBot
{
    public class NlpProcessor
    {
        private Library _library;

        public NlpProcessor(Library library)
        {
            _library = library;
        }

        public NlpResult Process(string input)
        {
            string lowerInput = input.ToLower();
            var result = new NlpResult();

            // Intent: Open Task Assistant directly
            if (lowerInput.Contains("task assistant"))
            {
                result.Intent = UserIntent.AddTask;
                result.TaskTitle = "";
                result.ReminderDate = null;
                return result;
            }

            // Intent: Add Task/Reminder
            if (lowerInput.Contains("add a task") || lowerInput.Contains("new task") || lowerInput.Contains("remind me to") || lowerInput.Contains("set a reminder"))
            {
                result.Intent = UserIntent.AddTask;
                result.TaskTitle = ExtractTaskTitle(input);
                result.ReminderDate = ExtractReminderDate(lowerInput);
                return result;
            }

            // Intent: Show Quiz
            if (lowerInput.Contains("quiz") || lowerInput.Contains("game"))
            {
                result.Intent = UserIntent.ShowQuiz;
                return result;
            }

            // Intent: Show Log
            if (lowerInput.Contains("activity log") || lowerInput.Contains("what have you done") || lowerInput.Trim() == "activity")
            {
                result.Intent = UserIntent.ShowLog;
                return result;
            }

            // Intent: Farewell
            if (lowerInput.Contains("exit") || lowerInput.Contains("goodbye") || lowerInput.Contains("bye"))
            {
                result.Intent = UserIntent.Farewell;
                result.Response = "Goodbye! Stay safe online!";
                return result;
            }

            // Default to General Question
            result.Intent = UserIntent.GeneralQuestion;
            result.Response = _library.GetResponse(lowerInput);
            return result;
        }

        private string ExtractTaskTitle(string input)
        {
            // Simple extraction logic: find the action phrase and take the text that follows.
            // This can be improved with more advanced regex.
            var match = Regex.Match(input, @"(add a task to|new task|remind me to|set a reminder for)\s+(.*)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[2].Value.Trim();
            }
            // Fallback for simple phrases like "Add a task 2FA"
            match = Regex.Match(input, @"(add a task|new task)\s+(.*)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[2].Value.Trim();
            }
            return input; // Fallback to the whole input if extraction fails
        }

        private DateTime? ExtractReminderDate(string lowerInput)
        {
            if (lowerInput.Contains("tomorrow"))
            {
                return DateTime.Now.AddDays(1);
            }

            var match = Regex.Match(lowerInput, @"in (\d+) days");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int days))
            {
                return DateTime.Now.AddDays(days);
            }

            match = Regex.Match(lowerInput, @"in (\d+) hours");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int hours))
            {
                return DateTime.Now.AddHours(hours);
            }

            return null; // No specific date found
        }
    }
}