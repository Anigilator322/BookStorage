using BookStorage.Core.Models;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStorageApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LibraryApiClient _client = new LibraryApiClient("http://localhost:5002");

        public MainWindow() => InitializeComponent();

        private async void OnSearchClick(object sender, RoutedEventArgs e)
        {
            var books = await _client.SearchBooksAsync(SearchBox.Text);
            BooksList.ItemsSource = books;
        }

        private async void OnDownloadClick(object sender, RoutedEventArgs e)
        {
            if (BooksList.SelectedItem is Book book)
            {
                var data = await _client.DownloadBookAsync(book.Id);

                var dialog = new SaveFileDialog
                {
                    FileName = $"{book.Title}.pdf",
                    Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
                };

                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllBytes(dialog.FileName, data);
                    MessageBox.Show($"Файл сохранён: {dialog.FileName}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksList.SelectedItem is Book book)
            {
                Title = $"{book.Title} — {book.Author}";
            }
        }

    }
}