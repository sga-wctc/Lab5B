using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab5B
{
    public partial class MainPage : ContentPage
    {
        int CurrentQuestion;
        string[] QuestionList = { "Do you prefer running or walking?", "How do you prefer to travel?",
            "Which activity do you prefer?", "Which vehicle would you rather drive?", 
            "Which sport would you rather watch?" };
        string[] OptionAList = { "running", "airplane", "bike riding", "car", "football" };
        string[] OptionBList = { "walking", "train", "fishing", "truck", "baseball" };
        List<string> ResponseList = new List<string>{ };

        public MainPage()
        {
            InitializeComponent();
            InitSurvey();
        }

       private void OnButtonClicked_OptionA(object sender, EventArgs e)
        {
            //await DisplayAlert("Alert", "Option A", "OK");
            SaveAnswerAsync("A");
        }
        private void OnButtonClicked_OptionB(object sender, EventArgs e)
        {
            //await DisplayAlert("Alert", "Option B", "OK");
            SaveAnswerAsync("B");
        }
        private void OnButtonClicked_Restart(object sender, EventArgs e)
        {
            InitSurvey();
        }

        private void InitSurvey()
        {
            CurrentQuestion = 0;
            SetNewQuestion();
            SurveyOptionA.IsVisible = true;
            SurveyOptionB.IsVisible = true;
            SurveyOptionA.IsEnabled = true;
            SurveyOptionB.IsEnabled = true;
            SurveyRestart.IsVisible = false;
            SurveyRestart.IsEnabled = false;
            SurveyResult.IsVisible = false;
            SurveyQuestion.IsVisible = true;
            ResponseList = new List<string> { };
        }

        private void SaveAnswerAsync(string answer )
        {
            ResponseList.Add(answer);
            CurrentQuestion = ResponseList.Count;
            if (CurrentQuestion >= QuestionList.Length)
            {
                string yourResult = "";
                int A_count = 0;
                int B_count = 0;
                for (int questioncount = 0; questioncount < QuestionList.Length; questioncount++)
                {
                    yourResult += $"Q{(questioncount+1)}: {QuestionList[questioncount]} \n A{(questioncount + 1)}: ";
                    switch (ResponseList[questioncount])
                    {
                        case "A":
                            yourResult += $"{OptionAList[questioncount]}\n";
                            A_count++;
                            break;
                        default:
                            yourResult += $"{OptionBList[questioncount]}\n";
                            B_count++;
                            break;
                    }
                }
                SurveyOptionA.IsVisible = false;
                SurveyOptionB.IsVisible = false;
                SurveyOptionA.IsEnabled = false;
                SurveyOptionB.IsEnabled = false;
                SurveyResult.IsVisible = true;
                SurveyQuestion.IsVisible = false;
                // Build personality result.
                if (A_count == QuestionList.Length)
                {
                    yourResult += $"\n You enjoy things which are fast, and probably have a short attention span\n";
                }
                else if (B_count == QuestionList.Length)
                {
                    yourResult += $"\n You enjoy things which are slower, and probably send a lot of time in thought\n";
                }
                else if (A_count >= B_count)
                {
                    yourResult += $"\n You enjoy things which of different activity levels, but tend toward more thoughtful things\n";
                }
                else
                {
                    yourResult += $"\n You enjoy things which of different activity levels, but tend toward more active things\n";
                }

                SurveyResult.Text = yourResult;

                SurveyRestart.IsVisible = true;
                SurveyRestart.IsEnabled = true;

            } else
            {
                SetNewQuestion();
            }
        }
        private void SetNewQuestion() {
            SurveyQuestion.Text = QuestionList[CurrentQuestion];

            SurveyOptionA.Text = OptionAList[CurrentQuestion];
            SurveyOptionA.ImageSource = ImageSource.FromFile("Lab5B.images.SeatedMonkey.jpg");
            SurveyOptionB.Text = OptionBList[CurrentQuestion];
        }

    }
}
