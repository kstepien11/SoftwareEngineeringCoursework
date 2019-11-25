using System.Windows;
using System.Windows.Controls;
using coursework.ViewModels;

namespace coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string textDisplay)
        {

            InitializeComponent();
            this.TB.Text = textDisplay;
            
        }

    }
}
