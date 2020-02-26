using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ITQuiz.Classes;
using Microsoft.AspNetCore.Http;

namespace ITQuiz.Pages
{
    public class ResultModel : PageModel
    {
        [BindProperty]
        public int Score {get; set; }
        public void OnGet()
        {
            Score = AllConstants.Scores[HttpContext.Session.GetString("TeamName")];
        }
    }
}
