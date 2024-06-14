using System.Windows;

namespace Lab8Wpf
{
    public partial class AddCountryWindow : Window
    {
        public Countries NewCountry { get; private set; }

        public AddCountryWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CountryNameTextBox.Text))
            {
                NewCountry = new Countries { CountryName = CountryNameTextBox.Text };
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Country name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
