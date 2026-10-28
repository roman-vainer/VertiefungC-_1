using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LINQ;

public static class ExtensionMethods
{
    //int filter
    public static int[] LessThanSeven(this int[] zahlen) => zahlen.Where(x => x < 7).ToArray();

    public static int[] GetEven(this int[] numbers) => [.. numbers.Where(n => n % 2 == 0)];

    public static int[] GetSingleDigetOdd(this int[] numbers) => [.. numbers.Where(n => n < 10 && n % 2 != 0)];

    public static int[] GetEvenAfter5thElement(this int[] numbers) => [.. numbers.Where((n, i) => i > 4 && n % 2 == 0)];

    public static int[] GetDivisibleBy2And3(this int[] numbers) => [.. numbers.Where(n => n % 2 == 0 && n % 3 == 0)];

    //string filter
    public static string[] GetLongerThan3Char(this string[] numbers) => [.. numbers.Where(n => n.Length == 3)];

    public static string[] GetContainsO(this string[] numbers) => [.. numbers.Where(n => n.Contains('o'))];

    public static string[] GetEndWithTeen(this string[] numbers) => [.. numbers.Where(n => n.EndsWith("teen"))];

    public static string[] GetEndWithTeenToUpper(this string[] numbers) => [.. numbers.Where(n => n.EndsWith("teen")).Select(n => n.ToUpper())];

    public static string[] GetContainsFour(this string[] numbers) => [.. numbers.Where(n => n.Contains("four"))];

    public static string[] GetStartNotWithTOrF(this string[] numbers) => [.. numbers.Where(n => !n.StartsWith('f') && !n.StartsWith('t'))];

    //int sort
    public static int[] OrderAscending(this int[] num) => [.. num.Order()];

    public static int[] OrderDescending(this int[] num) => [.. num.OrderByDescending(n => n)];

    public static int[] OrderDescendingEven(this int[] num) => [.. num.Where(n => n % 2 == 0).Order()];

    public static int[] OrderDescendingBetween5And11(this int[] num) => [.. num.Where(n => n >= 5 && n <= 11).OrderByDescending(n => n)];

    //string sort
    public static string[] OrderAscendingWordSize(this string[] numbers) => [.. numbers.OrderBy(n => n.Length)];

    public static string[] OrderAscendingWordSizeAndDescendingAlphabet(this string[] numbers) => [.. numbers.OrderBy(n => n.Length).ThenByDescending(n => n)];

    public static string[] MyReverse(this string[] numbers) => [.. numbers.Reverse()];

    public static string[] OrderByLetter(this string[] numbers) => [.. numbers.OrderBy(n => n[0]).ThenByDescending(n => n[^1])];

    //Aggregation
    public static int GetSum(this int[] num) => num.Sum();

    public static int GetMin(this int[] num) => num.Min();

    public static int GetMax(this int[] num) => num.Max();

    public static double GetAverage(this int[] num) => num.Average();

    public static int GetMinEven(this int[] num) => num.Where(n => n % 2 == 0).Min();

    public static int GetMaxOdd(this int[] num) => num.Where(n => n % 2 != 0).Max();

    public static int GetSumEven(this int[] num) => num.Where(n => n % 2 == 0).Sum();

    public static double GetAverageOdd(this int[] num) => num.Where(n => n % 2 != 0).Average();

    public static double GetEvenCount(this int[] num) => num.Where(n => n % 2 == 0).Count();
}
