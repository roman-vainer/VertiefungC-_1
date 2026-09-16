// =====================================================================
// Mini-Projekt 4: Alle wichtigen eingebauten Delegate-Arten an EINEM
// durchgängigen Beispiel - eine kleine Kontaktverwaltung.
// -----------------------------------------------------------------
// Ziel: Action<T>, Func<T, TResult>, Predicate<T> und Comparison<T>
// nicht isoliert, sondern gemeinsam an einem realistischen Szenario
// sehen und verstehen, wofür genau man welchen Typ benutzt.
//
//   Action<T>      -> etwas TUN, kein Rückgabewert      (z. B. ausgeben)
//   Func<T,R>       -> etwas BERECHNEN/UMWANDELN, Rückgabewert
//   Predicate<T>    -> etwas PRÜFEN, Rückgabewert ist immer bool
//   Comparison<T>   -> zwei Elemente VERGLEICHEN (für Sortierung)
// =====================================================================

namespace AlleDelegateArten;

public record Kontakt(string Name, int Alter, string Stadt);

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Mini-Projekt 4: Action, Func, Predicate & Comparison ===\n");

        var kontakte = new List<Kontakt>
        {
            new("Sina Bauer",    34, "Berlin"),
            new("Tom Keller",    19, "München"),
            new("Lea Vogt",      45, "Berlin"),
            new("Jonas Weiss",   27, "Hamburg"),
            new("Mira Fuchs",    52, "München"),
            new("Paul Nagel",    16, "Hamburg"),
        };

        // -------------------------------------------------------------
        // 1) Action<T> - etwas TUN, kein Rückgabewert.
        // Typisch: Ausgeben, Loggen, Verändern eines Objekts.
        // List<T>.ForEach(Action<T>) ruft die Action für jedes Element auf.
        // -------------------------------------------------------------
        Console.WriteLine("--- 1) Action<Kontakt>: alle Kontakte ausgeben ---");
        Action<Kontakt> kontaktAusgeben = k =>
            Console.WriteLine($"  {k.Name,-14} | {k.Alter,3} Jahre | {k.Stadt}");

        kontakte.ForEach(kontaktAusgeben);

        // -------------------------------------------------------------
        // 2) Predicate<T> - etwas PRÜFEN, IMMER Rückgabewert bool.
        // Typisch: Filtern, Suchen. List<T> hat viele Methoden, die
        // exakt einen Predicate<T> erwarten: FindAll, Find, RemoveAll,
        // Exists, TrueForAll ...
        // -------------------------------------------------------------
        Console.WriteLine("\n--- 2) Predicate<Kontakt>: volljährige Kontakte aus Berlin filtern ---");
        Predicate<Kontakt> istVolljaehrig = k => k.Alter >= 18;
        Predicate<Kontakt> wohntInBerlin = k => k.Stadt == "Berlin";

        // Zwei Predicates lassen sich einfach kombinieren, indem man
        // sie in einem neuen Lambda aufruft (kein automatisches "Und"
        // wie bei Multicast-Delegates - Predicates geben ja bool zurück,
        // Verknüpfung macht man selbst mit && / ||).
        Predicate<Kontakt> volljaehrigUndBerlin = k => istVolljaehrig(k) && wohntInBerlin(k);

        List<Kontakt> ergebnis = kontakte.FindAll(volljaehrigUndBerlin);
        ergebnis.ForEach(kontaktAusgeben);

        // -------------------------------------------------------------
        // 3) Func<T, TResult> - etwas BERECHNEN/UMWANDELN, Rückgabewert
        // beliebigen Typs (letzter Typ-Parameter). Typisch: Projektionen,
        // Transformationen, Aggregationen - Kernstück von LINQ (Select,
        // Where nimmt zwar Func<T,bool>, Aggregate, ...).
        // -------------------------------------------------------------
        Console.WriteLine("\n--- 3) Func<Kontakt, string>: Anzeigetext für jeden Kontakt erzeugen ---");
        Func<Kontakt, string> anzeigetextErzeugen = k => $"{k.Name} ({k.Alter}) aus {k.Stadt}";

        // .Select erwartet ebenfalls einen Func<T, TResult> - Predicate<T>
        // ist im Grunde nur ein Spezialfall von Func<T, bool>.
        List<string> anzeigetexte = kontakte.Select(anzeigetextErzeugen).ToList();
        anzeigetexte.ForEach(Console.WriteLine);

        // Func kann auch mehrere Eingabeparameter haben:
        Func<Kontakt, int, string> mitLaufnummer = (k, nummer) => $"{nummer}. {k.Name}";
        Console.WriteLine("\nMit Laufnummer (Func<Kontakt, int, string>):");
        for (int i = 0; i < kontakte.Count; i++)
        {
            Console.WriteLine("  " + mitLaufnummer(kontakte[i], i + 1));
        }

        // -------------------------------------------------------------
        // 4) Comparison<T> - ZWEI Elemente VERGLEICHEN, Rückgabewert int:
        //    < 0  -> erstes Element kommt vor dem zweiten
        //    == 0 -> gleichwertig
        //    > 0  -> erstes Element kommt nach dem zweiten
        // Typisch: List<T>.Sort(Comparison<T>) für eigene Sortierlogik,
        // ohne extra eine IComparer<T>-Klasse schreiben zu müssen.
        // -------------------------------------------------------------
        Console.WriteLine("\n--- 4) Comparison<Kontakt>: nach Alter sortieren ---");
        Comparison<Kontakt> nachAlterAufsteigend = (a, b) => a.Alter.CompareTo(b.Alter);

        var nachAlterSortiert = new List<Kontakt>(kontakte);
        nachAlterSortiert.Sort(nachAlterAufsteigend);
        nachAlterSortiert.ForEach(kontaktAusgeben);

        Console.WriteLine("\n--- 4b) Comparison<Kontakt>: erst nach Stadt, dann nach Name ---");
        Comparison<Kontakt> nachStadtDannName = (a, b) =>
        {
            int stadtVergleich = string.Compare(a.Stadt, b.Stadt, StringComparison.Ordinal);
            // Wenn die Städte gleich sind (0), entscheidet der Name.
            return stadtVergleich != 0 ? stadtVergleich : string.Compare(a.Name, b.Name, StringComparison.Ordinal);
        };

        var kombiniertSortiert = new List<Kontakt>(kontakte);
        kombiniertSortiert.Sort(nachStadtDannName);
        kombiniertSortiert.ForEach(kontaktAusgeben);

        // -------------------------------------------------------------
        // 5) Alle vier Delegate-Arten in EINER Pipeline kombiniert:
        // filtern (Predicate) -> sortieren (Comparison) -> umwandeln
        // (Func) -> ausgeben (Action).
        // -------------------------------------------------------------
        Console.WriteLine("\n--- 5) Alle vier zusammen: Bericht für volljährige Kontakte ---");
        BerichtErstellen(
            kontakte,
            filter: istVolljaehrig,                              // Predicate<Kontakt>
            sortierung: nachAlterAufsteigend,                    // Comparison<Kontakt>
            formatierung: anzeigetextErzeugen,                   // Func<Kontakt, string>
            ausgabe: zeile => Console.WriteLine($"  * {zeile}")  // Action<string>
        );

        Console.WriteLine("\n--- Ende. Siehe README.md für Erklärung & Übungsaufgaben. ---");
    }

    // Eine generische Methode, die absichtlich alle vier Delegate-Arten
    // als Parameter verlangt, um zu zeigen, wie sie im Zusammenspiel
    // eine kleine Verarbeitungs-Pipeline bilden.
    private static void BerichtErstellen(
        List<Kontakt> kontakte,
        Predicate<Kontakt> filter,
        Comparison<Kontakt> sortierung,
        Func<Kontakt, string> formatierung,
        Action<string> ausgabe)
    {
        List<Kontakt> gefiltert = kontakte.FindAll(filter);
        gefiltert.Sort(sortierung);

        foreach (Kontakt kontakt in gefiltert)
        {
            string zeile = formatierung(kontakt);
            ausgabe(zeile);
        }
    }
}
