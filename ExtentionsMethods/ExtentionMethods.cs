using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ExtentionsMethods;

public static class ExtentionMethods
{
    public static string Left(this string text, int n) => text.Substring(0, n);

    public static string Right(this string text, int n) => text.Substring(text.Length - n);

    public static bool IsEven(this int n) => n % 2 == 0;

    public static bool IsOdd(this int n) => n % 2 != 0;

    public static bool IsPallindrome(this string text)
    {
        char[] ch = text.ToLower().Replace(" ","").ToCharArray();
        int length = ch.Length;
        for (int i = 0; i < length / 2; i++)
        {
            if (ch[i] != ch[length - i - 1]) { return false; }
        }
        return true;
    }

    public static int WordCount(this string text) => text.Split(' ').Length;


    public static string Umdrehen(this string text)
    {
        var ch = text.ToCharArray();
        StringBuilder sb = new();

        for (int i = ch.Length - 1; i >= 0; i--) { sb.Append(ch[i]); }

        return sb.ToString();
    }

    public static int MinNumber(this List<int> numbers)
    {
        int min = numbers[0];
        foreach (var item in numbers)
        {
            if (item < min) { min = item; }
        }
        return min;
    }

    public static List<string> LongerThan(this List<string> words, int length)
    {
        List<string> newList = [ ];
        foreach (var item in words)
        {
            if (item.Length > length) { newList.Add(item); }
        }
        return newList;
    }

    public static string ToPrettyString<T>(this List<T> l)
    {
        return String.Join(',', l);
    }

    


}


