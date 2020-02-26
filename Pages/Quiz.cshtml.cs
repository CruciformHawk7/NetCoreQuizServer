using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ITQuiz.Classes;

namespace ITQuiz.Pages {
    public class Quiz : PageModel {
        
        int questionNumber;
        string CollegeID;

        //Dictionary<string, string> Result = new Dictionary<string, string>();

        [BindProperty] public string Question { get; set; }
        [BindProperty] public string Option1 { get; set; }
        [BindProperty] public string Option2 { get; set; }
        [BindProperty] public string Option3 { get; set; }
        [BindProperty] public string Option4 { get; set; }


        public void OnGet() {
            questionNumber = Convert.ToInt32(HttpContext.Session.GetString("QuestionNumber"));
            CollegeID = HttpContext.Session.GetString("TeamName");
            Question = AllConstants.sets[CollegeID][questionNumber-1].Ques;
            var opt = AllConstants.sets[CollegeID][questionNumber-1].getOptions();
            Option1 = opt[0];
            Option2 = opt[1];
            Option3 = opt[2];
            Option4 = opt[3];
        }

        public void OnPostOption1() {
            questionNumber = Convert.ToInt32(HttpContext.Session.GetString("QuestionNumber"));
            CollegeID = HttpContext.Session.GetString("TeamName");
            if (AllConstants.sets[CollegeID][questionNumber-1].isRight(Option1)) 
                AllConstants.Scores[CollegeID]++;
            //Result.Add(Question, Option1);
            questionNumber++;
            HttpContext.Session.SetString("QuestionNumber", questionNumber.ToString());
            if (isDone(questionNumber)) Response.Redirect("/Result");
            else Response.Redirect("/Quiz");
        }

        public void OnPostOption2() {
            questionNumber = Convert.ToInt32(HttpContext.Session.GetString("QuestionNumber"));
            CollegeID = HttpContext.Session.GetString("TeamName");
            if (AllConstants.sets[CollegeID][questionNumber-1].isRight(Option2)) 
                AllConstants.Scores[CollegeID]++;
            //Result.Add(Question, Option2);
            questionNumber++;
            HttpContext.Session.SetString("QuestionNumber", questionNumber.ToString());
            if (isDone(questionNumber)) Response.Redirect("/Result");
            else Response.Redirect("/Quiz");
        }

        public void OnPostOption3() {
            questionNumber = Convert.ToInt32(HttpContext.Session.GetString("QuestionNumber"));
            CollegeID = HttpContext.Session.GetString("TeamName");
            if (AllConstants.sets[CollegeID][questionNumber-1].isRight(Option3)) 
                AllConstants.Scores[CollegeID]++;
            // Result.Add(Question, Option3);
            questionNumber++;
            HttpContext.Session.SetString("QuestionNumber", questionNumber.ToString());
            if (isDone(questionNumber)) Response.Redirect("/Result");
            else Response.Redirect("/Quiz");
        }

        public void OnPostOption4() {
            questionNumber = Convert.ToInt32(HttpContext.Session.GetString("QuestionNumber"));
            CollegeID = HttpContext.Session.GetString("TeamName");
            if (AllConstants.sets[CollegeID][questionNumber-1].isRight(Option4)) 
                AllConstants.Scores[CollegeID]++;
            // Result.Add(Question, Option4);
            questionNumber++;
            HttpContext.Session.SetString("QuestionNumber", questionNumber.ToString());
            if (isDone(questionNumber)) Response.Redirect("/Result");
            else Response.Redirect("/Quiz");
        }

        private bool isDone(int questionNumber) {
            if (questionNumber > AllConstants.size) {
                AllConstants.flushToDB(HttpContext.Session.GetString("TeamName"));
                return true;
            }
            return false;
        }
    }
}