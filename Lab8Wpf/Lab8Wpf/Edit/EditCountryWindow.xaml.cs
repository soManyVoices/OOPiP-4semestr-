using System.Windows;

namespace Lab8Wpf
{
    public partial class EditCountryWindow : Window
    {
        public Countries EditedCountry { get; private set; }

        public EditCountryWindow(Countries country)
        {
            InitializeComponent();

            EditedCountry = country;
            CountryNameTextBox.Text = country.CountryName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CountryNameTextBox.Text))
            {
                EditedCountry.CountryName = CountryNameTextBox.Text;
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
