using System;
using System.Threading;

namespace CyberSecurityChatBot
{
    public class Greeting
    {
        public void ShowAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
   _____                            _____   ____  
  / ____|                          / ____| / __ \ 
 | (___   ___  ___ _   _ _ __ __  | |     | |__) ) 
  \___ \ / _ \/ __| | | | '__/ _ \| |     |  __ |
  ____) |  __/ (__| |_| | | |  __/| |____ | |__) )
 |_____/ \___|\___|\__,_|_|  \___| \_____| \____/ 
                                                  
    ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Welcome to the SecureCB - Cybersecurity Awareness Chatbot\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("  Helping you stay safe online, one answer at a time.\n");
            Console.ResetColor();
        }

        public string GetUserName()
        {
            Console.Write("\n Please enter your name: ");
            string name = Console.ReadLine();
            return string.IsNullOrWhiteSpace(name) ? "User" : name;
        }

        public void WelcomeUser(string name)
        {
            Console.WriteLine($"\n Welcome, {name}! I'm here to help you with cybersecurity questions.");
            Thread.Sleep(1000);
        }
    }
}
