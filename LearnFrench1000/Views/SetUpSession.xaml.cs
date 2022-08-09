using LearnFrench1000.Models;
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
        List<ProgressCard> ProgressCards;
        
        public SetUpSession(Action startLearning, List<Word> words)
        {
            InitializeComponent();
            StartLearning = startLearning;

            // Setup progress cards
            ProgressCards = new List<ProgressCard>();
            foreach(Word word in words)
            {
                ProgressCard pc = new ProgressCard(word);
                ProgressCards.Add(pc);
            }
            ProgressList.ItemsSource = ProgressCards;
        }

        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            StartLearning();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            ProgressColumn.Width = new System.Windows.GridLength(200);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            ProgressColumn.Width = new System.Windows.GridLength(80);
        }
    }
}
