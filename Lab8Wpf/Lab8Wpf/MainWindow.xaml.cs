using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace Lab8Wpf
{
    public partial class MainWindow : Window
    {
        private MediaLibraryDataContext _context;
        private ObservableCollection<Genres> _genres;
        private ObservableCollection<Countries> _countries;
        private ObservableCollection<ProductionCompanies> _productionCompanies;
        private ObservableCollection<Films> _films;

        public MainWindow()
        {
            InitializeComponent();
            _context = new MediaLibraryDataContext("Data Source=(local);Initial Catalog=MediaLibrary;Integrated Security=True");
            TableComboBox.Items.Add("Genres");
            TableComboBox.Items.Add("Countries");
            TableComboBox.Items.Add("ProductionCompanies");
            TableComboBox.Items.Add("Films");
        }

        private void TableComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TableComboBox.SelectedItem.ToString())
            {
                case "Genres":
                    LoadGenres();
                    break;
                case "Countries":
                    LoadCountries();
                    break;
                case "ProductionCompanies":
                    LoadProductionCompanies();
                    break;
                case "Films":
                    LoadFilms();
                    break;
            }
        }

        private void LoadGenres()
        {
            _genres = new ObservableCollection<Genres>(_context.Genres);
            DataGrid.ItemsSource = _genres;
        }

        private void LoadCountries()
        {
            _countries = new ObservableCollection<Countries>(_context.Countries);
            DataGrid.ItemsSource = _countries;
        }

        private void LoadProductionCompanies()
        {
            _productionCompanies = new ObservableCollection<ProductionCompanies>(_context.ProductionCompanies);
            DataGrid.ItemsSource = _productionCompanies;
        }

        private void LoadFilms()
        {
            _films = new ObservableCollection<Films>(_context.Films);
            DataGrid.ItemsSource = _films;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            switch (TableComboBox.SelectedItem.ToString())
            {
                case "Genres":
                    AddGenre();
                    break;
                case "Countries":
                    AddCountry();
                    break;
                case "ProductionCompanies":
                    AddProductionCompany();
                    break;
                case "Films":
                    AddFilm();
                    break;
            }
        }
        private void AddGenre()
        {
            var addGenreWindow = new AddGenreWindow();
            if (addGenreWindow.ShowDialog() == true)
            {
                _context.Genres.InsertOnSubmit(addGenreWindow.NewGenre);
                _context.SubmitChanges();
                _genres.Add(addGenreWindow.NewGenre);
                DataGrid.ItemsSource = _genres;
                DataGrid.SelectedItem = addGenreWindow.NewGenre;
            }
        }
        private void AddCountry()
        {
            var addCountryWindow = new AddCountryWindow();
            if (addCountryWindow.ShowDialog() == true)
            {
                _context.Countries.InsertOnSubmit(addCountryWindow.NewCountry);
                _context.SubmitChanges();
                _countries.Add(addCountryWindow.NewCountry);
                DataGrid.ItemsSource = _countries;
                DataGrid.SelectedItem = addCountryWindow.NewCountry;
            }
        }
        private void AddProductionCompany()
        {
            var addProductionCompanyWindow = new AddProductionCompanyWindow();
            if (addProductionCompanyWindow.ShowDialog() == true)
            {
                _context.ProductionCompanies.InsertOnSubmit(addProductionCompanyWindow.NewProductionCompany);
                _context.SubmitChanges();
                _productionCompanies.Add(addProductionCompanyWindow.NewProductionCompany);
                DataGrid.ItemsSource = _productionCompanies;
                DataGrid.SelectedItem = addProductionCompanyWindow.NewProductionCompany;
            }
        }
        private void AddFilm()
        {
            var addFilmWindow = new AddFilmWindow(_context);
            if (addFilmWindow.ShowDialog() == true)
            {
                _context.Films.InsertOnSubmit(addFilmWindow.NewFilm);
                _context.SubmitChanges();
                _films.Add(addFilmWindow.NewFilm);
                DataGrid.ItemsSource = _films;
                DataGrid.SelectedItem = addFilmWindow.NewFilm;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGrid.SelectedItem;

            switch (TableComboBox.SelectedItem.ToString())
            {
                case "Genres":
                    EditGenre(selectedItem as Genres);
                    break;
                case "Countries":
                    EditCountry(selectedItem as Countries);
                    break;
                case "ProductionCompanies":
                    EditProductionCompany(selectedItem as ProductionCompanies);
                    break;
                case "Films":
                    EditFilm(selectedItem as Films);
                    break;
            }
        }
        private void EditGenre(Genres genre)
        {
            if (genre != null)
            {
                var editGenreWindow = new EditGenreWindow(genre);
                if (editGenreWindow.ShowDialog() == true)
                {
                    _context.SubmitChanges();
                    DataGrid.ItemsSource = _genres;
                    DataGrid.SelectedItem = editGenreWindow.EditedGenre;
                }
            }
            else
            {
                MessageBox.Show("No genre selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EditCountry(Countries country)
        {
            if (country != null)
            {
                var editCountryWindow = new EditCountryWindow(country);
                if (editCountryWindow.ShowDialog() == true)
                {
                    _context.SubmitChanges();
                    DataGrid.ItemsSource = _countries;
                    DataGrid.SelectedItem = editCountryWindow.EditedCountry;
                }
            }
            else
            {
                MessageBox.Show("No country selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EditProductionCompany(ProductionCompanies productionCompany)
        {
            if (productionCompany != null)
            {
                var editProductionCompanyWindow = new EditProductionCompanyWindow(productionCompany);
                if (editProductionCompanyWindow.ShowDialog() == true)
                {
                    _context.SubmitChanges();
                    DataGrid.ItemsSource = _productionCompanies;
                    DataGrid.SelectedItem = editProductionCompanyWindow.EditedProductionCompany;
                }
            }
            else
            {
                MessageBox.Show("No production company selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EditFilm(Films film)
        {
            var selectedFilm = DataGrid.SelectedItem as Films;
            if (selectedFilm != null)
            {
                var editFilmWindow = new EditFilmWindow(_context, selectedFilm);
                if (editFilmWindow.ShowDialog() == true)
                {
                    _context.SubmitChanges();
                    LoadFilms();
                }
            }
            else
            {
                MessageBox.Show("No film selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGrid.SelectedItem;

            switch (TableComboBox.SelectedItem.ToString())
            {
                case "Genres":
                    DeleteGenre(selectedItem as Genres);
                    break;
                case "Countries":
                    DeleteCountry(selectedItem as Countries);
                    break;
                case "ProductionCompanies":
                    DeleteProductionCompany(selectedItem as ProductionCompanies);
                    break;
                case "Films":
                    DeleteFilm(selectedItem as Films);
                    break;
            }
        }

        private void DeleteGenre(Genres genre)
        {
            if (genre != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete this genre?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Genres.DeleteOnSubmit(genre);
                        _context.SubmitChanges();
                        _genres.Remove(genre);
                        
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547)
                        {
                            MessageBox.Show("Cannot delete this genre because there are films that reference it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No genre selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteCountry(Countries country)
        {
            if (country != null)
            {
                try
                {
                    _context.Countries.DeleteOnSubmit(country);
                    _context.SubmitChanges();
                    _countries.Remove(country);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Cannot delete this country because there are films that reference it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                MessageBox.Show("No country selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteProductionCompany(ProductionCompanies productionCompany)
        {
            if (productionCompany != null)
            {
                try
                {
                    _context.ProductionCompanies.DeleteOnSubmit(productionCompany);
                    _context.SubmitChanges();
                    _productionCompanies.Remove(productionCompany);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Cannot delete this production company because there are films that reference it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                MessageBox.Show("No production company selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteFilm(Films film)
        {
            if (film != null)
            {
                _context.Films.DeleteOnSubmit(film);
                _context.SubmitChanges();
                _films.Remove(film);
            }
            else
            {
                MessageBox.Show("No film selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
