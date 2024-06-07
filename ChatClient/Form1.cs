using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        // ��������� ���������� ��� �������, �������� ������ � ������ ��� ��������� ���������
        private TcpClient client = null;
        private NetworkStream stream = null;
        private Thread receiveThread = null;

        // ����������� �����
        public Form1()
        {
            InitializeComponent();
        }

        // ���������� ������ �����������
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // ������������ � ������� �� ���������� �����
                client = new TcpClient("127.0.0.1", int.Parse(txtPort.Text));
                // �������� ������� ����� ��� ������ �������
                stream = client.GetStream();
                // ������� � ��������� ����� ��� ��������� ���������
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();
                // ������� ��������� � �����������
                AppendChat("���������� � �������!");
                MessageBox.Show("���������� � �������!");
            }
            catch (Exception ex)
            {
                // ������������ ������ �����������
                MessageBox.Show("������ �����������: " + ex.Message);
            }
        }

        // ���������� ������ �������� ���������
        private void btnSend_Click(object sender, EventArgs e)
        {
            // ���������, ��������� �� ������ � �������
            if (client == null)
            {
                MessageBox.Show("������� ������������ � �������.");
                return;
            }

            // �������� ����� ��������� �� ���������� ����
            string message = txtMessage.Text;
            // ����������� ��������� � ������ ������
            byte[] data = Encoding.UTF8.GetBytes(message);
            // ���������� ������ �� ������
            stream.Write(data, 0, data.Length);

            // ��������� ��������� � ��� �������
            AppendChat("������: " + message);
            // ������� ��������� ���� ����� ���������
            txtMessage.Clear();
        }

        // ����� ��� ��������� ��������� �� �������
        private void ReceiveMessages()
        {
            byte[] bytes = new byte[1024];
            int i;

            while (true)
            {
                try
                {
                    // ������ ������ �� �������� ������
                    i = stream.Read(bytes, 0, bytes.Length);
                    // ���� ��� ������, ������� �� �����
                    if (i == 0) break;

                    // ����������� ���������� ����� � ������
                    string message = Encoding.UTF8.GetString(bytes, 0, i);
                    // ��������� ��������� � ��� �������
                    AppendChat($"������: {message}");
                }
                catch
                {
                    // � ������ ������ ������� �� �����
                    break;
                }
            }
        }

        // ����� ��� ���������� ��������� � ��������� ���� ����
        private void AppendChat(string message)
        {
            if (InvokeRequired)
            {
                // ���� ����� ������ �� �� ��������� ������, ���������� Invoke
                Invoke(new Action<string>(AppendChat), message);
            }
            else
            {
                // ��������� ��������� � ��������� ����
                txtChat.AppendText(message + Environment.NewLine);
            }
        }

        // ���������� ������� �������� �����
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ��������� ������� ����� � ���������� �����������
            stream?.Close();
            client?.Close();
            // ��������� ����� ��������� ���������
            receiveThread?.Abort();
        }
    }
}
