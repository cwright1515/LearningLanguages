using LearnFrench1000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Action ChooseALanguage;
        
        public SetUpSession(Action startLearning, Language currentLanguage, Action chooseALanguage)
        {
            // Set up actions
            StartLearning = startLearning;
            ChooseALanguage = chooseALanguage;

            InitializeComponent();

            // Handling virtualisation ? 
            ProgressList.Visibility = Visibility.Collapsed;
            ProgressList.SetValue(VirtualizingStackPanel.VirtualizationModeProperty, VirtualizationMode.Recycling);
            ProgressList.Visibility = Visibility.Visible;

            

            // Setup progress cards
            ProgressCards = new List<ProgressCard>();
            foreach(Word word in currentLanguage.Words)
            {
                ProgressCard pc = new ProgressCard(word);
                ProgressCards.Add(pc);
            }
            ProgressList.ItemsSource = ProgressCards;

            // Set up UI
            PracticeLbl.Content = $"Practice {currentLanguage.Name}";
            DescriptTxt.Text = $"Welcome! \n\nThis app has a collection of the top 1000 most commonly used words in the {currentLanguage.Name} language. \n\nClick go to start practising them! You will see 10 words to translate, then can review your progress at the end! ";
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

        private void OrderByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string orderBY = Regex.Match(OrderByCombo.SelectedItem.ToString(), @"\: (.*)").Groups[1].Value;
            switch (orderBY)
            {
                case "Times seen asc":
                    ProgressCards =  ProgressCards.OrderBy(o => o.Word.TimesSeen).ToList();
                    break;
                case "Times seen desc":
                    ProgressCards = ProgressCards.OrderByDescending(o => o.Word.TimesSeen).ToList();
                    break;
                case "Progress asc":
                    ProgressCards = ProgressCards.OrderBy(o => o.Word.PercentageCorrect).ToList();
                    break;
                case "Progress desc":
                    ProgressCards = ProgressCards.OrderByDescending(o => o.Word.PercentageCorrect).ToList();
                    break;
                default:
                    break;
            }
            ProgressList.ItemsSource = ProgressCards;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            ChooseALanguage();
        }
    }
}
