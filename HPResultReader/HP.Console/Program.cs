using HP.Console;
using HP.Helpers;

var pdf = new PDFHandler();
var resultHandler = new ResultHandler();

string extractedPdf = pdf.ExtractResults("C:/Users/Daniel Aldén .NET/Desktop/HP program/Test-facit.pdf");
var results = resultHandler.ConvertToQuarterTests(extractedPdf);


var ui = new ConsoleUI(results, new Char[] {'a','b','c','d','e'});

bool running = true;

while (running)
{   
    var test = ui.ChooseQuarterResult();
    if(test != null)
    {
        running = ui.Run(test);
    }
}