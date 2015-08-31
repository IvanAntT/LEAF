using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomen_proekt
{
    public class GrammarQuestion
    {
        public string Question { get; set; }
        public string[] Answers { get; set; }
        public string CurrectAnswer { get; set; }

        public GrammarQuestion(string question, string[] answers, string currectAnswer)
        {
            this.Question = question;
            this.Answers = answers;
            this.CurrectAnswer = currectAnswer;
        }
    }
}
