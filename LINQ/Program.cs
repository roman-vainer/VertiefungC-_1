namespace LINQ;

public class Program
{
    static void Main(string[] args)
    {
        int[] num = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0, 22, 12, 16, 18, 11, 19, 13 };
        string[] numbers = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen"];
        Console.WriteLine("===initial data===");
        Console.WriteLine(String.Join(',', num));
        Console.WriteLine(String.Join(',', numbers));

        //int filter
        Console.WriteLine("\n===int filter===");
        Console.WriteLine(String.Join(',', num.LessThanSeven()));
        Console.WriteLine(String.Join(',', num.GetEven()));
        Console.WriteLine(String.Join(',', num.GetSingleDigetOdd()));
        Console.WriteLine(String.Join(',', num.GetEvenAfter5thElement()));
        Console.WriteLine(String.Join(',', num.GetDivisibleBy2And3()));

        //string filter
        Console.WriteLine("\n===string filter===");
        Console.WriteLine(String.Join(',', numbers.GetLongerThan3Char()));
        Console.WriteLine(String.Join(',', numbers.GetContainsO()));
        Console.WriteLine(String.Join(',', numbers.GetEndWithTeen()));
        Console.WriteLine(String.Join(',', numbers.GetEndWithTeenToUpper()));
        Console.WriteLine(String.Join(',', numbers.GetContainsFour()));
        Console.WriteLine(String.Join(',', numbers.GetStartNotWithTOrF()));

        //int sort
        Console.WriteLine("\n===int sort===");
        Console.WriteLine(String.Join(',', num.OrderAscending()));
        Console.WriteLine(String.Join(',', num.OrderDescending()));
        Console.WriteLine(String.Join(',', num.OrderDescendingEven()));
        Console.WriteLine(String.Join(',', num.OrderDescendingBetween5And11()));

        //string sort
        Console.WriteLine("\n===string sort===");
        Console.WriteLine(String.Join(',', numbers.OrderAscendingWordSize()));
        Console.WriteLine(String.Join(',', numbers.OrderAscendingWordSizeAndDescendingAlphabet()));
        Console.WriteLine(String.Join(',', numbers.MyReverse()));
        Console.WriteLine(String.Join(',', numbers.OrderByLetter()));

        //aggregation
        Console.WriteLine("\n===aggregation===");
        Console.WriteLine(String.Join(',', num.GetSum()));
        Console.WriteLine(String.Join(',', num.GetMin()));
        Console.WriteLine(String.Join(',', num.GetMax()));
        Console.WriteLine(String.Join(',', num.GetAverage()));
        Console.WriteLine(String.Join(',', num.GetMinEven()));
        Console.WriteLine(String.Join(',', num.GetMaxOdd()));
        Console.WriteLine(String.Join(',', num.GetSumEven()));
        Console.WriteLine(String.Join(',', num.GetAverageOdd()));
        Console.WriteLine(String.Join(',', num.GetEvenCount()));
    }
}
