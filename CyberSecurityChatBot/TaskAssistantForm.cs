using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace CyberSecurityChatBot
{
    public partial class TaskAssistantForm : Form
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private ListView lvTasks;

        public TaskAssistantForm()
        {
            InitializeTaskAssistantUI();
        }

        public TaskAssistantForm(string initialTitle, DateTime? initialReminder)
        {
            InitializeTaskAssistantUI();

            // Pre-fill the form based on NLP result
            var txtTitle = this.Controls.Find("txtTitle", true).FirstOrDefault() as TextBox;
            var dtpReminder = this.Controls.Find("dtpReminder", true).FirstOrDefault() as DateTimePicker;

            if (txtTitle != null)
            {
                txtTitle.Text = initialTitle;
            }

            if (dtpReminder != null && initialReminder.HasValue)
            {
                dtpReminder.Value = initialReminder.Value;
                dtpReminder.Checked = true;
            }
        }

        private void InitializeTaskAssistantUI()
        {
            this.Text = "Cybersecurity Task Assistant";
            this.Width = 650;
            this.Height = 450;
            this.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);
            this.Font = new System.Drawing.Font("Segoe UI", 10);

            // Main panel for padding
            Panel mainPanel = new Panel { Left = 20, Top = 20, Width = 600, Height = 370, BackColor = System.Drawing.Color.White, BorderStyle = BorderStyle.FixedSingle };
            this.Controls.Add(mainPanel);

            // Controls
            Label lblTitle = new Label { Text = "Task Title:", Top = 20, Left = 20, Width = 100 };
            TextBox txtTitle = new TextBox { Top = 20, Left = 130, Width = 200, Name = "txtTitle" };
            Label lblDesc = new Label { Text = "Description:", Top = 60, Left = 20, Width = 100 };
            TextBox txtDesc = new TextBox { Top = 60, Left = 130, Width = 200, Name = "txtDesc" };
            Label lblReminder = new Label { Text = "Reminder (optional):", Top = 100, Left = 20, Width = 140 };
            DateTimePicker dtpReminder = new DateTimePicker { Top = 100, Left = 170, Width = 160, Name = "dtpReminder", Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm", ShowCheckBox = true };

            // Add Task button with icon
            Button btnAdd = new Button { Text = "  Add Task", Top = 140, Left = 130, Width = 120, Height = 32, ImageAlign = System.Drawing.ContentAlignment.MiddleLeft };
            btnAdd.Image = System.Drawing.SystemIcons.Information.ToBitmap();
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnAdd.Click += (s, e) => AddTask(txtTitle, txtDesc, dtpReminder);

            // ListView for tasks
            lvTasks = new ListView { Top = 190, Left = 20, Width = 550, Height = 120, Name = "lvTasks", View = View.Details, FullRowSelect = true, GridLines = true, BackColor = System.Drawing.Color.WhiteSmoke };
            lvTasks.Columns.Add("Title", 120);
            lvTasks.Columns.Add("Description", 200);
            lvTasks.Columns.Add("Reminder", 120);
            lvTasks.Columns.Add("Status", 80);

            // Complete button with icon
            Button btnComplete = new Button { Text = "  Mark as Completed", Top = 320, Left = 20, Width = 170, Height = 32, ImageAlign = System.Drawing.ContentAlignment.MiddleLeft };
            btnComplete.Image = System.Drawing.SystemIcons.Shield.ToBitmap();
            btnComplete.FlatStyle = FlatStyle.Flat;
            btnComplete.BackColor = System.Drawing.Color.FromArgb(0, 153, 51);
            btnComplete.ForeColor = System.Drawing.Color.White;
            btnComplete.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnComplete.Click += (s, e) => CompleteTask();

            // Delete button with icon
            Button btnDelete = new Button { Text = "  Delete Task", Top = 320, Left = 210, Width = 140, Height = 32, ImageAlign = System.Drawing.ContentAlignment.MiddleLeft };
            btnDelete.Image = System.Drawing.SystemIcons.Error.ToBitmap();
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnDelete.Click += (s, e) => DeleteTask();

            // Add controls to main panel
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(txtTitle);
            mainPanel.Controls.Add(lblDesc);
            mainPanel.Controls.Add(txtDesc);
            mainPanel.Controls.Add(lblReminder);
            mainPanel.Controls.Add(dtpReminder);
            mainPanel.Controls.Add(btnAdd);
            mainPanel.Controls.Add(lvTasks);
            mainPanel.Controls.Add(btnComplete);
            mainPanel.Controls.Add(btnDelete);
        }

        private void AddTask(TextBox txtTitle, TextBox txtDesc, DateTimePicker dtpReminder)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }
            var task = new TaskItem
            {
                Title = txtTitle.Text,
                Description = txtDesc.Text,
                Reminder = dtpReminder.Checked ? dtpReminder.Value : (DateTime?)null,
                IsCompleted = false
            };
            tasks.Add(task);
            ActivityLogger.Log($"Task added: '{task.Title}' with reminder set to {task.Reminder?.ToString("g") ?? "none"}.");
            RefreshTaskList();
            txtTitle.Text = "";
            txtDesc.Text = "";
            dtpReminder.Value = DateTime.Now;
            dtpReminder.Checked = false;
        }

        private void RefreshTaskList()
        {
            lvTasks.Items.Clear();
            foreach (var task in tasks)
            {
                var reminder = task.Reminder.HasValue ? task.Reminder.Value.ToString("g") : "";
                var status = task.IsCompleted ? "Completed" : "Pending";
                var item = new ListViewItem(new[] { task.Title, task.Description, reminder, status });
                lvTasks.Items.Add(item);
            }
        }

        private void CompleteTask()
        {
            if (lvTasks.SelectedItems.Count > 0)
            {
                int idx = lvTasks.SelectedItems[0].Index;
                tasks[idx].IsCompleted = true;
                ActivityLogger.Log($"Task marked as completed: '{tasks[idx].Title}'.");
                RefreshTaskList();
            }
        }

        private void DeleteTask()
        {
            if (lvTasks.SelectedItems.Count > 0)
            {
                int idx = lvTasks.SelectedItems[0].Index;
                ActivityLogger.Log($"Task deleted: '{tasks[idx].Title}'.");
                tasks.RemoveAt(idx);
                RefreshTaskList();
            }
        }
    }
}