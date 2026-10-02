namespace HelloStreams;

using ClosedXML.Excel;

internal class Program
{
    static void Main(string[] args)
    {



        string filename = @"C:\Users\70192\source\repos\VertiefungC#_1\HelloStreams\HelloStreams\docs\teilnehmer_liste.xlsx";

        using XLWorkbook xlW = new(filename);

        var worksheet = xlW.Worksheet(1);

        Console.WriteLine(worksheet.Cell("A4").Value);
        Console.WriteLine(worksheet.Cell(3, 5).Value);

        int letzteZeile = worksheet.LastRowUsed().RowNumber();
        int letzteSpalte = worksheet.LastColumnUsed().ColumnNumber();
        Console.WriteLine();

        for (int i = 1; i <= letzteZeile; i++)
        {
            for (int j = 1; j <= letzteSpalte; j++)
            {
                Console.Write($"{worksheet.Cell(i, j).Value,-15}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        worksheet.Cell(4, 3).Value = 100;
        xlW.Save();

        for (int i = 1; i <= letzteZeile; i++)
        {
            for (int j = 1; j <= letzteSpalte; j++)
            {
                Console.Write($"{worksheet.Cell(i, j).Value,-15}");
            }
            Console.WriteLine();
        }
    }
}
