using LearnFrench1000.Scrapers;
using LearnFrench1000.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFrench1000.Models
{
    public class MainController : ObservableObject
    {
        private List<Language> languages;

        public List<Language> Languages
        {
            get { return languages; }
            set { languages = value; }
        }

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged();  }
        }

        private string currentLanguage;

        public string CurrentLanguage
        {
            get { return currentLanguage; }
            set { currentLanguage = value; }
        }


        public MainController()
        {
            setUpLanguages();
            CurrentView = new ChooseALanguage(changeFrenchView);
        }

        private void setUpLanguages()
        {
            List<Word> words = Scraper.scrape();
            Language French = new Language("French", words);
            Languages = new List<Language>() { French };
        }

        private void changeFrenchView()
        {
            CurrentView = new SetUpSession(startSession);
            CurrentLanguage = "French";
        }

        private void startSession()
        {
            List<LearningWindow2> windows = new List<LearningWindow2>();
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();
            for (int i = 0; i < 10; i++)
            {
                int wordNumber = GetRandomNumber(0, 999);

                string fword = currentLanguage.Words[wordNumber].ForeignWord;
                string eword = currentLanguage.Words[wordNumber].EnglishTranslation;
                
                windows.Add(new LearningWindow2(fword, eword));
            }

            CurrentView = new LearningWindow(windows, ReviewView);

        }

        private void ReviewView()
        {
            CurrentView = new ReviewView();
        }

        #region Random number stuff
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }
        #endregion 
    }
}
