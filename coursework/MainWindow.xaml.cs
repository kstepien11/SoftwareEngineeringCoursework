using System.Windows;
using System.Windows.Controls;
using coursework.ViewModels;

namespace coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
            
            object model = new MainWindowViewModel();
            this.DataContext = model;
            
        }

        private void HandleDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = ((ListViewItem)sender).Content;
            if (item != null)
            {
                
                MessageWindow window = new MessageWindow(item.ToString());
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Topmost = true;
                window.Show();

            }
        }
    }
}
