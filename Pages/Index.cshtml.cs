using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ITQuiz.Classes;

namespace ITQuiz.Pages
{
    public class IndexModel : PageModel {
        [BindProperty]
        public string Team { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger) {
            _logger = logger;
        }

        public void OnPost() {
            var Team = Request.Form["TeamName"].ToString();
            HttpContext.Session.SetString("QuestionNumber", "1");
            HttpContext.Session.SetString("TeamName", Team);
            HttpContext.Session.SetString("Points", "0");

            //Preparing session
            AllConstants.AddSession(Team);

            Response.Redirect("/Quiz");
        }
    }
}
