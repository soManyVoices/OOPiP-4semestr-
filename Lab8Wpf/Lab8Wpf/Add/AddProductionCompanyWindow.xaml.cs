using System.Windows;

namespace Lab8Wpf
{
    public partial class AddProductionCompanyWindow : Window
    {
        public ProductionCompanies NewProductionCompany { get; private set; }

        public AddProductionCompanyWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CompanyNameTextBox.Text))
            {
                NewProductionCompany = new ProductionCompanies { CompanyName = CompanyNameTextBox.Text };
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Company name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
