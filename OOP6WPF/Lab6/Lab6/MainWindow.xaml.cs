using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab6
{
    public partial class MainWindow : Window
    {
        private string _connectionString = "Data Source=(local);Initial Catalog=Personnel;Integrated Security=True";
        private IDao<Person> _personDao = new PersonDao();
        private IDao<Job> _jobDao = new JobDao();
        private IDao<Status> _statusDao = new StatusDao();
        private string _selectedTable = "Person"; 

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<Person> persons = _personDao.GetAll();
            personsListBox.ItemsSource = persons;

            List<Job> jobs = _jobDao.GetAll();
            jobsListBox.ItemsSource = jobs;
            jobsComboBox.ItemsSource = jobs;
            jobsComboBox.DisplayMemberPath = "Name";
            jobsComboBox.SelectedValuePath = "ID";

            List<Status> statuses = _statusDao.GetAll();
            statusesComboBox.ItemsSource = statuses;
            statusesComboBox.DisplayMemberPath = "StatusName";
            statusesComboBox.SelectedValuePath = "StatusID";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTable == "Person")
            {
                Person person = new Person
                {
                    Name = nameTextBox.Text,
                    Gender = genderComboBox.SelectedItem.ToString(),
                    Age = int.Parse(ageTextBox.Text),
                    Job_ID = (int?)jobsComboBox.SelectedValue,
                    Status_ID = (int?)statusesComboBox.SelectedValue
                };
                _personDao.Add(person);
                LoadData();
            }
            else
            {
                Job job = new Job
                {
                    Name = NameTextBox.Text
                };
                _jobDao.Add(job);
                LoadData();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTable == "Person")
            {
                Person person = (Person)personsListBox.SelectedItem;
                _personDao.Delete(person.PersonID);
                LoadData();
            }
            else
            {
                Job job = (Job)jobsListBox.SelectedItem;
                _jobDao.Delete(job.ID);
                LoadData();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTable == "Person")
            {
                Person person = (Person)personsListBox.SelectedItem;
                person.Name = nameTextBox.Text;
                person.Gender = genderComboBox.SelectedItem.ToString();
                person.Age = int.Parse(ageTextBox.Text);
                person.Job_ID = (int?)jobsComboBox.SelectedValue;
                person.Status_ID = (int?)statusesComboBox.SelectedValue;
                _personDao.Update(person);
                LoadData();
            }
            else
            {
                Job job = (Job)jobsListBox.SelectedItem;
                job.Name = NameTextBox.Text;
                _jobDao.Update(job);
                LoadData();
            }
        }

        private void JobButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTable == "Person")
            {
                _selectedTable = "Job";

                // Скрыть элементы управления для таблицы "Person"
                personsListBox.Visibility = Visibility.Collapsed;
                nameTextBox.Visibility = Visibility.Collapsed;
                genderComboBox.Visibility = Visibility.Collapsed;
                ageTextBox.Visibility = Visibility.Collapsed;
                jobsComboBox.Visibility = Visibility.Collapsed;
                statusesComboBox.Visibility = Visibility.Collapsed;

                // Отобразить элементы управления для таблицы "Job"
                jobsListBox.Visibility = Visibility.Visible;
                nameTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                _selectedTable = "Person";

                // Скрыть элементы управления для таблицы "Job"
                jobsListBox.Visibility = Visibility.Collapsed;

                // Отобразить элементы управления для таблицы "Person"
                personsListBox.Visibility = Visibility.Visible;
                nameTextBox.Visibility = Visibility.Visible;
                genderComboBox.Visibility = Visibility.Visible;
                ageTextBox.Visibility = Visibility.Visible;
                jobsComboBox.Visibility = Visibility.Visible;
                statusesComboBox.Visibility = Visibility.Visible;
            }
        }
    }
}
