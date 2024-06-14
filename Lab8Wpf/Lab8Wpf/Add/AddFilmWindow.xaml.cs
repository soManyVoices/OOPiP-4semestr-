using System;
using System.Runtime.Remoting.Contexts;
using System.Windows;

namespace Lab8Wpf
{
    public partial class AddFilmWindow : Window
    {
        public Films NewFilm { get; private set; }
        private MediaLibraryDataContext _context; // добавлено поле для хранения экземпляра MediaLibraryDataContext

        public AddFilmWindow(MediaLibraryDataContext context) 
        {
            InitializeComponent();

            _context = context; // присвоено значение полю _context

            foreach (var country in _context.Countries)
            {
                CountryComboBox.Items.Add(country);
            }

            foreach (var genre in _context.Genres)
            {
                GenreComboBox.Items.Add(genre);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TitleTextBox.Text) && !string.IsNullOrWhiteSpace(DurationTextBox.Text) &&
            CountryComboBox.SelectedItem != null && GenreComboBox.SelectedItem != null)
            {
                int duration;
                if (int.TryParse(DurationTextBox.Text, out duration))
                {
                    NewFilm = new Films
                    {
                        Title = TitleTextBox.Text,
                        Duration = duration,
                        ReleaseDate = ReleaseDateDatePicker.SelectedDate.Value,
                        CountryID = ((Countries)CountryComboBox.SelectedItem).CountryID, 
                        GenreID = ((Genres)GenreComboBox.SelectedItem).GenreID, 
                    };
                    _context.Films.InsertOnSubmit(NewFilm);
                    _context.SubmitChanges();

                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Invalid duration value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
