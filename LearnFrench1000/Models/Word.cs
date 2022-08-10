using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFrench1000.Models
{
    [Serializable]
    public class Word
    {
        #region Attributes

        private string foreignWord;

        public string ForeignWord
        {
            get { return foreignWord; }
            set { foreignWord = value; }
        }

        private string englishTranslation;

        public string EnglishTranslation
        {
            get { return englishTranslation; }
            set { englishTranslation = value; }
        }

        private int timesSeen;

        public int TimesSeen
        {
            get { return timesSeen; }
            set { timesSeen = value; }
        }

        private int timesCorrect;

        public int TimesCorrect
        {
            get { return timesCorrect; }
            set { timesCorrect = value; }
        }

        private double percentageCorrect;

        public double PercentageCorrect
        {
            get 
            {

                double _correct = TimesSeen > 0 ? TimesCorrect / TimesSeen * 100 : 0;
                return _correct; 
            }
            set { percentageCorrect = value; }
        }

        private bool mostRecentAttempt;

        public bool MostRecentAttempt
        {
            get { return mostRecentAttempt; }
            set { mostRecentAttempt = value; }
        }

        private string mostRecentAnswer;

        public string MostRecentAnswer
        {
            get { return mostRecentAnswer; }
            set { mostRecentAnswer = value; }
        }

        private List<string> synonyms;

        public List<string> Synonyms
        {
            get { return synonyms; }
            set { synonyms = value; }
        }




        #endregion
        public Word(string foreignWord, string englishTranslation, List<string> synonyms)
        {
            ForeignWord = foreignWord;
            EnglishTranslation = englishTranslation;
            TimesSeen = 0;
            TimesCorrect = 0;
            MostRecentAttempt = false ;
            MostRecentAnswer = "";
            Synonyms = synonyms;
        }

    }
}
