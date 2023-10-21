using HP.Console;
using HP.Helpers;
using System;

var pdf = new PDFHandler();
var resultHandler = new ResultHandler();

Console.WriteLine("Skriv filens sökväg (filepath)");
var filepath = Console.ReadLine();

string extractedPdf = pdf.ExtractResults(filepath);
var results = resultHandler.ConvertToQuarterTests(extractedPdf);


var ui = new ConsoleUI(results, new Char[] { 'a', 'b', 'c', 'd', 'e' });

bool running = true;

while (running)
{
    var test = ui.ChooseQuarterResult();
    if (test != null)
    {
        running = ui.Run(test);
    }
}