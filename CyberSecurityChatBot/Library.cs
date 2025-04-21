using System;

namespace CyberSecurityChatBot
{
    public class Library
    {
        public void RespondToInput(string input)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (input.Contains("how are you"))
                Console.WriteLine(" I'm great, thanks for asking! Ready to protect your data!");
            else if (input.Contains("purpose") || input.Contains("what are you"))
                Console.WriteLine(" I'm a chatbot designed to teach and answer questions about cybersecurity.");
            else if (input.Contains("password"))
                Console.WriteLine(" Always use long, complex passwords and avoid reusing the same one on multiple sites.");
            else if (input.Contains("phishing"))
                Console.WriteLine(" Phishing is when scammers try to trick you into giving away personal info. Don't click on suspicious links!");
            else if (input.Contains("browsing") || input.Contains("safe browsing"))
                Console.WriteLine(" Use HTTPS websites, keep your browser updated, and avoid downloading from untrusted sources.");
            else if (input.Contains("what can i ask") || input.Contains("help"))
                Console.WriteLine(" You can ask me about passwords, phishing, browsing safely, or general cybersecurity tips!");
            else
                Console.WriteLine(" I’m not sure how to answer that. Try asking about phishing, passwords, or safe browsing.");

            Console.ResetColor();
        }
    }
}
