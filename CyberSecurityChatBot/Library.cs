﻿using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{
    public class Library
    {
        private readonly Dictionary<string, List<string>> keywordResponses;
        private readonly Random random;
        private delegate void ResponseHandler(string keyword, string namePrefix);
        private readonly Dictionary<string, ResponseHandler> handlers;
        private string userName;
        private string favoriteTopic;
        private string lastTopic;

        public Library()
        {
            random = new Random();
            keywordResponses = new Dictionary<string, List<string>>
            {
                { "password", new List<string> { "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.",
                                                  "Always use long, complex passwords and avoid reusing the same one on multiple sites.",
                                                  "Consider using a password manager to keep your passwords safe and unique." } },
                { "phishing", new List<string> { "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                                                  "Never click on suspicious links or download attachments from unknown sources.",
                                                  "Check the sender's email address carefully—phishing emails often use addresses that look similar to legitimate ones." } },
                { "privacy", new List<string> { "Review your privacy settings on social media and limit the amount of personal information you share online.",
                                                "Be mindful of the permissions you grant to apps and websites.",
                                                "Use encrypted messaging apps to protect your conversations." } },
                { "scam", new List<string> { "If something sounds too good to be true, it probably is. Be wary of online scams.",
                                            "Never share your financial information with unverified sources.",
                                            "Report suspicious messages or websites to the relevant authorities." } },
                { "browsing", new List<string> { "Use HTTPS websites to ensure your connection is secure.",
                                                "Keep your browser updated to protect against vulnerabilities.",
                                                "Avoid downloading files from untrusted sources." } },
                { "safe browsing", new List<string> { "Enable pop-up blockers and avoid clicking on suspicious ads.",
                                                      "Log out of accounts when using public computers.",
                                                      "Regularly clear your browser cache and cookies for better privacy." } }
            };

            handlers = new Dictionary<string, ResponseHandler>
            {
                { "password", RespondWithRandomTip },
                { "phishing", RespondWithRandomTip },
                { "privacy", RespondWithRandomTip },
                { "scam", RespondWithRandomTip },
                { "browsing", RespondWithRandomTip },
                { "safe browsing", RespondWithRandomTip }
            };
        }
        public void SetUserName(string name)
        {
            userName = name;
        }

        public string GetResponse(string input)
        {
            // This method replaces RespondToInput for GUI use
            bool found = false;
            string namePrefix = string.IsNullOrWhiteSpace(userName) ? "" : userName + ", ";
            string response = "";

            // Memory: Check if user expresses interest in a topic
            foreach (var topic in keywordResponses.Keys)
            {
                if (input.Contains($"interested in {topic}") || input.Contains($"like {topic}") || input.Contains($"love {topic}"))
                {
                    favoriteTopic = topic;
                    response = $"Great! {namePrefix}I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.";
                    lastTopic = topic;
                    found = true;
                    break;
                }
            }

            // Sentiment detection
            if (!found)
            {
                string sentiment = null;
                if (input.Contains("worried") || input.Contains("anxious") || input.Contains("scared"))
                    sentiment = "worried";
                else if (input.Contains("curious") || input.Contains("interested"))
                    sentiment = "curious";
                else if (input.Contains("frustrated") || input.Contains("confused") || input.Contains("unsure"))
                    sentiment = "frustrated";

                if (sentiment != null)
                {
                    switch (sentiment)
                    {
                        case "worried":
                            response = $"It's completely understandable to feel that way. Cybersecurity can be overwhelming, but I'm here to help. Let me share some tips to help you stay safe.";
                            break;
                        case "curious":
                            response = $"Curiosity is great! Learning about cybersecurity is a smart move. Here's some information that might interest you.";
                            break;
                        case "frustrated":
                            response = $"I understand it can be frustrating. If you need more details or clarification, just ask!";
                            break;
                    }
                    // Try to find a topic in the input to continue with tips
                    foreach (var keyword in handlers.Keys)
                    {
                        if (input.Contains(keyword))
                        {
                            response += " " + GetRandomTip(keyword);
                            lastTopic = keyword;
                            found = true;
                            break;
                        }
                    }
                    if (!found) found = true; // Mark as handled even if no keyword found
                }
            }

            // Main keyword/topic response
            if (!found)
            {
                foreach (var keyword in handlers.Keys)
                {
                    if (input.Contains(keyword))
                    {
                        response = GetRandomTip(keyword);
                        lastTopic = keyword;
                        found = true;
                        break;
                    }
                }
            }

            // Follow-up/conversation flow
            if (!found)
            {
                if ((input.Contains("more") || input.Contains("details") || input.Contains("explain") || input.Contains("confused")) && lastTopic != null)
                {
                    response = $"Here's some more information about {lastTopic}: {GetRandomTip(lastTopic)}";
                    found = true;
                }
            }
            // Help and fallback
            if (!found)
            {
                if (input.Contains("what can i ask") || input.Contains("help"))
                    response = $"You can ask me about passwords, phishing, scams, privacy, browsing safely, or general cybersecurity tips!";
                else if (favoriteTopic != null && input.Contains(favoriteTopic))
                    response = $"As someone interested in {favoriteTopic}, let me know if you want more tips or details about it!";
                else if (string.IsNullOrWhiteSpace(input) || input.Length < 2)
                    response = $"I'm not sure I understand. Can you try rephrasing?";
                else
                    response = $"I'm still in development and may not understand your request if it's not about cybersecurity. Please ask about passwords, phishing, scams, privacy, or browsing. If I didn't understand, can you try rephrasing?";
            }
            return response;
        }

        public void RespondToInput(string input)
        {
            // This method is now legacy, kept for potential console debugging.
            // New logic is in GetResponse(string).
            Console.ForegroundColor = ConsoleColor.Yellow;
            string response = GetResponse(input);
            Console.WriteLine(response);
            Console.ResetColor();
        }


        private void RespondWithRandomTip(string keyword, string namePrefix)
        {
            Console.WriteLine(GetRandomTip(keyword));
        }

        private string GetRandomTip(string keyword)
        {
            var responses = keywordResponses[keyword];
            int idx = random.Next(responses.Count);
            return responses[idx];
        }
    }
}

