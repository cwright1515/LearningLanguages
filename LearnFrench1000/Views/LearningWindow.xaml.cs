using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for LearningWindow.xaml
    /// </summary>
    public partial class LearningWindow : UserControl
    {
        #region Attributes and actions

        private List<LearningWindow2> LearningWindows;
        private int LearningWindowCounter;
        private Action ReviewView;
        private Action FinishSession; 

        private double progressBarValue;

        public double ProgressBarValue
        {
            get { return progressBarValue; }
            set { progressBarValue = value; }
        }

        #endregion 

        public LearningWindow(List<LearningWindow2> learningWindows, Action reviewView, Action finishSession)
        {
            InitializeComponent();
            // Set up attributes
            ReviewView = reviewView;
            FinishSession = finishSession;
            LearningWindows = learningWindows;

            // Set up UI content
            LearningWindowCounter = 0;
            LearningWindowControl.Content = LearningWindows[LearningWindowCounter];
            ProgressLbl.Content = $"Progress: {LearningWindowCounter}/9 ";
            ProgressBarValue = 0;
        } 
                   
        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            checkAnswer();            
        } 

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            progress();
        }

        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        {
            FinishSession();
        }



        private void checkAnswer()
        {
            LearningWindow2 currentLW = LearningWindows[LearningWindowCounter]; 
            CheckBtn.Focus(); 

            // Validate answer -- so it doesnt crash if user doesnt input an answer
            string answer = currentLW.Answer != null ? currentLW.Answer.Trim() : " ";
            

            if (currentLW.EWord == answer.Trim() || currentLW.Word.Synonyms.Contains(answer))
            {
                currentLW.Word.TimesSeen++;
                currentLW.Word.TimesCorrect++;
                currentLW.Word.MostRecentAttempt = true;
                currentLW.Word.MostRecentAnswer = currentLW.AnswerTxt.Text;
                currentLW.AnswerTxt.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#449091");
            }
            else
            {
                currentLW.Word.TimesSeen++;
                currentLW.Word.MostRecentAttempt = false;
                CorrectAnswerTxt.Content = currentLW.EWord;
                currentLW.Word.MostRecentAnswer = currentLW.AnswerTxt.Text;
                CorrectAnswerTxt.Visibility = Visibility.Visible;
                currentLW.AnswerTxt.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#df7292");
            }

            setButtonFocus(true);
            ContinueBtn.Focus();
        }

        private void progress()
        {
            if (LearningWindowCounter < 9)
            {
                CorrectAnswerTxt.Visibility = Visibility.Hidden;
                setButtonFocus(false);

                LearningWindowCounter++;
                LearningWindowControl.Content = LearningWindows[LearningWindowCounter];

                ProgressBar.Value = (double)LearningWindowCounter / (double)10 * (double)100;
                ProgressLbl.Content = $"Progress: {LearningWindowCounter}/9 ";
                LearningWindows[LearningWindowCounter].AnswerTxt.Focus();
            }
            else
            {
                ReviewView();
            }
        }

        private void setButtonFocus(bool checkOrContinue)
        {
            ContinueBtn.IsEnabled = checkOrContinue;
            ContinueBtn.IsDefault = checkOrContinue;
            CheckBtn.IsDefault = !checkOrContinue;
        }
    }
}
