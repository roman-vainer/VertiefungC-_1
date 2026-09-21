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

        public Chiffre(string name, ChiffrierVerfahren verschlusseln, ChiffrierVerfahren entschlusseln)
        {
            _name = name;
            _verschluesseln = verschlusseln;
            _entschluesseln = entschlusseln;
        }

        public void Test(string klartext)
        {
            string geheimtext;
            Console.WriteLine($"Klartext: {klartext}");
            Console.WriteLine($"Geheimtext: {geheimtext = _verschluesseln(klartext)}");
            Console.WriteLine($"Zurückentschlüsselten Text: {_entschluesseln(geheimtext)}");
        }
    }
}
