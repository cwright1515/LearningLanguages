using LearnFrench1000.Scrapers;
using LearnFrench1000.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearnFrench1000.Models
{
    [Serializable]
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

        private List<Word> currentSession;

        public List<Word> CurrentSession
        {
            get { return currentSession; }
            set { currentSession = value; }
        } 

        public MainController()
        {
            CurrentView = new ChooseALanguage(changeFrenchView, changeSpanishView, changeGermanView);
        }

        private void setUpLanguages()
        {
            Languages = Scraper.scrape(getLanguagesDict());
        }

        private Dictionary<string, string> getLanguagesDict()
        {
            Dictionary<string, string> langsDict = new Dictionary<string, string>();
            langsDict.Add("French", "https://1000mostcommonwords.com/1000-most-common-french-words/");
            langsDict.Add("Spanish", "https://1000mostcommonwords.com/1000-most-common-spanish-words/");
            langsDict.Add("German", "https://1000mostcommonwords.com/1000-most-common-german-words/");

            return langsDict;
        }

        private void chooseLanguage()
        {
            CurrentView = new ChooseALanguage(changeFrenchView, changeSpanishView, changeGermanView) ;
        }


        private void startSession()
        {
            List<LearningWindow2> windows = new List<LearningWindow2>();
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();

            CurrentSession = new List<Word>(); 

            for (int i = 0; i < 10; i++)
            {
                int wordNumber = GetRandomNumber(0, 999);
                
                Word sessionWord = currentLanguage.Words[wordNumber];

                CurrentSession.Add(sessionWord);
                windows.Add(new LearningWindow2(ref sessionWord, currentLanguage.Name));
            }

            CurrentView = new LearningWindow(windows, ReviewView, FinishSession); 
        }

        private void FinishSession()
        {
            // add switch case statement so depending on the language you call a different function
            switch (CurrentLanguage)
            {
                case "French":
                    changeFrenchView();
                    break;
                case "Spanish":
                    changeSpanishView();
                    break;
                case "German":
                    changeGermanView();
                    break;
            }
        }

        private void ReviewView()
        {
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();
            CurrentView = new ReviewView(currentLanguage.Words, CurrentSession, CurrentLanguage, FinishSession);
        }

        private void changeFrenchView()
        {
            CurrentLanguage = "French";
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();
            CurrentView = new SetUpSession(startSession, currentLanguage, chooseLanguage);
        }

        private void changeSpanishView()
        {
            CurrentLanguage = "Spanish";
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();
            CurrentView = new SetUpSession(startSession, currentLanguage, chooseLanguage);
        }

        private void changeGermanView()
        {
            CurrentLanguage = "German";
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();
            CurrentView = new SetUpSession(startSession, currentLanguage, chooseLanguage);
        }


        // Save 
        // When exit, save current main controller to a folder within the project 
        public void Serialize()
        {
            string saveFilePath = "../../SaveFolder/";
            try
            {
                this.CurrentView = null;
                Stream s = File.Open(saveFilePath + "SaveData.dat", FileMode.Create, FileAccess.Write, FileShare.Read);

                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(s, this);

                s.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // Load 
        // When opened, check the save location for a .dat file - if present, load that as the controller ( and don't scrape !) and if not then make a new controller as usual
        public MainController Load()
        {
            string saveFilePath = "../../SaveFolder/";
            try
            {
                string[] fileEntries = Directory.GetFiles(saveFilePath);
                if (fileEntries.Contains("../../SaveFolder/SaveData.dat"))
                {
                    string loadFilePath = saveFilePath + "SaveData.dat";
                    Stream s = new FileStream(loadFilePath, FileMode.Open, FileAccess.Read);
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    MainController loadedController = (MainController)binaryFormatter.Deserialize(s);
                    loadedController.CurrentView = new ChooseALanguage(loadedController.changeFrenchView, loadedController.changeSpanishView, loadedController.changeGermanView);
                    return loadedController;
                }
                else
                {
                    MainController mc = new MainController();
                    mc.setUpLanguages();
                    return mc;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
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
