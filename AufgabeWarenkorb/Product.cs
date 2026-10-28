using System;
using System.Collections.Generic;
using System.Text;

namespace AufgabeWarenkorb;

class Produkt
{
    public int ProduktNr { get; private set; }
    public string Name { get; private set; }
    public decimal Preis { get; private set; }
    public Produkt(int produktNr, string name, decimal preis)
    {
        ProduktNr = produktNr;
        Name = name;
        Preis = preis;
    }
    public override string ToString()
    {
        return $"{ProduktNr} - {Name} - {Preis}";
    }
    public static Produkt[] GetProduktListe()
    {
        return new Produkt[] {
                new Produkt(1, "Marmelade", 5),
                new Produkt(2, "Quark", 10),
                new Produkt(3, "Mohrrüben", 15),
                new Produkt(4, "Käse", 20),
                new Produkt(5, "Honig", 25),
                new Produkt(6, "Mehl", 30)
            };
    }
}