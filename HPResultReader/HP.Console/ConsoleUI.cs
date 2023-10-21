using HP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Console
{
    public class ConsoleUI
    {
        private IEnumerable<QuarterTest> _tests;
        private IEnumerable<Char> _possibleAnswers;
        public ConsoleUI(IEnumerable<QuarterTest> tests,
            IEnumerable<Char> possibleAnswers)
        {
            this._tests = tests;
            this._possibleAnswers = possibleAnswers;
        }
        public QuarterTest ChooseQuarterResult()
        {
            bool runningMenu = true;
            while (runningMenu)
            {
                System.Console.WriteLine("Välj Provpass med dess nummer: ");

                foreach (var test in _tests)
                {
                    System.Console.WriteLine($"\nProvpass{test.TestId}");
                }
                var input = System.Console.ReadLine();
                foreach (var test in _tests)
                {
                    if (input.ToLower() == test.TestId.ToString()) return test;
                    if (input.ToLower() == "q") return new QuarterTest();
                }
                System.Console.WriteLine("Felaktig inmatning, avsluta med input Q");
            }
            return null;
        }
        public bool Run(QuarterTest test)
        {
            System.Console.WriteLine($"Provpass{test.TestId} startad, lycka till");
            var correctAnswers = 0;
            var startTime = DateTime.Now.Minute;
            string input;
            foreach (var answer in test.Answers)
            {
                do
                {
                    System.Console.WriteLine($"Fråga {answer.AnswerId}:");
                
                    input = System.Console.ReadLine().ToLower();

                    if (!_possibleAnswers.Select(c => c.ToString())
                        .Contains(input)) System.Console.WriteLine("felaktig inmatning");
                
                } while (!_possibleAnswers.Select(c => c.ToString().ToLower())
                .Contains(input.ToLower()));

                if (input == answer.AnswerChar.ToString())
                {
                    correctAnswers++;
                    System.Console.WriteLine($"\n{input.ToUpper()} är rätt!");
                }
                else System.Console.WriteLine($"\n{input.ToUpper()} är fel" +
                    $"\nRätt svar är {answer.AnswerChar.ToString().ToUpper()}");
            }
            var stopTime = DateTime.Now.Minute;
            System.Console.WriteLine($"\nResultat: {correctAnswers}/{test.Answers.Count()}" +
                $"\nTid: Inte implementerat ännu." +
                $"\nFortsät med samma eller ett annat Provpass? (y/n)");
            input = System.Console.ReadLine();
            return input == "y" ? true : false;
        }
    }
}
