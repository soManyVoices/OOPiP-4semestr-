using System.Windows;

namespace Lab8Wpf
{
    public partial class EditGenreWindow : Window
    {
        public Genres EditedGenre { get; private set; }

        public EditGenreWindow(Genres genre)
        {
            InitializeComponent();

            EditedGenre = genre;
            GenreNameTextBox.Text = genre.GenreName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GenreNameTextBox.Text))
            {
                EditedGenre.GenreName = GenreNameTextBox.Text;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Genre name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
