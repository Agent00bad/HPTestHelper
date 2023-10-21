using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Data
{
    public class QuarterTest
    {
        public int TestId { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public override string ToString()
        {
            string returnString = $"ProvPass{TestId}";
            foreach (Answer answer in Answers)
            {
                returnString += $"\nQuestion {answer.AnswerId}: {answer.AnswerChar}";
            }
            return returnString;
        }
    }
}
