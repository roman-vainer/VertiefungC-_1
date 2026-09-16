using System;
using System.Collections.Generic;
using System.Text;
using static ChiffreAufgabe.Chiffren;

namespace ChiffreAufgabe
{
    internal class Funkzentrale
    {
        public static void Senden(string absender, string nachricht, ChiffrierVerfahren verfahren)
        {
            Console.WriteLine($"[{absender}]: {verfahren(nachricht)}");
        }

        public static ChiffrierVerfahren VerfahrenFuerTag(string wochentag)
        {
            return wochentag switch
            {
                "Montag" or "Diensatg" => Rueckwaerts,
                "Mitwoch" or "Donnerstag" => Leet,
                _ => VokaleZuSterne
            };
        }
    }
}
