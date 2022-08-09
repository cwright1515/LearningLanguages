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
    /// Interaction logic for ReviewCard.xaml
    /// </summary>
    public partial class ReviewCard : UserControl
    {
        public ReviewCard(bool correct, string language, string fword, string eword, string answer)
        {
            InitializeComponent();
            setImage(correct);
            ForeginLbl.Content = $"{language} word: {fword}";
            EnglishLbl.Content = $"English translation: {eword}";
            AnswerLbl.Content = $"Your answer: {answer}";
        }

        private void setImage(bool correct)
        {
            if (correct)
            {
                OutcomeImage.Source = new BitmapImage(new Uri(@"../Resources/Correct.png", UriKind.RelativeOrAbsolute)); 
            }
            else
            {
                OutcomeImage.Source = new BitmapImage(new Uri(@"../Resources/Incorrect.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
