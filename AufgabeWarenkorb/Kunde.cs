using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AufgabeWarenkorb;

class Kunde
{
    public string Name { get; private set; }
    public string Ort { get; private set; }
    public Länder Land { get; private set; }
    public Bestellung[] Bestellungen { get; private set; }
    public Kunde(string name, string ort, Länder land, Bestellung[] bestellungen)
    {
        Name = name;
        Ort = ort;
        Land = land;
        Bestellungen = bestellungen;
    }
    public override string ToString()
    {
        string result = $"{Name} - {Ort} - {Land}";
        foreach (Bestellung best in Bestellungen)
        {
            result += $"\n  {best}";
        }
        return result;
    }
    public static Kunde[] GetKundenListe()
    {
        return new Kunde[] {
                new Kunde("Walter", "Altenburg", Länder.Germany, new Bestellung[] {
                    new Bestellung(2, 4, "März", false)
                }),
                new Kunde("Thomas", "Berlin", Länder.Germany, new Bestellung[] {
                    new Bestellung(1, 11, "Juni", false),
                    new Bestellung(3, 19, "November", true)
                }),
                new Kunde("Holger", "Washington", Länder.USA, new Bestellung[] {
                    new Bestellung(5, 17, "November", true)
                }),
                new Kunde("Fernando", "New York", Länder.USA, new Bestellung[] {
                    new Bestellung(6, 12, "Juni", false)
                }),
                new Kunde("Alice", "London", Länder.GB, new Bestellung[] {
                    new Bestellung(4, 3, "Februar", true),
                    new Bestellung(2, 1, "Februar", false),
                    new Bestellung(3, 19, "Juni", true)
                })
            };
    }
}
