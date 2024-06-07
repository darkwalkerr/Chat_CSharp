using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        // ��������� ���������� ��� �������, ������ �������, ������ �������� � ������� ��� ����������
        private TcpListener server = null;
        private Thread serverThread = null;
        private List<TcpClient> clients = new List<TcpClient>();
        private readonly object lockObject = new object();

        // ����������� �����
        public Form1()
        {
            InitializeComponent();
        }

        // ���������� ������ "�����"
        private void btnStart_Click(object sender, EventArgs e)
        {
            int port;
            // ���������, �������� �� ����� � ���� ����� ������
            if (int.TryParse(txtPort.Text, out port))
            {
                // ������� � ��������� ����� �������
                serverThread = new Thread(() => StartServer(port));
                serverThread.IsBackground = true;
                serverThread.Start();
                AppendLog($"������ ������� �� ����� {port}");
            }
            else
            {
                // ���� ���� ������ �����������, ���������� ��������� �� ������
                MessageBox.Show("������� ���������� ����� �����.");
            }
        }

        // ���������� ������ "���������"
        private void btnSend_Click(object sender, EventArgs e)
        {
            // �������� ��������� �� ���������� ����
            string message = txtMessage.Text;
            // ���������� ��������� ���� ��������
            BroadcastMessage(message, null, true);
            // ������� ��������� ����
            txtMessage.Clear();
        }

        // ����� ��� ������� �������
        private void StartServer(int port)
        {
            try
            {
                // ������� TcpListener � �������� ������� �� ��������� �����
                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                // ����������� ���� ��� ������ ��������
                while (true)
                {
                    // ��������� ����������� �� �������
                    TcpClient client = server.AcceptTcpClient();
                    lock (lockObject) // ���������� ���������� ��� ����������� ������� � ������ ��������
                    {
                        clients.Add(client);
                    }
                    AppendLog("��������� ������!");

                    // ������� � ��������� ����� ��� ��������� �������
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (SocketException ex)
            {
                // �������� ������ ������
                AppendLog("SocketException: " + ex.Message);
            }
            finally
            {
                // ������������� ������, ���� �������� ����������
                server?.Stop();
            }
        }

        // ����� ��� ��������� �������
        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                byte[] bytes = new byte[1024];
                int i;

                // ����������� ���� ��� ������ ������ �� �������
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // ����������� ����� � ������
                    string data = Encoding.UTF8.GetString(bytes, 0, i);
                    AppendLog($"������: {data}");

                    // ���������� ��������� ���� ��������, ����� �����������
                    BroadcastMessage(data, client, false);
                }
            }
            catch (Exception ex)
            {
                // �������� ���������� (�������� ����������������)
                //AppendLog("Exception: " + ex.Message);
            }
            finally
            {
                // ������� ������� �� ������ ��������
                lock (lockObject)
                {
                    clients.Remove(client);
                }
                // ��������� ���������� � ��������
                client.Close();
                AppendLog("������ ��������!");
            }
        }

        // ����� ��� �������� ��������� ���� ��������
        private void BroadcastMessage(string message, TcpClient excludeClient = null, bool logMessage = true)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // ���������� ��������� ���� ��������, ����� ������������
            foreach (var client in clients)
            {
                if (client != excludeClient)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(msg, 0, msg.Length);
                }
            }

            // �������� ������������ ���������, ���� ����������
            if (logMessage)
            {
                AppendLog($"������: {message}");
            }
        }

        // ����� ��� ���������� ��������� � ���
        private void AppendLog(string message)
        {
            if (InvokeRequired)
            {
                // ���� ����� ������ �� �� ��������� ������, ���������� Invoke
                Invoke(new Action<string>(AppendLog), message);
            }
            else
            {
                // ��������� ��������� � ��������� ���� ����
                txtLog.AppendText(message + Environment.NewLine);
            }
        }
    }
}
