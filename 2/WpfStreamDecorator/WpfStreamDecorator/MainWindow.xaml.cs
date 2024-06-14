using System;
using System.IO;
using System.Text;
using System.Windows;
using StreamDecoratorLibrary;


namespace StreamDecoratorWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReadFileStream_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FileStream fileStream = new FileStream("example.txt", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        outputTextBox.Text = reader.ReadToEnd();
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                outputTextBox.Text = "Файл не найден: " + ex.Message;
            }
            catch (Exception ex)
            {
                outputTextBox.Text = "Ошибка чтения: " + ex.Message;
            }
        }

        private void ReadMemoryStream_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(" private void ReadMemoryStream_Click(object sender, RoutedEventArgs e)\r\n        {\r\n            try\r\n            {\r\n                byte[] data = Encoding.UTF8.GetBytes(\"\");\r\n                using (MemoryStream memoryStream = new MemoryStream(data))\r\n                {\r\n                    using (StreamReader reader = new StreamReader(memoryStream))\r\n                    {\r\n                        outputTextBox.Text = reader.ReadToEnd();\r\n                    }\r\n                }\r\n            }\r\n            catch (Exception ex)\r\n            {\r\n                outputTextBox.Text = \"Error reading memory stream: \" + ex.Message;\r\n            }\r\n        }");
                using (MemoryStream memoryStream = new MemoryStream(data))
                {
                    using (StreamReader reader = new StreamReader(memoryStream))
                    {
                        outputTextBox.Text = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                outputTextBox.Text = "Ошибка чтения: " + ex.Message;
            }
        }

        private bool firstCall = true;

        private byte[] fileBuffer;

        private void ReadBufferedStream_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FileStream fileStream = new FileStream("example.txt", FileMode.Open))
                {
                    using (BufferedStreamDecorator bufferedStream = new BufferedStreamDecorator(fileStream, 1024))
                    {
                        using (StreamReader reader = new StreamReader(bufferedStream))
                        {
                            if (firstCall)
                            {
                                string text = reader.ReadToEnd();
                                int bytesRead = Encoding.UTF8.GetByteCount(text);
                                outputTextBox.Text = $"Число считанных байт: {bytesRead}";
                                fileBuffer = Encoding.UTF8.GetBytes(text);
                                firstCall = false;
                            }
                            else
                            {
                                string text = reader.ReadToEnd();
                                int bytesRead = Encoding.UTF8.GetByteCount(text);
                                outputTextBox.Text = text + $"\n\nЧисло считанных байт: {bytesRead}";
                                fileBuffer = Encoding.UTF8.GetBytes(text);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                outputTextBox.Text = "Файл не найден: " + ex.Message;
            }
            catch (Exception ex)
            {
                outputTextBox.Text = "Ошибка чтения: " + ex.Message;
            }
        }
        private void ShowBufferedData_Click(object sender, RoutedEventArgs e)
        {
            if (fileBuffer != null)
            {
                try
                {
                    string bufferedData = Encoding.UTF8.GetString(fileBuffer);
                    outputTextBox.Text = $"Содержимое буфера:\n\n{bufferedData}";
                }
                catch (Exception ex)
                {
                    outputTextBox.Text = "Ошибка при выводе содержимого буфера: " + ex.Message;
                }
            }
            else
            {
                outputTextBox.Text = "Буфер пуст.";
            }
        }


    }
}
