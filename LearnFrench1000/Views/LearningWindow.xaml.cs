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

        private double progressBarValue;

        public double ProgressBarValue
        {
            get { return progressBarValue; }
            set { progressBarValue = value; }
        }


        public LearningWindow(List<LearningWindow2> learningWindows, Action reviewView)
        {
            InitializeComponent();
            ReviewView = reviewView;

            LearningWindows = learningWindows;
            LearningWindowCounter = 0;
            LearningWindowControl.Content = LearningWindows[LearningWindowCounter];

            ProgressLbl.Content = $"Progress: {LearningWindowCounter}/10 ";
            ProgressBarValue = 0;
        }
                   
        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            LearningWindow2 currentLW = LearningWindows[LearningWindowCounter];
            // MessageBox.Show(currentLW.FWordLbl.Content.ToString() + "\n Answer: " +  currentLW.EWord + "\n Your answer: " + currentLW.Answer);
            if(currentLW.EWord.Trim( ) == currentLW.Answer.Trim())
            {
                currentLW.AnswerTxt.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                CorrectAnswerTxt.Content = currentLW.EWord;
                CorrectAnswerTxt.Visibility = Visibility.Visible;
                currentLW.AnswerTxt.Background = new SolidColorBrush(Colors.Red);
            }

            ContinueBtn.IsEnabled = true;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LearningWindowCounter < 9)
            {
                CorrectAnswerTxt.Visibility = Visibility.Hidden;
                ContinueBtn.IsEnabled = false;
                LearningWindowCounter++;
                LearningWindowControl.Content = LearningWindows[LearningWindowCounter];

                ProgressBar.Value = (double)LearningWindowCounter / (double)10 * (double)100;
                ProgressLbl.Content = $"Progress: {LearningWindowCounter}/10 ";
            }
            else
            {
                ReviewView();
            }
        }
    }
}
