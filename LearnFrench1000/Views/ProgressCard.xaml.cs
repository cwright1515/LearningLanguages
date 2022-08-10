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
    /// Interaction logic for ProgressCard.xaml
    /// </summary>
    public partial class ProgressCard : UserControl
    {
        public Word Word;
        public ProgressCard(Word word)
        {
            InitializeComponent();
            Word = word;

            ForeignWordLbl.Content = $"French word : {word.ForeignWord}";
            TranslationLbl.Content = $"Translation : {word.EnglishTranslation}";
            TimesSeenLbl.Content = $"Times seen : {word.TimesSeen}";
            ProgressLbl.Content = $"Progress : {word.PercentageCorrect}";
        }
    }
}
