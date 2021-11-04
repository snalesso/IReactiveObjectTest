using System.Windows;

namespace IReactiveObjectTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            this.InitializeComponent();
        }
    }
}
