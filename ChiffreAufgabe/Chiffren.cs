using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ChiffreAufgabe
{
    public class Chiffren
    {
        public delegate string ChiffrierVerfahren(string klartext);

        public delegate bool Sicherheitspruefung(string nachricht);

        public static string Rueckwaerts(string text)
        {
            return new string(text.Reverse().ToArray());
        }

        public static string VokaleZuSterne(string text)
        {
            return text
                .Replace('a', '*')
                .Replace('e', '*')
                .Replace('i', '*')
                .Replace('o', '*')
                .Replace('u', '*');
        }

        public static string Leet(string text)
        {
            return text
                .Replace('a', '4')
                .Replace('e', '3')
                .Replace('i', '1')
                .Replace('o', '0');
        }

        public static string LeetZurueck(string text)
        {
            return text
                .Replace('4', 'a')
                .Replace('3', 'e')
                .Replace('1', 'i')
                .Replace('0', 'o');
        }
    }
}
