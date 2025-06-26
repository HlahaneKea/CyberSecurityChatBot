using System;

namespace CyberSecurityChatBot
{
    public enum UserIntent
    {
        Unknown,
        GeneralQuestion,
        AddTask,
        ShowQuiz,
        ShowLog,
        Greeting,
        Farewell
    }

    public class NlpResult
    {
        public UserIntent Intent { get; set; } = UserIntent.Unknown;
        public string TaskTitle { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string Response { get; set; }
    }
}