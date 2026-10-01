using System.Xml.Linq;

namespace AufgabeWarenkorb;

public class Program
{
    static Kunde[] kunden;
    static Produkt[] produkte;
    static void Main(string[] args)
    {
        kunden = Kunde.GetKundenListe();
        produkte = Produkt.GetProduktListe();
        Aufgaben();
    }

    public static void Aufgaben()
    {
        // a) Selektieren Sie nur die Namen aller Produkte und anschließend nur Name und Wohnort aller 
        //Kunden aus den beiden Arrays
        var produkteNamen = produkte.Select(x => x.Name).ToList();
        produkteNamen.ForEach(x => Console.WriteLine(x));

        Console.WriteLine();
        var kundenDaten = kunden.Select((x) => (x.Name, x.Ort)).ToList();
        kundenDaten.ForEach(x => Console.WriteLine($"{x.Name} - {x.Ort}"));

        // b) Selektieren Sie die Bestellungen aller Kunden aus Deutschland
        Console.WriteLine();
        var bestellungenAusDeutschland = kunden.Where(k => k.Land == Länder.Germany).Select(k => k.Bestellungen).ToList();
        bestellungenAusDeutschland.ForEach(b => Array.ForEach(b, x => Console.WriteLine(x)));

        // c) Selektieren Sie nur den Namen für jeden zweiten Kunden, beginnend mit dem ersten
        Console.WriteLine();
        var kundenNamen = kunden.Where((k, i) => i % 2 == 0).Select(k => k.Name).ToList();
        kundenNamen.ForEach(x => Console.WriteLine(x));

        // d) Selektieren Sie nur Name und Preis aller Produkte, die höchstens 20 Euro kosten. Das 
        //Ergebnis soll absteigend nach dem Preis sortiert sein.
        Console.WriteLine();
        var produkteNamenUndPreis = produkte.Where(p => p.Preis > 20).Select(p => (p.Name, p.Preis)).OrderByDescending(p => p.Preis).ToList();
        produkteNamenUndPreis.ForEach(p => Console.WriteLine($"{p.Name} - {p.Preis}"));

        // e) Selektieren Sie nur Name und Land aller Kunden. Das Ergebnis soll aufsteigend nach dem 
        //Land sortiert werden.Bei gleichem Land sollen die Kunden nach dem Namen sortiert werden.
        Console.WriteLine();
        var nameUndLandKunden = kunden.Select(k => (k.Name, k.Land)).OrderBy(k => k.Land.ToString()).ThenBy(k => k.Name).ToList();
        nameUndLandKunden.ForEach(k => Console.WriteLine($"{k.Name} - {k.Land}"));

        // f) Gruppieren Sie die Kunden nach dem Land.Als Gruppenelement soll jeweils das gesamte
        //Kunden - Objekt verwendet werden.
        Console.WriteLine();
        var kundenGruppen = kunden.GroupBy(k => k.Land).ToList();
        kundenGruppen.ForEach(g =>
        {
            Console.WriteLine($"\n{g.Key}");
            g.ToList().ForEach(k => Console.WriteLine(k));
        });

        // g) Gruppieren Sie die Produkte nach dem ersten Buchstaben des Namens. Als Elemente in den 
        //Gruppen sollen nur die Namen der Produkte vorhanden sein.
        Console.WriteLine();
        var ProdukteGruppen = produkte.Select(p => p.Name).GroupBy(p => p[0]).ToList();
        ProdukteGruppen.ForEach(g =>
        {
            Console.WriteLine($"\n{g.Key}");
            g.ToList().ForEach(p => Console.WriteLine(p));
        });

        // h) Bilden Sie einen Join zwischen den Bestellungen und den Produkten. Selektieren Sie dann die 
        //Werte für Monat, ProduktNr, Name, Preis und Versendet sortiert nach dem Preis.
        Console.WriteLine();


    }
}
