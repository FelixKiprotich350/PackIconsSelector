using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace PackIconsSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PackIcon pik = new PackIcon();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                string filter = Textbox_Search.Text.Trim();
                ICollectionView cv = CollectionViewSource.GetDefaultView(IconsListView.ItemsSource);
                if (filter != "")
                {
                    cv.Filter = o =>
                    {
                        MyPackIconKind p = o as MyPackIconKind;
                        return p.IconName.ToLower().Contains(filter.ToLower());
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message Box", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox t = (TextBox)sender;
                string filter = t.Text.Trim();
                ICollectionView cv = CollectionViewSource.GetDefaultView(IconsListView.ItemsSource);
                if (filter != "")
                {
                    cv.Filter = o =>
                    {
                        MyPackIconKind p = o as MyPackIconKind;
                        return p.IconName.ToLower().Contains(filter.ToLower());
                    };

                }
                else
                {
                    cv.Filter = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message Box", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<PackIconKind> a = Enum.GetValues(typeof(PackIconKind)).Cast<PackIconKind>().ToList();
                //List<string> b = a.ConvertAll<string>(c => c.ToString());
                List<MyPackIconKind> b = a.ConvertAll<MyPackIconKind>(c => new MyPackIconKind() { IconName = c.ToString() }).GroupBy(f=>f.IconName).Select(g=>g.First()).ToList();
                ObservableCollection<MyPackIconKind> d = new ObservableCollection<MyPackIconKind>(b);
                IconsListView.ItemsSource = d;
                Textbox_Total.Text = d.Count.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message Box", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class MyPackIconKind
    {
       public string IconName { get; set; }
    }
}
