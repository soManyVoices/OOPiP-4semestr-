using System;
using System.Windows;
using System.Windows.Controls;

namespace ArrayListDemo
{
    public partial class MainWindow : Window
    {
        private CustomCollections.ArrayList<object> arrayList;
        private object[] copiedArray;

        public MainWindow()
        {
            InitializeComponent();
            arrayList = new CustomCollections.ArrayList<object>();
  
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            object value = InputTextBox.Text;
            arrayList.Add(value);
            UpdateListBox();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            object value = InputTextBox.Text;
            if (arrayList.Remove(value))
            {
                UpdateListBox();
            }
            else
            {
                MessageBox.Show("Item not found in the list.");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            arrayList.Clear();
            UpdateListBox();
        }

        private void ContainsButton_Click(object sender, RoutedEventArgs e)
        {
            object value = InputTextBox.Text;
            bool contains = arrayList.Contains(value);
            MessageBox.Show(contains ? "Item is in the list." : "Item is not in the list.");
        }

        private void CopyToButton_Click(object sender, RoutedEventArgs e)
        {
            copiedArray = new object[arrayList.Count];
            arrayList.CopyTo(copiedArray, 0);
            MessageBox.Show("Array copied successfully.");
        }

        private void ShowCopiedArrayButton_Click(object sender, RoutedEventArgs e)
        {
            if (copiedArray != null)
            {
                string output = "Copied Array:\n";
                foreach (var item in copiedArray)
                {
                    output += item.ToString() + "\n";
                }
                MessageBox.Show(output);
            }
            else
            {
                MessageBox.Show("No array has been copied yet.");
            }
        }
        private void UpdateListBox()
        {
            OutputListBox.Items.Clear();
            foreach (var item in arrayList)
            {
                OutputListBox.Items.Add(item);
            }
            CountLabel.Content = $"Count: {arrayList.Count}";
            CapacityLabel.Content = $"Capacity: {arrayList.Capacity}";
        }
    }
}