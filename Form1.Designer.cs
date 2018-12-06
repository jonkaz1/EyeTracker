namespace EyeTracker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keybindingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editKeybindingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutEyeTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Action1Label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Action2Label = new System.Windows.Forms.Label();
            this.Action3Label = new System.Windows.Forms.Label();
            this.ExecutedCommandLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.button1.Location = new System.Drawing.Point(571, 264);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 96);
            this.button1.TabIndex = 1;
            this.button1.Text = "Control mouse with eyes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(720, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(66, 20);
            this.toolStripMenuItem1.Text = "Calibrate";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keybindingsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.editKeybindingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.settingsToolStripMenuItem.Text = "Tools";
            // 
            // keybindingsToolStripMenuItem
            // 
            this.keybindingsToolStripMenuItem.Name = "keybindingsToolStripMenuItem";
            this.keybindingsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.keybindingsToolStripMenuItem.Text = "Keybindings";
            this.keybindingsToolStripMenuItem.Click += new System.EventHandler(this.keybindingsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // editKeybindingsToolStripMenuItem
            // 
            this.editKeybindingsToolStripMenuItem.Name = "editKeybindingsToolStripMenuItem";
            this.editKeybindingsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.editKeybindingsToolStripMenuItem.Text = "Edit Keybindings";
            this.editKeybindingsToolStripMenuItem.Click += new System.EventHandler(this.editKeybindingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.aboutEyeTrackerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.aboutToolStripMenuItem.Text = "View Help";
            // 
            // aboutEyeTrackerToolStripMenuItem
            // 
            this.aboutEyeTrackerToolStripMenuItem.Name = "aboutEyeTrackerToolStripMenuItem";
            this.aboutEyeTrackerToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.aboutEyeTrackerToolStripMenuItem.Text = "About EyeTracker";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Action1Label
            // 
            this.Action1Label.AutoSize = true;
            this.Action1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Action1Label.Location = new System.Drawing.Point(109, 150);
            this.Action1Label.Name = "Action1Label";
            this.Action1Label.Size = new System.Drawing.Size(40, 25);
            this.Action1Label.TabIndex = 3;
            this.Action1Label.Text = "A1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(109, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Executed Command: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(109, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Observed Actions:";
            // 
            // Action2Label
            // 
            this.Action2Label.AutoSize = true;
            this.Action2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Action2Label.Location = new System.Drawing.Point(212, 150);
            this.Action2Label.Name = "Action2Label";
            this.Action2Label.Size = new System.Drawing.Size(40, 25);
            this.Action2Label.TabIndex = 8;
            this.Action2Label.Text = "A2";
            // 
            // Action3Label
            // 
            this.Action3Label.AutoSize = true;
            this.Action3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Action3Label.Location = new System.Drawing.Point(330, 150);
            this.Action3Label.Name = "Action3Label";
            this.Action3Label.Size = new System.Drawing.Size(40, 25);
            this.Action3Label.TabIndex = 9;
            this.Action3Label.Text = "A3";
            // 
            // ExecutedCommandLabel
            // 
            this.ExecutedCommandLabel.AutoSize = true;
            this.ExecutedCommandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecutedCommandLabel.Location = new System.Drawing.Point(303, 216);
            this.ExecutedCommandLabel.Name = "ExecutedCommandLabel";
            this.ExecutedCommandLabel.Size = new System.Drawing.Size(160, 16);
            this.ExecutedCommandLabel.TabIndex = 10;
            this.ExecutedCommandLabel.Text = "ExecutedCommandLabel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Once you use any eye actions, they wil be displayed here";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 360);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExecutedCommandLabel);
            this.Controls.Add(this.Action3Label);
            this.Controls.Add(this.Action2Label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Action1Label);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EyeTracker";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutEyeTrackerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keybindingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editKeybindingsToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label Action1Label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Action2Label;
        private System.Windows.Forms.Label Action3Label;
        private System.Windows.Forms.Label ExecutedCommandLabel;
        private System.Windows.Forms.Label label1;
    }
}

