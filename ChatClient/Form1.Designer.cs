namespace ChatClient
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
            txtChat = new TextBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            label1 = new Label();
            txtPort = new TextBox();
            btnConnect = new Button();
            SuspendLayout();
            // 
            // txtChat
            // 
            txtChat.Location = new Point(57, 32);
            txtChat.Multiline = true;
            txtChat.Name = "txtChat";
            txtChat.Size = new Size(335, 245);
            txtChat.TabIndex = 0;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(57, 317);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(335, 23);
            txtMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(317, 357);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 6;
            btnSend.Text = "Отправить";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(428, 32);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 7;
            label1.Text = "Port";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(463, 29);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(115, 23);
            txtPort.TabIndex = 8;
            txtPort.Text = "13000";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(479, 58);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(99, 23);
            btnConnect.TabIndex = 9;
            btnConnect.Text = "Подключиться";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 450);
            Controls.Add(btnConnect);
            Controls.Add(txtPort);
            Controls.Add(label1);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(txtChat);
            Name = "Form1";
            Text = "ChatClient";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtChat;
        private TextBox txtMessage;
        private Button btnSend;
        private Label label1;
        private TextBox txtPort;
        private Button btnConnect;
    }
}
