using LaunchDeck.Converters;
using LaunchDeck.ViewModel;
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

namespace LaunchDeck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool BooleanValue { get; set; } = false;
        public MainWindow()
        {
            //TopRightCornerPositionConverter has to be initialized from code behind because the component initalization is using the converter for window positioning

             DataContext = this; //Handled in xaml in window.datacontext
            TopRightCornerPositionConverter topRightCornerPositionConverter = new TopRightCornerPositionConverter();
            Resources.Add("topRightCornerPositionConverter", topRightCornerPositionConverter);
            InvertBooleanConverter invertBooleanConverter = new InvertBooleanConverter();
            Resources.Add("invertBooleanConverter", invertBooleanConverter);
            InitializeComponent();
            
            


        }

        private void Shutdown(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}