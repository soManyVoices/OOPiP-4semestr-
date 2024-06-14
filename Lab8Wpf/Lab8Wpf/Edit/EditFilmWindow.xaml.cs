using System;
using System.Runtime.Remoting.Contexts;
using System.Windows;

namespace Lab8Wpf
{
    public partial class EditFilmWindow : Window
    {
        public Films EditedFilm { get; private set; }
        private MediaLibraryDataContext _context; // добавлено поле для хранения экземпляра MediaLibraryDataContext


        public EditFilmWindow(MediaLibraryDataContext context, Films film) // изменен конструктор, добавлены параметры для передачи экземпляра MediaLibraryDataContext и фильма
        {
            InitializeComponent();

            _context = context; // присвоено значение полю _context

            EditedFilm = film;
            TitleTextBox.Text = film.Title;
            DurationTextBox.Text = film.Duration.ToString();
            ReleaseDateDatePicker.SelectedDate = film.ReleaseDate;
            CountryComboBox.SelectedItem = film.CountryID;
            GenreComboBox.SelectedItem = film.GenreID;

            // Заполните ComboBox данными из таблиц "Countries" и "Genres"
            foreach (var country in _context.Countries)
            {
                CountryComboBox.Items.Add(country);
            }

            foreach (var genre in _context.Genres)
            {
                GenreComboBox.Items.Add(genre);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TitleTextBox.Text) && !string.IsNullOrWhiteSpace(DurationTextBox.Text) &&
                CountryComboBox.SelectedItem != null && GenreComboBox.SelectedItem != null)
            {
                int duration;
                if (int.TryParse(DurationTextBox.Text, out duration))
                {
                    EditedFilm.Title = TitleTextBox.Text;
                    EditedFilm.Duration = duration;
                    EditedFilm.ReleaseDate = ReleaseDateDatePicker.SelectedDate.Value;
                    EditedFilm.CountryID = ((Countries)CountryComboBox.SelectedItem).CountryID; // изменено присваивание значения свойству CountryID
                    EditedFilm.GenreID = ((Genres)GenreComboBox.SelectedItem).GenreID; // изменено присваивание значения свойству GenreID

                    // обновление записи в таблице "Films"
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
