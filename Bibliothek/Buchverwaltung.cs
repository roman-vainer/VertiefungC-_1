namespace Bibliothek;

public class Buchverwaltung
{

    public static void Preisberechnen(Buch buch)
    {
            TimeSpan ausleihdauer = buch.Rueckgabedatum - buch.Ausleihdatum;
            decimal gesamtpreis = (decimal)ausleihdauer.TotalDays * buch.PreisProTag;
            Console.WriteLine($"Der Gesamtpreis für das Buch '{buch.Titel}' beträgt: {gesamtpreis:C}' für {ausleihdauer.TotalDays} Tage.");
     
    }


     public static void Prozess(Buch buch, Action<Buch> actBuch, Func<Buch, int> funcBuch,
      Predicate<Buch> predBuch)
    {
        //Ausleihen, Zurückgeben und Preis berechnen
        actBuch(buch);
        var result = funcBuch(buch); // gibt die Ausleihdauer in Tagen zurück
        Console.WriteLine($"Die Ausleihdauer in Tagen lautet: {result}.");

        Console.WriteLine($"Ist die Ausleihdauer länger als 10 Tage? {predBuch(buch)}");

    }


}