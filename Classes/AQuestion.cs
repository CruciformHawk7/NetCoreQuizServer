using System.Security.Cryptography;
using System.Linq;
using System;

namespace ITQuiz.Classes {
    public class AQuestion {
        public string Ques { get; private set; }
        public string RightOption { get; private set; }
        public string Option2 { get; private set; }
        public string Option3 { get; private set; }
        public string Option4 { get; private set; }

        internal AQuestion(string question, string rightOption, string option2, string option3, string option4) {
            Ques = question;
            RightOption = rightOption;
            Option2 = option2;
            Option3 = option3;
            Option4 = option4;
        }

        internal AQuestion() {
            Ques = RightOption = Option2 = Option3 = Option4 = "";
        }

        internal bool isRight(string answer) {
            return (answer == RightOption);
        }

        internal string[] getOptions() {
            string[] opt = { this.RightOption, this.Option2, this.Option3, this.Option4 };
            RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();
            return opt.OrderBy(_ => GetNextInt32(rnd)).ToArray();

        }

        internal static int GetNextInt32(RNGCryptoServiceProvider rnd) {
            byte[] randomInt = new byte[4];
            rnd.GetBytes(randomInt);
            return Convert.ToInt32(randomInt[0]);
        }
    }
}