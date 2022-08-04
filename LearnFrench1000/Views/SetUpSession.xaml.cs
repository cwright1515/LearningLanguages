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

namespace LearnFrench1000.Views
{
    /// <summary>
    /// Interaction logic for SetUpSession.xaml
    /// </summary>
    public partial class SetUpSession : UserControl
    {
        Action StartLearning;
        
        public SetUpSession(Action startLearning)
        {
            InitializeComponent();
            StartLearning = startLearning;
        }

        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            StartLearning();
        }
    }
}
