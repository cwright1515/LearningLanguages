using LearnFrench1000.Models;
using LearnFrench1000.Scrapers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace LearnFrench1000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainController mainController;
        public MainWindow()
        {
            InitializeComponent();
            mainController = new MainController();
            mainController = mainController.Load();
            CurrentView.DataContext = mainController;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainController.Serialize();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
