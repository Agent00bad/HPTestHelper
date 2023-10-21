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

            var answering = true;
            string input;
            bool changeQuestion;
            for (int i = 0; i < test.Answers.Count(); i++)
            {
                do
                {
                    System.Console.WriteLine($"Fråga {test.Answers.ToList()[i].AnswerId}:");

                    input = System.Console.ReadLine().ToLower();
                    changeQuestion = input.Contains(':');
                    if (changeQuestion) answering = false;
                    else if (!_possibleAnswers.Select(c => c.ToString())
                                 .Contains(input))
                    {
                        System.Console.WriteLine("felaktig inmatning");
                        answering = true;
                    }
                } while (answering);

                if (input.First() == ':')
                {
                    string questionIndex = input.Substring(1);
                    if (QuestionIdValidator(test, questionIndex))
                    {
                        if (!Int32.TryParse(questionIndex, out i)) System.Console.WriteLine("Felaktig + inmatning");
                        else
                        {
                            //TODO: add better implementation later
                            System.Console.WriteLine($"\nByt till fråga {test.Answers.ToList()[i-1].AnswerId}\n");
                            i = i - 2 ;
                        }
                    }
                }
                else if (input == test.Answers.ToList()[i].AnswerChar.ToString())
                {
                    correctAnswers++;
                    System.Console.WriteLine($"\n{input.ToUpper()} är rätt!");
                }
                else
                    System.Console.WriteLine($"\n{input.ToUpper()} är fel" +
                                             $"\nRätt svar är {test.Answers.ToList()[i].AnswerChar.ToString().ToUpper()}");
            }

            var stopTime = DateTime.Now.Minute;
            System.Console.WriteLine($"\nResultat: {correctAnswers}/{test.Answers.Count()}" +
                                     $"\nTid: Inte implementerat ännu." +
                                     $"\nFortsät med samma eller ett annat Provpass? (y/n)");
            input = System.Console.ReadLine();
            return input == "y" ? true : false;
        }

        private bool QuestionIdValidator(QuarterTest test, string questionId)
        {
            var foundAnswers = test.Answers.ToList().FirstOrDefault(a => a.AnswerId == questionId);
            if (foundAnswers != null) return true;
            return false;
        }
    }
}