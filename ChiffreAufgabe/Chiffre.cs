using System;
using System.Collections.Generic;
using System.Text;
using static ChiffreAufgabe.Chiffren;

namespace ChiffreAufgabe
{
    public class Chiffre
    {
        private string _name;
        private ChiffrierVerfahren _verschluesseln;
        private ChiffrierVerfahren _entschluesseln;

        public void Test(string klartext)
        {
            Console.WriteLine($"Klartext: {klartext}");
        }
    }
}
