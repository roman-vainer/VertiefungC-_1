using System;
using System.Collections.Generic;
using System.Text;
using static ChiffreAufgabe.Chiffren;

namespace ChiffreAufgabe
{
    public class Agent
    {
        private readonly string _codename;
        private ChiffrierVerfahren _verfahren;

        private Sicherheitspruefung _pruefen;

        public Agent(string codename, ChiffrierVerfahren verfahren, Sicherheitspruefung pruefen)
        {
            _codename = codename;
            _verfahren = verfahren;
            _pruefen = pruefen;
        }

        public void VerfahrenWechseln(ChiffrierVerfahren neuesVerfahren)
        {
            _verfahren = neuesVerfahren;
        }

        public void Melden(string nachricht)
        {
            if (_pruefen(nachricht))
            {
                Console.WriteLine($"[{_codename}] {_verfahren(nachricht)}");
            }
            else
            {
                Console.WriteLine("ABBRUCH: nicht freigegeben");
            }
        }
    }
}
