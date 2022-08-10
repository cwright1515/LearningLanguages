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
        List<LearningWindow2> LearningWindows;
        int LearningWindowCounter;
        Action ReviewView;
        Action FinishSession; 

        private double progressBarValue;

        public double ProgressBarValue
        {
            get { return progressBarValue; }
            set { progressBarValue = value; }
        }

        public LearningWindow(List<LearningWindow2> learningWindows, Action reviewView, Action finishSession)
        {
            InitializeComponent();
            ReviewView = reviewView;
            FinishSession = finishSession;

            LearningWindows = learningWindows;
            LearningWindowCounter = 0;
            LearningWindowControl.Content = LearningWindows[LearningWindowCounter];
               
            ProgressLbl.Content = $"Progress: {LearningWindowCounter}/9 ";
            ProgressBarValue = 0;
        }


                   
        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {

            LearningWindow2 currentLW = LearningWindows[LearningWindowCounter];
            CheckBtn.Focus();

            // MessageBox.Show(currentLW.FWordLbl.Content.ToString() + "\n Answer: " +  currentLW.EWord + "\n Your answer: " + currentLW.Answer);
            if(currentLW.EWord.Trim() == currentLW.Answer.Trim() || currentLW.Word.Synonyms.Contains(currentLW.Answer.Trim()) )
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

            
            ContinueBtn.IsEnabled = true;
            ContinueBtn.IsDefault = true;
            CheckBtn.IsDefault = false;
            ContinueBtn.Focus();
        }



        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LearningWindowCounter < 9)
            {
                CorrectAnswerTxt.Visibility = Visibility.Hidden;
                ContinueBtn.IsEnabled = false;
                ContinueBtn.IsDefault = false;
                CheckBtn.IsDefault = true;

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

        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        {
            FinishSession();
        }
    }
}
