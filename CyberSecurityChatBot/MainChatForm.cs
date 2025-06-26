using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace CyberSecurityChatBot
{
    public partial class MainChatForm : Form
    {
        private RichTextBox chatDisplay;
        private TextBox userInput;
        private Button sendButton;
        private Library chatLibrary;
        private NlpProcessor nlpProcessor;
        private string userName = "User";

        public MainChatForm()
        {
            InitializeChatUI();

            // Play greeting audio
            AudioPlayer audioPlayer = new AudioPlayer();
            audioPlayer.PlayGreetingAudio("AudioFile/greeting.wav");

            chatLibrary = new Library();
            nlpProcessor = new NlpProcessor(chatLibrary);
            chatLibrary.SetUserName(userName);
            ShowGreeting();
        }

        private void InitializeChatUI()
        {
            this.Text = "Cybersecurity Awareness Chatbot";
            this.Width = 700;
            this.Height = 550;
            this.BackColor = Color.FromArgb(245, 248, 255);
            this.Font = new Font("Segoe UI", 10);

            // Chat display area
            chatDisplay = new RichTextBox
            {
                Location = new Point(15, 15),
                Width = 650,
                Height = 400,
                ReadOnly = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11)
            };
            this.Controls.Add(chatDisplay);

            // User input text box
            userInput = new TextBox
            {
                Location = new Point(15, 430),
                Width = 520,
                Height = 30,
                Font = new Font("Segoe UI", 11)
            };
            userInput.KeyDown += UserInput_KeyDown;
            this.Controls.Add(userInput);

            // Send button
            sendButton = new Button
            {
                Text = "Send",
                Location = new Point(545, 430),
                Width = 120,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            sendButton.Click += SendButton_Click;
            this.Controls.Add(sendButton);
            this.AcceptButton = sendButton;
        }

        private void ShowGreeting()
        {
            AppendMessage("Chatbot", "Welcome to the SecureCB - Cybersecurity Awareness Chatbot! I'm here to help. You can ask me about cybersecurity topics, open the 'task assistant', or start a 'quiz'.");
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            ProcessUserInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevents the 'ding' sound on enter
                ProcessUserInput();
            }
        }

        private void ProcessUserInput()
        {
            string input = userInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            AppendMessage(userName, input);
            userInput.Clear();

            // Use the NLP processor to understand and handle the command
            var nlpResult = nlpProcessor.Process(input);
            HandleNlpResult(nlpResult);
        }

        private void HandleNlpResult(NlpResult result)
        {
            switch (result.Intent)
            {
                case UserIntent.AddTask:
                    ActivityLogger.Log($"User initiated a task via NLP: '{result.TaskTitle}'.");
                    var taskForm = new TaskAssistantForm(result.TaskTitle, result.ReminderDate);
                    taskForm.ShowDialog();
                    AppendMessage("Chatbot", "Task Assistant closed. I've logged the action for you.");
                    break;
                case UserIntent.ShowQuiz:
                    ActivityLogger.Log("User initiated the quiz via NLP.");
                    var quizForm = new QuizForm();
                    quizForm.ShowDialog();
                    AppendMessage("Chatbot", "Quiz closed. I hope you enjoyed it!");
                    break;
                case UserIntent.ShowLog:
                    DisplayActivityLog();
                    break;
                case UserIntent.Farewell:
                    AppendMessage("Chatbot", result.Response);
                    // Add a small delay then close the form
                    var timer = new System.Windows.Forms.Timer { Interval = 2000 };
                    timer.Tick += (s, ev) => this.Close();
                    timer.Start();
                    break;
                case UserIntent.GeneralQuestion:
                case UserIntent.Unknown:
                default:
                    AppendMessage("Chatbot", result.Response);
                    break;
            }
        }

        private void DisplayActivityLog()
        {
            ActivityLogger.Log("User viewed the activity log.");
            var logs = ActivityLogger.GetRecentLogs();
            string response;
            if (logs.Count == 0)
            {
                response = "No recent activity to show.";
            }
            else
            {
                var logText = new System.Text.StringBuilder("Here's a summary of recent actions:\n");
                for (int i = 0; i < logs.Count; i++)
                {
                    logText.AppendLine($"{i + 1}. {logs[i].Description}");
                }
                response = logText.ToString();
            }
            AppendMessage("Chatbot", response);
        }

        private void HandleCommand(string command)
        {
            // This method is now obsolete and replaced by HandleNlpResult
        }

        private void AppendMessage(string sender, string message)
        {
            chatDisplay.SelectionStart = chatDisplay.TextLength;
            chatDisplay.SelectionLength = 0;

            chatDisplay.SelectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
            chatDisplay.SelectionColor = (sender == "Chatbot") ? Color.FromArgb(0, 120, 215) : Color.Black;
            chatDisplay.AppendText($"{sender}: ");

            chatDisplay.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
            chatDisplay.SelectionColor = Color.FromArgb(50, 50, 50);
            chatDisplay.AppendText(message + Environment.NewLine + Environment.NewLine);

            chatDisplay.ScrollToCaret();
        }
    }
}