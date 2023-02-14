using System;
using System.Collections.Generic;
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

namespace SE1610_MyWPF_DEMO1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void btnToPage01_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new Page_01();
        }
        public void btnToPage02_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new Page_02();
            this.Resources["BackgroudButton2"] = new SolidColorBrush(Colors.Yellow);
            this.Resources["SolidBlack"] = new SolidColorBrush(Colors.Red);
            this.Resources["SolidGray"] = new SolidColorBrush(Colors.Yellow);
        }
    }
}
