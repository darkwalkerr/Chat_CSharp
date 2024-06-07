using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        // Объявляем переменные для клиента, сетевого потока и потока для получения сообщений
        private TcpClient client = null;
        private NetworkStream stream = null;
        private Thread receiveThread = null;

        // Конструктор формы
        public Form1()
        {
            InitializeComponent();
        }

        // Обработчик кнопки подключения
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Подключаемся к серверу по указанному порту
                client = new TcpClient("127.0.0.1", int.Parse(txtPort.Text));
                // Получаем сетевой поток для обмена данными
                stream = client.GetStream();
                // Создаем и запускаем поток для получения сообщений
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();
                // Выводим сообщение о подключении
                AppendChat("Подключено к серверу!");
                MessageBox.Show("Подключено к серверу!");
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибки подключения
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }

        // Обработчик кнопки отправки сообщения
        private void btnSend_Click(object sender, EventArgs e)
        {
            // Проверяем, подключен ли клиент к серверу
            if (client == null)
            {
                MessageBox.Show("Сначала подключитесь к серверу.");
                return;
            }

            // Получаем текст сообщения из текстового поля
            string message = txtMessage.Text;
            // Преобразуем сообщение в массив байтов
            byte[] data = Encoding.UTF8.GetBytes(message);
            // Отправляем данные на сервер
            stream.Write(data, 0, data.Length);

            // Добавляем сообщение в чат клиента
            AppendChat("Клиент: " + message);
            // Очищаем текстовое поле ввода сообщения
            txtMessage.Clear();
        }

        // Метод для получения сообщений от сервера
        private void ReceiveMessages()
        {
            byte[] bytes = new byte[1024];
            int i;

            while (true)
            {
                try
                {
                    // Читаем данные из сетевого потока
                    i = stream.Read(bytes, 0, bytes.Length);
                    // Если нет данных, выходим из цикла
                    if (i == 0) break;

                    // Преобразуем полученные байты в строку
                    string message = Encoding.UTF8.GetString(bytes, 0, i);
                    // Добавляем сообщение в чат клиента
                    AppendChat($"Сервер: {message}");
                }
                catch
                {
                    // В случае ошибки выходим из цикла
                    break;
                }
            }
        }

        // Метод для добавления сообщений в текстовое поле чата
        private void AppendChat(string message)
        {
            if (InvokeRequired)
            {
                // Если вызов сделан не из основного потока, используем Invoke
                Invoke(new Action<string>(AppendChat), message);
            }
            else
            {
                // Добавляем сообщение в текстовое поле
                txtChat.AppendText(message + Environment.NewLine);
            }
        }

        // Обработчик события закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Закрываем сетевой поток и клиентское подключение
            stream?.Close();
            client?.Close();
            // Прерываем поток получения сообщений
            receiveThread?.Abort();
        }
    }
}
