using System.Windows;

namespace Lab8Wpf
{
    public partial class AddGenreWindow : Window
    {
        public Genres NewGenre { get; private set; }

        public AddGenreWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GenreNameTextBox.Text))
            {
                NewGenre = new Genres { GenreName = GenreNameTextBox.Text };
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
