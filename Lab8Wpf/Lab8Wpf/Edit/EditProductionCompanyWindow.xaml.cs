using System.Windows;

namespace Lab8Wpf
{
    public partial class EditProductionCompanyWindow : Window
    {
        public ProductionCompanies EditedProductionCompany { get; private set; }

        public EditProductionCompanyWindow(ProductionCompanies productionCompany)
        {
            InitializeComponent();

            EditedProductionCompany = productionCompany;
            CompanyNameTextBox.Text = productionCompany.CompanyName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CompanyNameTextBox.Text))
            {
                EditedProductionCompany.CompanyName = CompanyNameTextBox.Text;
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
