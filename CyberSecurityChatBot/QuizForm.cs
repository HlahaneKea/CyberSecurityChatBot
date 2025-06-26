using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CyberSecurityChatBot
{
    public partial class QuizForm : Form
    {
        private List<QuizQuestion> questions;
        private int currentQuestion = 0;
        private int score = 0;

        public QuizForm()
        {
            InitializeQuizQuestions();
            InitializeQuizUI();
            DisplayQuestion();
            ActivityLogger.Log("Quiz started.");
        }

        private void InitializeQuizQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion { Question = "What should you do if you receive an email asking for your password?", Options = new List<string>{"Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it"}, CorrectOptionIndex = 2, Explanation = "Correct! Reporting phishing emails helps prevent scams.", IsTrueFalse = false },
                new QuizQuestion { Question = "True or False: You should use the same password for all your accounts.", Options = new List<string>{"True", "False"}, CorrectOptionIndex = 1, Explanation = "Correct! Always use unique passwords for each account.", IsTrueFalse = true },
                new QuizQuestion { Question = "Which of the following is a sign of a phishing attempt?", Options = new List<string>{"Unexpected attachments", "Personalized greeting", "Proper grammar", "Known sender"}, CorrectOptionIndex = 0, Explanation = "Correct! Unexpected attachments are a common sign of phishing.", IsTrueFalse = false },
                new QuizQuestion { Question = "What is two-factor authentication?", Options = new List<string>{"A type of malware", "A security process requiring two forms of verification", "A password manager", "A firewall"}, CorrectOptionIndex = 1, Explanation = "Correct! Two-factor authentication adds an extra layer of security.", IsTrueFalse = false },
                new QuizQuestion { Question = "True or False: You should click on links from unknown sources.", Options = new List<string>{"True", "False"}, CorrectOptionIndex = 1, Explanation = "Correct! Never click on suspicious links.", IsTrueFalse = true },
                new QuizQuestion { Question = "Which is the safest way to browse online?", Options = new List<string>{"Use public Wi-Fi without VPN", "Use HTTPS websites", "Share passwords with friends", "Ignore software updates"}, CorrectOptionIndex = 1, Explanation = "Correct! HTTPS websites are more secure.", IsTrueFalse = false },
                new QuizQuestion { Question = "What should you do if you suspect your account is compromised?", Options = new List<string>{"Ignore it", "Change your password immediately", "Tell no one", "Delete your account"}, CorrectOptionIndex = 1, Explanation = "Correct! Change your password immediately.", IsTrueFalse = false },
                new QuizQuestion { Question = "True or False: Social engineering is a cybersecurity threat.", Options = new List<string>{"True", "False"}, CorrectOptionIndex = 0, Explanation = "Correct! Social engineering is a common threat.", IsTrueFalse = true },
                new QuizQuestion { Question = "Which of these is a good password?", Options = new List<string>{"123456", "password", "Qw!8z$Lp9@", "yourname"}, CorrectOptionIndex = 2, Explanation = "Correct! Strong passwords use a mix of characters.", IsTrueFalse = false },
                new QuizQuestion { Question = "What is the best way to protect your privacy on social media?", Options = new List<string>{"Share everything", "Use strong privacy settings", "Accept all friend requests", "Post your location"}, CorrectOptionIndex = 1, Explanation = "Correct! Use strong privacy settings.", IsTrueFalse = false }
            };
        }

        private void InitializeQuizUI()
        {
            this.Text = "Cybersecurity Quiz";
            this.Width = 650;
            this.Height = 450;
            this.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);
            this.Font = new System.Drawing.Font("Segoe UI", 10);

            // Main panel for padding
            Panel mainPanel = new Panel { Name = "mainPanel", Left = 20, Top = 20, Width = 600, Height = 370, BackColor = System.Drawing.Color.White, BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(mainPanel);

            Label lblQuestion = new Label { Name = "lblQuestion", Top = 20, Left = 20, Width = 560, Height = 60, Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold) };
            GroupBox grpOptions = new GroupBox { Name = "grpOptions", Top = 90, Left = 20, Width = 560, Height = 150 };

            // Next Button
            Button btnNext = new Button { Name = "btnNext", Text = "Next", Top = 260, Left = 240, Width = 120, Height = 32 };
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnNext.ForeColor = System.Drawing.Color.White;
            btnNext.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnNext.Click += (s, e) => NextQuestion();

            Label lblFeedback = new Label { Name = "lblFeedback", Top = 310, Left = 20, Width = 560, Height = 40, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic) };

            mainPanel.Controls.Add(lblQuestion);
            mainPanel.Controls.Add(grpOptions);
            mainPanel.Controls.Add(btnNext);
            mainPanel.Controls.Add(lblFeedback);
        }

        private void DisplayQuestion()
        {
            if (currentQuestion >= questions.Count)
            {
                ShowFinalScore();
                return;
            }
            var mainPanel = this.Controls["mainPanel"] as Panel;
            var lblQuestion = mainPanel.Controls["lblQuestion"] as Label;
            var grpOptions = mainPanel.Controls["grpOptions"] as GroupBox;
            var lblFeedback = mainPanel.Controls["lblFeedback"] as Label;
            lblFeedback.Text = "";
            grpOptions.Controls.Clear();
            var q = questions[currentQuestion];
            lblQuestion.Text = $"Q{currentQuestion + 1}: {q.Question}";
            for (int i = 0; i < q.Options.Count; i++)
            {
                RadioButton rb = new RadioButton { Text = q.Options[i], Top = 10 + i * 30, Left = 10, Width = 500, Tag = i };
                grpOptions.Controls.Add(rb);
            }
        }

        private void NextQuestion()
        {
            var mainPanel = this.Controls["mainPanel"] as Panel;
            var grpOptions = mainPanel.Controls["grpOptions"] as GroupBox;
            var lblFeedback = mainPanel.Controls["lblFeedback"] as Label;
            int selected = -1;
            foreach (RadioButton rb in grpOptions.Controls)
            {
                if (rb.Checked)
                {
                    selected = (int)rb.Tag;
                    break;
                }
            }
            if (selected == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }
            var q = questions[currentQuestion];
            if (selected == q.CorrectOptionIndex)
            {
                score++;
                lblFeedback.ForeColor = System.Drawing.Color.Green;
                lblFeedback.Text = q.Explanation;
            }
            else
            {
                lblFeedback.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69); // Red
                lblFeedback.Text = $"Incorrect. {q.Explanation}";
            }
            currentQuestion++;
            var btnNext = mainPanel.Controls["btnNext"] as Button;
            btnNext.Enabled = false; // Disable button to prevent clicks while showing feedback

            // Use a timer to pause before showing the next question
            System.Windows.Forms.Timer feedbackTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            feedbackTimer.Tick += (s, ev) =>
            {
                feedbackTimer.Stop();
                if (currentQuestion < questions.Count)
                {
                    DisplayQuestion();
                }
                else
                {
                    ShowFinalScore();
                }
                btnNext.Enabled = true;
            };
            feedbackTimer.Start();
        }

        private void ShowFinalScore()
        {
            var mainPanel = this.Controls["mainPanel"] as Panel;
            mainPanel.Controls.Clear();
            ActivityLogger.Log($"Quiz finished. Final score: {score}/{questions.Count}.");

            Label lblScore = new Label { Text = $"Quiz Complete! Your score: {score}/{questions.Count}", Top = 80, Left = 0, Width = 600, Height = 40, TextAlign = System.Drawing.ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold) };
            Label lblFeedback = new Label { Top = 140, Left = 0, Width = 600, Height = 40, TextAlign = System.Drawing.ContentAlignment.MiddleCenter, Font = new System.Drawing.Font("Segoe UI", 12) };
            if (score >= 9)
                lblFeedback.Text = "Great job! You're a cybersecurity pro!";
            else if (score >= 6)
                lblFeedback.Text = "Good effort! Keep learning to stay safe online!";
            else
                lblFeedback.Text = "Keep practicing! Cybersecurity is important for everyone.";

            Button btnClose = new Button { Name = "btnClose", Text = "Close", Top = 220, Left = 240, Width = 120, Height = 32 };
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnClose.ForeColor = System.Drawing.Color.White;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnClose.Click += (s, e) => this.Close();

            mainPanel.Controls.Add(lblScore);
            mainPanel.Controls.Add(lblFeedback);
            mainPanel.Controls.Add(btnClose);
        }
    }
}