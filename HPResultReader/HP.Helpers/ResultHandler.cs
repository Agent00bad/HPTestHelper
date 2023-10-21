using HP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HP.Helpers
{
    public class ResultHandler
    {
        //test that is used for statistic and results aren't published for one of these
        public Char[] possibleUnassignedTest = new Char[] { '1', '2', '3', '4', '5' };
        //possible answers
        public Char[] possibleAnswers = new Char[] { 'a', 'b', 'c', 'd', 'e' };
        public IEnumerable<QuarterTest> ConvertToQuarterTests(string pdfResults)
        {
            var resultRows = RowsToList(pdfResults);
            List<QuarterTest> finalTestResults = new List<QuarterTest>();
            int testId = 1;
            for (int i = 1; i <= 4; i++)
            {
                finalTestResults.Add(new QuarterTest
                {
                    TestId = testId != UnassignedTest(resultRows.Last()) ? testId : (testId += 1),
                    Answers = RowListToAnswer(resultRows, i)
                });
                testId++;
            }
            return finalTestResults;
        }
        private List<string> RowsToList(string result)
        {
            //removes all whitespaces and spaces and turns to lower
            string configuredResult = result.Replace(" ", "").ToLower();
            //turns configuredResult into list where every row is a seperate item in the new resultsList list
            List<string> resultsList = configuredResult.Split(Environment.NewLine.ToCharArray()).ToList();

            //removes all empty string items
            resultsList.RemoveAll(s => s == string.Empty);
            return resultsList;
        }
        private IEnumerable<Answer> RowListToAnswer(List<string> rows, int quarterTestIndex)
        {
            List<string> rowsCopy = new List<string>();
            
            //removes last row containing details on test not used
            for (int i = 0; i < rows.Count - 1; i++)
            {
                rowsCopy.Add(rows[i]);
            }
            var answers = new List<Answer>();
            
            for (int i = 0; i < rowsCopy.Count; i++)
            {
                string relatedAnswer = rowsCopy[i].Substring(i < 9 
                    ?(quarterTestIndex * 2 -2) 
                    : (quarterTestIndex * 3 -3),
                    i < 9 ? 2 : 3 );
                var answerIdValues = relatedAnswer.Where(s => Int32.TryParse(s.ToString(), out int notUsed));

                string answerId = string.Empty;
                foreach (var value in answerIdValues)
                {
                    int answerIdSingleValue;
                    if (Int32.TryParse(value.ToString(), out answerIdSingleValue)) answerId += answerIdSingleValue;
                }
                var answerChar = relatedAnswer.First(s => possibleAnswers.Contains(s));
                answers.Add(new Answer()
                {
                    AnswerId = answerId,
                    AnswerChar = answerChar
                }
                );
            }
            return answers;
        }
        private int UnassignedTest(string LastResultRow)
        {
            int testId = 0;
            var testIdAsChar = LastResultRow.First(c => possibleUnassignedTest.Contains(c)).ToString();
            if (Int32.TryParse(testIdAsChar, out testId)) return testId;
            return testId;
        }
    }
}
