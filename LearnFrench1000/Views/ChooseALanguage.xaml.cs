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
    /// Interaction logic for ChooseALanguage.xaml
    /// </summary>
    public partial class ChooseALanguage : UserControl
    {
        Action French;
        Action Spanish;
        Action German;
        

        public ChooseALanguage(Action french, Action spanish, Action german)
        {
            InitializeComponent();
            French = french;
            Spanish = spanish;
            German = german;
        }

        private void FrenchBtn_Click(object sender, RoutedEventArgs e)
        {
            French();
        }

        private void SpanishBtn_Click(object sender, RoutedEventArgs e)
        {
            Spanish();
        }

        private void GermanBtn_Click(object sender, RoutedEventArgs e)
        {
            German();
        }
    }
}
