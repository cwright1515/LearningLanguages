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
            CurrentLanguage = "French";
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();
            CurrentView = new SetUpSession(startSession, currentLanguage.Words);
        }

        private void startSession()
        {
            List<LearningWindow2> windows = new List<LearningWindow2>();
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();

            CurrentSession = new List<Word>(); 

            // remove next 3 lines and change i in loop to 0 for release
            Word firstWord = currentLanguage.Words[0];
            CurrentSession.Add(firstWord);
            windows.Add(new LearningWindow2(ref firstWord));

            for (int i = 1; i < 10; i++)
            {
                int wordNumber = GetRandomNumber(0, 999);
                
                
                Word sessionWord = currentLanguage.Words[wordNumber];

                CurrentSession.Add(sessionWord);
                windows.Add(new LearningWindow2(ref sessionWord));
            }

            CurrentView = new LearningWindow(windows, ReviewView, FinishSession);

        }

        private void FinishSession()
        {
            changeFrenchView();
        }

        private void ReviewView()
        {
            Language currentLanguage = Languages.Where(l => l.Name == CurrentLanguage).FirstOrDefault();
            CurrentView = new ReviewView(currentLanguage.Words, CurrentSession, CurrentLanguage, FinishSession);
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
        // When opened, check the save location for a .dat file - if present, load that as the controller ( and don't scrape !)
        // and if not then make a new controller as usual
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
                    loadedController.CurrentView = new ChooseALanguage(loadedController.changeFrenchView);
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
