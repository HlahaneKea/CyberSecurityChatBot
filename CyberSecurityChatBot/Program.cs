using System;

namespace CyberSecurityChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            AudioPlayer audioPlayer = new AudioPlayer();
            audioPlayer.PlayGreetingAudio("AudioFile/greeting.wav");

            Greeting greeting = new Greeting();
            Library library = new Library();
            MenuDisplay menu = new MenuDisplay();

            greeting.ShowAsciiArt();
            string name = greeting.GetUserName();
            library.SetUserName(name);
            greeting.WelcomeUser(name);

            bool keepChatting = true;
            while (keepChatting)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nAsk me something about cybersecurity or type 'exit': ");
                Console.ResetColor();
                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine(" I didn’t quite understand that. Could you rephrase?");
                    continue;
                }

                if (input == "exit")
                {
                    Console.WriteLine(" Goodbye! Stay safe online!");
                    keepChatting = false;
                }
                else
                {
                    library.RespondToInput(input);
                }
            }
        }
    }
}