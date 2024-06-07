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
        // Объявляем переменные для сервера, потока сервера, списка клиентов и объекта для блокировки
        private TcpListener server = null;
        private Thread serverThread = null;
        private List<TcpClient> clients = new List<TcpClient>();
        private readonly object lockObject = new object();

        // Конструктор формы
        public Form1()
        {
            InitializeComponent();
        }

        // Обработчик кнопки "Старт"
        private void btnStart_Click(object sender, EventArgs e)
        {
            int port;
            // Проверяем, является ли текст в поле порта числом
            if (int.TryParse(txtPort.Text, out port))
            {
                // Создаем и запускаем поток сервера
                serverThread = new Thread(() => StartServer(port));
                serverThread.IsBackground = true;
                serverThread.Start();
                AppendLog($"Сервер запущен на порту {port}");
            }
            else
            {
                // Если порт введен некорректно, показываем сообщение об ошибке
                MessageBox.Show("Введите правильный номер порта.");
            }
        }

        // Обработчик кнопки "Отправить"
        private void btnSend_Click(object sender, EventArgs e)
        {
            // Получаем сообщение из текстового поля
            string message = txtMessage.Text;
            // Отправляем сообщение всем клиентам
            BroadcastMessage(message, null, true);
            // Очищаем текстовое поле
            txtMessage.Clear();
        }

        // Метод для запуска сервера
        private void StartServer(int port)
        {
            try
            {
                // Создаем TcpListener и начинаем слушать на указанном порту
                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                // Бесконечный цикл для приема клиентов
                while (true)
                {
                    // Принимаем подключение от клиента
                    TcpClient client = server.AcceptTcpClient();
                    lock (lockObject) // Используем блокировку для безопасного доступа к списку клиентов
                    {
                        clients.Add(client);
                    }
                    AppendLog("Подключен клиент!");

                    // Создаем и запускаем поток для обработки клиента
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (SocketException ex)
            {
                // Логируем ошибки сокета
                AppendLog("SocketException: " + ex.Message);
            }
            finally
            {
                // Останавливаем сервер, если возникло исключение
                server?.Stop();
            }
        }

        // Метод для обработки клиента
        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                byte[] bytes = new byte[1024];
                int i;

                // Бесконечный цикл для чтения данных от клиента
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Преобразуем байты в строку
                    string data = Encoding.UTF8.GetString(bytes, 0, i);
                    AppendLog($"Клиент: {data}");

                    // Отправляем сообщение всем клиентам, кроме отправителя
                    BroadcastMessage(data, client, false);
                }
            }
            catch (Exception ex)
            {
                // Логируем исключения (временно закомментировано)
                //AppendLog("Exception: " + ex.Message);
            }
            finally
            {
                // Удаляем клиента из списка клиентов
                lock (lockObject)
                {
                    clients.Remove(client);
                }
                // Закрываем соединение с клиентом
                client.Close();
                AppendLog("Клиент отключен!");
            }
        }

        // Метод для отправки сообщений всем клиентам
        private void BroadcastMessage(string message, TcpClient excludeClient = null, bool logMessage = true)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем сообщение всем клиентам, кроме исключенного
            foreach (var client in clients)
            {
                if (client != excludeClient)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(msg, 0, msg.Length);
                }
            }

            // Логируем отправленное сообщение, если необходимо
            if (logMessage)
            {
                AppendLog($"Сервер: {message}");
            }
        }

        // Метод для добавления сообщений в лог
        private void AppendLog(string message)
        {
            if (InvokeRequired)
            {
                // Если вызов сделан не из основного потока, используем Invoke
                Invoke(new Action<string>(AppendLog), message);
            }
            else
            {
                // Добавляем сообщение в текстовое поле лога
                txtLog.AppendText(message + Environment.NewLine);
            }
        }
    }
}
