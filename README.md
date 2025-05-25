# CyberSecurity ChatBot

## Overview
CyberSecurity ChatBot is a C# console application designed to educate users about cybersecurity topics. It provides dynamic, personalized, and empathetic responses to help users stay safe online.

## Features
- **Keyword Recognition:** Understands and responds to cybersecurity-related keywords (e.g., password, phishing, scam, privacy, browsing).
- **Randomized Tips:** Offers varied, informative responses for each topic to keep conversations engaging.
- **Memory and Recall:** Remembers user interests and personalizes future responses.
- **Sentiment Detection:** Detects user sentiment (worried, curious, frustrated) and responds empathetically.
- **Conversation Flow:** Handles follow-up questions and continues the current topic for a natural chat experience.
- **Error Handling:** Provides helpful feedback for unknown or unclear inputs.
- **Personalized Responses:** Addresses the user by name for a more engaging experience.
- **Code Optimization:** Uses dictionaries and lists for efficient response management.

## Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone https://github.com/HlahaneKea/CyberSecurityChatBot.git
   ```
2. **Open the project:**
   - Open the solution file `CyberSecurityChatBot.sln` in Visual Studio **or**
   - Navigate to the project directory and run:
     ```bash
     dotnet run --project Chatbox/CyberSecurityChatBot/CyberSecurityChatBot.csproj
     ```

## Usage

- When prompted, enter your name to personalize the chat.
- Ask questions about cybersecurity topics, such as:
  - `Tell me about password safety.`
  - `Do you have any phishing tips?`
  - `I'm interested in privacy.`
  - `I'm worried about online scams.`
  - `Can you explain more?`
- The chatbot will remember your interests and adjust its responses accordingly.

## Example Conversation

User: I'm interested in privacy.
Chatbot: Great! I'll remember that you're interested in privacy. It's a crucial part of staying safe online.
User: Can you explain more?
Chatbot: Here's some more information about privacy:
Review your privacy settings on social media and limit the amount of personal information you share online.

## Project Structure

- `Program.cs` - Entry point for the application.
- `Library.cs` - Main chatbot logic and response handling.
- `Greeting.cs` - Handles greetings and user name input.
- `ChatBotData.cs` - Stores available topics.
- `.github/workflows/` - Contains CI/CD workflow files.

## Versioning

- The latest release is [v2.0](https://github.com/HlahaneKea/CyberSecurityChatBot/releases/tag/v2.0).
- See the [Releases](https://github.com/HlahaneKea/CyberSecurityChatBot/releases) page for version history and release notes.

## License

This project is for educational purposes.
