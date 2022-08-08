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
    /// Interaction logic for LearningWindow2.xaml
    /// </summary>
    public partial class LearningWindow2 : UserControl
    {
        private string fWord;

        public string FWord
        {
            get { return fWord; }
            set { fWord = value; }
        }

        private string eWord;
        public string EWord
        {
            get { return eWord; }
            set { eWord = value; }
        }

        private string answer;

        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        private Word word;

        public Word Word
        {
            get { return word; }
            set { word = value; }
        }


        public LearningWindow2(ref Word word)
        {
            InitializeComponent();
            AnswerTxt.DataContext = this;
            FWord = word.ForeignWord;
            EWord = word.EnglishTranslation;
            FWordLbl.Content = FWord;
            Word = word;
            //EWordLbl.Content = englishTranslation;
        }

    }
}
