namespace ChatServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtLog = new TextBox();
            txtMessage = new TextBox();
            txtPort = new TextBox();
            label1 = new Label();
            btnStart = new Button();
            btnSend = new Button();
            SuspendLayout();
            // 
            // txtLog
            // 
            txtLog.Location = new Point(47, 35);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(335, 244);
            txtLog.TabIndex = 0;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(47, 320);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(335, 23);
            txtMessage.TabIndex = 1;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(454, 35);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(115, 23);
            txtPort.TabIndex = 2;
            txtPort.Text = "13000";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(419, 38);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 3;
            label1.Text = "Port";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(487, 74);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(82, 23);
            btnStart.TabIndex = 4;
            btnStart.Text = "Старт";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(307, 362);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 5;
            btnSend.Text = "Отправить";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 450);
            Controls.Add(btnSend);
            Controls.Add(btnStart);
            Controls.Add(label1);
            Controls.Add(txtPort);
            Controls.Add(txtMessage);
            Controls.Add(txtLog);
            Name = "Form1";
            Text = "ChatServer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLog;
        private TextBox txtMessage;
        private TextBox txtPort;
        private Label label1;
        private Button btnStart;
        private Button btnSend;
    }
}
