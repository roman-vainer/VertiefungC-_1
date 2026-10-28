using System;
using System.Collections.Generic;
using System.Text;

namespace AufgabeWarenkorb;

class Bestellung
{
    public int ProduktNr { get; private set; }
    public int Anzahl { get; private set; }
    public string Monat { get; private set; }
    public bool Versendet { get; private set; }
    public Bestellung(int produktNr, int anzahl, string monat, bool versendet)
    {
        ProduktNr = produktNr;
        Anzahl = anzahl;
        Monat = monat;
        Versendet = versendet;
    }
    public override string ToString()
    {
        return $"ProdNr: {ProduktNr}, Anzahl: {Anzahl}, Monat: {Monat}, Versand: {Versendet}";
    }
    public override bool Equals(object obj)
    {
        if (!(obj is Bestellung))
            return false;
        else
        {
            Bestellung b = (Bestellung)obj;
            return (b.ProduktNr == this.ProduktNr && b.Monat == this.Monat && b.Anzahl == this.Anzahl && b.Versendet == this.Versendet);
        }
    }
    public override int GetHashCode()
    {
        return $"{ProduktNr}|{Anzahl}|{Monat}|{Versendet}".GetHashCode();
    }
}
