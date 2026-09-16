using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace ChiffreAufgabe
{
    public class Sicherheit
    {
        public static bool NichtZulang(string nachricht)
        {
            return nachricht.Length <= 20;
        }

        public static bool KeinKlarname(string nachricht)
        {
            return !nachricht.Contains("Moskau");
        }
    }
}
