# CyberSecurity ChatBot

## Overview

**CyberSecurity ChatBot** is a C# Windows Forms GUI application designed to educate users about cybersecurity topics. It provides dynamic, personalized, and empathetic responses, helps users manage cybersecurity tasks, and includes interactive features to reinforce learning.

## 🎥 Video Presentation

A full walkthrough and demonstration of all features is available in the video below:

▶️ **Watch the CyberSecurity ChatBot Demo Video**  
> This video covers the GUI, Task Assistant, Quiz, NLP simulation, Activity Log, and explains the code and logic behind each feature.

## Features

- **Modern GUI:** All interactions take place in a user-friendly Windows Forms interface—no console required.
- **Keyword Recognition:** Understands and responds to cybersecurity-related keywords (e.g., password, phishing, scam, privacy, browsing).
- **Randomized Tips:** Offers varied, informative responses for each topic to keep conversations engaging.
- **Memory and Recall:** Remembers user interests and personalizes future responses.
- **Sentiment Detection:** Detects user sentiment (worried, curious, frustrated) and responds empathetically.
- **Conversation Flow:** Handles follow-up questions and continues the current topic for a natural chat experience.
- **Error Handling:** Provides helpful feedback for unknown or unclear inputs.
- **Personalized Responses:** Addresses the user by name for a more engaging experience.
- **Task Assistant with Reminders:** Add, view, complete, and delete cybersecurity-related tasks. Set reminders for important actions.
- **Cybersecurity Quiz Mini-Game:** Interactive quiz with 10+ questions on cybersecurity topics. Provides instant feedback, tracks your score, and gives personalized encouragement.
- **NLP Simulation:** Understands natural language commands (e.g., `remind me to update my password tomorrow`, `add a task to enable 2FA`, `activity`, `quiz`, etc.) for a more human-like experience.
- **Activity Log:** Tracks all major actions (tasks, reminders, quiz attempts, etc.). Type `activity`, `activity log`, or `what have you done` to see a summary of recent actions.
- **Modern User Experience:** Consistent color scheme, modern fonts, and button styles across all windows. Audio greeting plays on startup (if enabled).
- **Seamless Integration:** All features from Parts 1 and 2 are fully integrated and accessible from the main chat window.

## Setup Instructions

1. **Clone the repository:**

   ```bash
   git clone https://github.com/HlahaneKea/CyberSecurityChatBot.git
   ```

2. **Open the project:**

   - Open the solution file `CyberSecurityChatBot.sln` in Visual Studio  
   **or**  
   - Navigate to the project directory and run:

     ```bash
     dotnet run --project Chatbox/CyberSecurityChatBot/CyberSecurityChatBot.csproj
     ```

## Usage

- **Start the application:** The main chat window will appear.
- **Ask questions about cybersecurity topics**, such as:
  - `Tell me about password safety.`
  - `Do you have any phishing tips?`
  - `I'm interested in privacy.`
  - `I'm worried about online scams.`
- **Use natural language commands to access features:**
  - `Remind me to update my password tomorrow.`
  - `Add a task to enable two-factor authentication.`
  - `Quiz` or `I want to take the quiz.`
  - `Activity`, `activity log`, or `what have you done?`
- **Task Assistant:** Add, complete, or delete tasks and set reminders.
- **Quiz:** Test your cybersecurity knowledge with instant feedback and scoring.
- **Activity Log:** View a summary of your recent actions at any time.

## Example Conversation

```
User: I'm interested in privacy.
Chatbot: Great! I'll remember that you're interested in privacy. It's a crucial part of staying safe online.

User: Can you explain more?
Chatbot: Here's some more information about privacy:
Review your privacy settings on social media and limit the amount of personal information you share online.

User: Remind me to update my password tomorrow.
Chatbot: (Opens Task Assistant with the task and reminder pre-filled.)

User: Activity
Chatbot: Here's a summary of recent actions:
Task added: 'Update my password' (Reminder set for tomorrow).
Quiz started - 10 questions answered.
```

## Project Structure

- `Program.cs` – Entry point for the application.
- `MainChatForm.cs` – Main GUI chat window.
- `Library.cs` – Main chatbot logic and response handling.
- `NlpProcessor.cs` / `NlpResult.cs` – Natural language processing logic.
- `TaskAssistantForm.cs` / `TaskItem.cs` – Task Assistant and task data.
- `QuizForm.cs` / `QuizQuestion.cs` – Quiz game and question data.
- `ActivityLogger.cs` / `ActivityLogEntry.cs` – Activity log feature.
- `AudioFile/greeting.wav` – Optional greeting audio.
- `.github/workflows/` – Contains CI/CD workflow files.

## Versioning

- The latest release is **v3.0**
- See the [Releases](https://github.com/HlahaneKea/CyberSecurityChatBot/releases) page for version history and release notes.

## License

This project is for **educational purposes**.
