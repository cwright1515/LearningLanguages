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
    /// Interaction logic for ReviewView.xaml
    /// </summary>
    public partial class ReviewView : UserControl
    {
        public int Size;

        public List<ReviewCard> ReviewCards;

        private Action FinishSession;
        public ReviewView(List<Word> allWords, List<Word> currentSessionWords, string language, Action finishSession)
        {
            InitializeComponent();
            FinishSession = finishSession;
            ReviewCards = new List<ReviewCard>();

            foreach(Word word in currentSessionWords)
            {
                ReviewCard rc = new ReviewCard(word.MostRecentAttempt, language, word.ForeignWord, word.EnglishTranslation, word.MostRecentAnswer);
                ReviewCards.Add(rc);
            }

            CardGrid.ItemsSource = ReviewCards;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            FinishSession();
        }
    }
}
