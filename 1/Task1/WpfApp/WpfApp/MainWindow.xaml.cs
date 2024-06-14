using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Xml;
using System.Xml.Schema;

namespace XmlDataApp
{
    public partial class MainWindow : Window
    {
        private bool isDataLoaded = false;
        private XmlDataManager xmlDataManager;
        private List<Person> people;
        private readonly string schemaFilePath = "Person_schema.xsd"; 

        public MainWindow()
        {
            InitializeComponent();
        }

        public class Person
        {
            public string Name { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
            public string Status { get; set; }

            public Person(string name, string gender, int age, string status)
            {
                Name = name;
                Gender = gender;
                Age = age;
                Status = status;
            }
        }

        private string loadedFilePath;

        private void LoadXml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                
                if (!ValidateXml(openFileDialog.FileName))
                {
                    MessageBox.Show("XML не соответствует схеме.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                xmlDataManager = new XmlDataManager(openFileDialog.FileName);
                people = xmlDataManager.ReadPeopleData();
                peopleListView.ItemsSource = people;
                loadedFilePath = openFileDialog.FileName;
            }
            isDataLoaded = true;
        }

        private bool ValidateXml(string xmlFilePath)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationEventHandler += ValidationCallback;

                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add(null, schemaFilePath);

                settings.Schemas = schemaSet;

                using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
                {
                    while (reader.Read()) ;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка валидации XML: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void ValidationCallback(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
            {
                MessageBox.Show($"Ошибка валидации XML: {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveXml_Click(object sender, RoutedEventArgs e)
        {
            if (xmlDataManager != null && people != null)
            {
                xmlDataManager.WritePeopleData(people);
                MessageBox.Show("Сохранено.");
            }
        }

        private void PeopleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (peopleListView.SelectedItem != null)
            {
                Person selectedPerson = (Person)peopleListView.SelectedItem;
                MessageBox.Show($"Выбранный человек: {selectedPerson.Name}");
            }
        }

        private void RefreshData_Click(object sender, RoutedEventArgs e)
        {
            if (isDataLoaded)
            {
                
                if (xmlDataManager != null && !string.IsNullOrEmpty(loadedFilePath))
                {
                    
                    people = xmlDataManager.ReadPeopleData();
                    peopleListView.ItemsSource = people;
                    MessageBox.Show("Данные обновлены.");
                }
                else
                {
                    MessageBox.Show("Нет загруженных данных для обновления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Загрузите данные перед обновлением.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (isDataLoaded)
            {
                AddPersonDialog addPersonDialog = new AddPersonDialog();
                if (addPersonDialog.ShowDialog() == true)
                {                   
                    if (addPersonDialog.Age < 0)
                    {
                        MessageBox.Show("Введён неверный возраст.", "Invalid Age", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    Person newPerson = new Person(addPersonDialog.Name, addPersonDialog.Gender, addPersonDialog.Age, addPersonDialog.Status);

                    people.Add(newPerson);
                    peopleListView.ItemsSource = people;

                    if (xmlDataManager != null)
                    {
                        xmlDataManager.WritePeopleData(people);
                        MessageBox.Show("Добавлено.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Загрузите данные прежде чем создавать новые.", "Data Not Loaded", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            
            if (peopleListView.SelectedItem != null)
            {
                
                Person selectedPerson = (Person)peopleListView.SelectedItem;

               
                people.Remove(selectedPerson);

                
                peopleListView.ItemsSource = null;
                peopleListView.ItemsSource = people;

                
                if (xmlDataManager != null && isDataLoaded)
                {
                    xmlDataManager.WritePeopleData(people);
                    MessageBox.Show("Удалено.");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "No Person Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public class XmlDataManager
        {
            private string filePath;

            public XmlDataManager(string filePath)
            {
                this.filePath = filePath;
            }

            

            public List<Person> ReadPeopleData()
            {
                List<Person> people = new List<Person>();

                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);

                    XmlNodeList personNodes = xmlDoc.SelectNodes("//Person");

                    foreach (XmlNode personNode in personNodes)
                    {
                        string name = personNode.SelectSingleNode("Name").InnerText;
                        string gender = personNode.SelectSingleNode("Gender").InnerText;
                        int age = Convert.ToInt32(personNode.SelectSingleNode("Age").InnerText);
                        string status = personNode.SelectSingleNode("Status").InnerText;

                        Person person = new Person(name, gender, age, status);
                        people.Add(person);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка загрузки " + ex.Message);
                }

                return people;
            }

            
            public void WritePeopleData(List<Person> people)
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xmlDoc.AppendChild(xmlDeclaration);

                    XmlNode rootElement = xmlDoc.CreateElement("People");
                    xmlDoc.AppendChild(rootElement);

                    foreach (Person person in people)
                    {
                        XmlNode personNode = xmlDoc.CreateElement("Person");

                        XmlNode nameNode = xmlDoc.CreateElement("Name");
                        nameNode.InnerText = person.Name;
                        personNode.AppendChild(nameNode);

                        XmlNode genderNode = xmlDoc.CreateElement("Gender");
                        genderNode.InnerText = person.Gender;
                        personNode.AppendChild(genderNode);

                        XmlNode ageNode = xmlDoc.CreateElement("Age");
                        ageNode.InnerText = person.Age.ToString();
                        personNode.AppendChild(ageNode);

                        XmlNode statusNode = xmlDoc.CreateElement("Status");
                        statusNode.InnerText = person.Status;
                        personNode.AppendChild(statusNode);

                        rootElement.AppendChild(personNode);
                    }

                    xmlDoc.Save(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing XML data: " + ex.Message);
                }
            }
        }
                
        public class AddPersonDialog : Window
        {
            public string Name { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
            public string Status { get; set; }

            private TextBox nameTextBox;
            private ComboBox genderComboBox;
            private TextBox ageTextBox;
            private TextBox statusTextBox;
            private Button okButton;
            private Button cancelButton;

            public AddPersonDialog()
            {
               
                Title = "Add Person";
                Width = 300;
                Height = 280;
                WindowStartupLocation = WindowStartupLocation.CenterScreen;

                
                nameTextBox = new TextBox();
                genderComboBox = new ComboBox { ItemsSource = GetGenderOptions() };
                ageTextBox = new TextBox();
                statusTextBox = new TextBox();
                okButton = new Button { Content = "OK" };
                cancelButton = new Button { Content = "Отмена" };

               
                okButton.Click += OkButton_Click;
                cancelButton.Click += CancelButton_Click;

                
                StackPanel mainPanel = new StackPanel();
                mainPanel.Children.Add(new Label { Content = "Имя:" });
                mainPanel.Children.Add(nameTextBox);
                mainPanel.Children.Add(new Label { Content = "Пол:" });
                mainPanel.Children.Add(genderComboBox);
                mainPanel.Children.Add(new Label { Content = "Возраст:" });
                mainPanel.Children.Add(ageTextBox);
                mainPanel.Children.Add(new Label { Content = "Профессия" });
                mainPanel.Children.Add(statusTextBox);
                mainPanel.Children.Add(okButton);
                mainPanel.Children.Add(cancelButton);

               
                Content = mainPanel;
            }

            private void OkButton_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                    {
                        throw new Exception("Пожалуйста, введите имя.");
                    }

                    Name = nameTextBox.Text;

                    if (genderComboBox.SelectedItem == null)
                    {
                        throw new Exception("Пожалуйста, выберите пол.");
                    }

                    Gender = genderComboBox.SelectedItem.ToString();

                    if (string.IsNullOrWhiteSpace(ageTextBox.Text))
                    {
                        throw new Exception("Пожалуйста, введите возраст.");
                    }

                    int age = int.Parse(ageTextBox.Text);

                    if (age < 0 || age > 110)
                    {
                        throw new Exception("Возраст должен быть от 0 до 110.");
                    }

                    Age = age;

                    if (string.IsNullOrWhiteSpace(statusTextBox.Text))
                    {
                        throw new Exception("Пожалуйста, введите статус.");
                    }

                    Status = statusTextBox.Text;

                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void CancelButton_Click(object sender, RoutedEventArgs e)
            {
              
                DialogResult = false;
            }

            private string[] GetGenderOptions()
            {
                
                return new string[] { "Мужской", "Женский" };
            }
        }
    }
}