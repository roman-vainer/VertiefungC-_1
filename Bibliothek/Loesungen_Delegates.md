# Lösungen: Delegates im Projekt "Bibliothek"

Dies ist die Lösung zu `Aufgaben_Delegates.md`. Eine mögliche saubere Umsetzung mit den in der Aufgabenstellung vorgeschlagenen Klassen `UebungTeil1` bis `UebungTeil4`. Es gibt bei vielen Aufgaben mehrere richtige Lösungswege – hier ist einer davon, jeweils mit kurzer Begründung.

---

## Vorbereitung: Ergänzungen in `BuchMethoden.cs`

```csharp
namespace Bibliothek;

public class BuchMethoden
{
    // ... bestehende Methoden Ausleihen() und Zurueckgeben() bleiben unverändert ...

    // Aufgabe 1.1
    public static void BuchDrucken(Buch buch)
    {
        Console.WriteLine($"{buch.Titel} – {buch.Autor} ({buch.Jahr})");
    }

    // Aufgabe 1.3
    public static void BuchTrennlinieDrucken(Buch buch)
    {
        Console.WriteLine("------------------------------");
    }

    // Aufgabe 1.2
    public static int BerechneAlter(Buch buch)
    {
        return DateTime.Now.Year - buch.Jahr;
    }

    // Aufgabe 2.2
    public static void BuchDetailsAnzeigen(Buch buch)
    {
        Console.WriteLine($"Titel: {buch.Titel}");
        Console.WriteLine($"Autor: {buch.Autor}");
        Console.WriteLine($"Jahr: {buch.Jahr}");
        Console.WriteLine($"ISBN: {buch.ISBN}");
        Console.WriteLine($"Preis/Tag: {buch.PreisProTag:C}");
        Console.WriteLine($"Verliehen: {buch.Verliehen}");
    }

    // Aufgabe 2.3
    public static string BuchZusammenfassung(Buch buch)
    {
        return $"{buch.Titel} ({buch.Jahr}) von {buch.Autor}";
    }

    // Aufgabe 4.4 – erweiterte Ausleihen()-Methode mit optionalem Callback
    public static void Ausleihen(Buch buch, BenachrichtigungsDelegate? benachrichtigung = null)
    {
        if (!buch.Verliehen)
        {
            buch.Verliehen = true;
            Console.WriteLine($"Bitte geben Sie das Ausleihdatum für das Buch '{buch.Titel}' ein (Format: yyyy-MM-dd):");
            buch.Ausleihdatum = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine($"Das Buch '{buch.Titel}' wurde erfolgreich ausgeliehen.");

            if (benachrichtigung != null)
            {
                benachrichtigung($"{buch.Titel} wurde soeben ausgeliehen.");
            }
        }
        else
        {
            Console.WriteLine($"Das Buch '{buch.Titel}' ist bereits verliehen.");
        }
    }

    public static void Zurueckgeben(Buch buch)
    {
        if (buch.Verliehen)
        {
            buch.Verliehen = false;
            Console.WriteLine($"Bitte geben Sie das Rückgabedatum für das Buch '{buch.Titel}' ein (Format: yyyy-MM-dd):");
            buch.Rueckgabedatum = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine($"Das Buch '{buch.Titel}' wurde erfolgreich zurückgegeben.");
        }
        else
        {
            Console.WriteLine($"Das Buch '{buch.Titel}' war nicht verliehen.");
        }
    }
}
```

> Hinweis: `Ausleihen` bekommt hier einen **zusätzlichen optionalen** Parameter (`= null`). Bestehende Aufrufe wie `BuchMethoden.Ausleihen` als `Action<Buch>` in `Program.cs` (Zeile 20) funktionieren weiterhin, da optionale Parameter bei Delegate-Zuweisungen mit passender Signatur kompatibel bleiben, solange die Methodengruppe (`Ausleihen`) einer `Action<Buch>` zugewiesen wird.

---

## Teil 1: Eigene Delegates deklarieren

### Deklaration des Delegate-Typs

Am besten in einer eigenen kleinen Datei `BuchAktion.cs`:

```csharp
namespace Bibliothek;

public delegate void BuchAktion(Buch buch);
public delegate int BuchAlterBerechnung(Buch buch);
public delegate void BenachrichtigungsDelegate(string nachricht);
```

### `UebungTeil1.cs`

```csharp
namespace Bibliothek;

public class UebungTeil1
{
    public static void Run()
    {
        Buch buch = new Buch("Die unendliche Geschichte", "Michael Ende", 1979, "978-3-522-20000-1", 0.35m);

        // Aufgabe 1.1: eigener Delegate ohne Rückgabewert
        BuchAktion aktion = BuchMethoden.BuchDrucken;
        aktion(buch);

        // Aufgabe 1.2: eigener Delegate mit Rückgabewert
        BuchAlterBerechnung alterBerechnung = BuchMethoden.BerechneAlter;
        int alter = alterBerechnung(buch);
        Console.WriteLine($"Das Buch ist {alter} Jahre alt.");

        // Aufgabe 1.3: Multicast mit eigenem Delegate
        BuchAktion mehrfachAktion = BuchMethoden.BuchDrucken;
        mehrfachAktion += BuchMethoden.BuchTrennlinieDrucken;
        mehrfachAktion += (Buch b) => Console.WriteLine("Ende der Ausgabe");
        mehrfachAktion(buch);

        // Aufgabe 1.4: eigener Delegate als Parameter einer Methode
        Buchverwaltung.FuehreAktionAus(buch, BuchMethoden.BuchDrucken);
        Buchverwaltung.FuehreAktionAus(buch, (Buch b) => Console.WriteLine($"Lambda-Aktion für {b.Titel}"));
    }
}
```

### Ergänzung in `Buchverwaltung.cs` (Aufgabe 1.4)

```csharp
public static void FuehreAktionAus(Buch buch, BuchAktion aktion)
{
    Console.WriteLine("Starte Aktion...");
    aktion(buch);
    Console.WriteLine("Aktion beendet.");
}
```

**Warum funktioniert das?** `BuchAktion` ist ein eigener Delegate-Typ mit der Signatur `void(Buch)`. Jede Methode und jede Lambda-Funktion mit exakt dieser Signatur kann einer `BuchAktion`-Variable zugewiesen werden – der Delegate-Typ ist quasi ein "Vertrag" über Rückgabewert und Parameter.

---

## Teil 2: Standard-Delegate-Typen verwenden

### `UebungTeil2.cs`

```csharp
namespace Bibliothek;

public class UebungTeil2
{
    public static void Run()
    {
        // Aufgabe 2.1: Action ohne Parameter
        Action begruessung = () => Console.WriteLine("Willkommen in der Bibliotheksverwaltung!");
        begruessung();

        Buch buch1 = new Buch("Harry Potter und der Stein der Weisen", "J.K. Rowling", 1997, "978-3-551-35701-3", 0.45m);
        Buch buch2 = new Buch("1984", "George Orwell", 1949, "978-3-548-23410-8", 0.30m);

        // Aufgabe 2.2: Action<Buch>
        Action<Buch> detailsAnzeigen = BuchMethoden.BuchDetailsAnzeigen;
        detailsAnzeigen(buch1);
        detailsAnzeigen(buch2);

        // Aufgabe 2.3: Func<Buch, string>
        Func<Buch, string> zusammenfassung = BuchMethoden.BuchZusammenfassung;
        string text = zusammenfassung(buch1);
        Console.WriteLine(text);

        // Aufgaben 2.4 - 2.6: Liste mit mehreren Büchern
        List<Buch> buecherListe = new List<Buch>
        {
            new Buch("Harry Potter und der Stein der Weisen", "J.K. Rowling", 1997, "978-3-551-35701-3", 0.45m),
            new Buch("1984", "George Orwell", 1949, "978-3-548-23410-8", 0.30m),
            new Buch("Der Herr der Ringe", "J.R.R. Tolkien", 1954, "978-3-551-35701-4", 0.55m),
            new Buch("Die unendliche Geschichte", "Michael Ende", 1979, "978-3-522-20000-1", 0.35m),
            new Buch("Der kleine Prinz", "Antoine de Saint-Exupéry", 1943, "978-3-15-018899-2", 0.20m)
        };

        // Aufgabe 2.4: Predicate<Buch>
        Predicate<Buch> altesBuch = (Buch b) => b.Jahr < 1990;
        List<Buch> alteBuecher = buecherListe.FindAll(altesBuch);
        Console.WriteLine("Bücher vor 1990:");
        alteBuecher.ForEach(b => Console.WriteLine($"- {b.Titel} ({b.Jahr})"));

        // Aufgabe 2.5: Comparison<Buch>
        Comparison<Buch> nachJahrSortieren = (Buch b1, Buch b2) => b1.Jahr.CompareTo(b2.Jahr);
        buecherListe.Sort(nachJahrSortieren);
        Console.WriteLine("Bücher nach Jahr sortiert:");
        buecherListe.ForEach(b => Console.WriteLine($"- {b.Titel} ({b.Jahr})"));

        // Aufgabe 2.6: Filtern, Sortieren, Ausgeben kombiniert
        Predicate<Buch> verfuegbar = (Buch b) => !b.Verliehen;
        List<Buch> verfuegbareBuecher = buecherListe.FindAll(verfuegbar);

        Comparison<Buch> nachPreisSortieren = (Buch b1, Buch b2) => b1.PreisProTag.CompareTo(b2.PreisProTag);
        verfuegbareBuecher.Sort(nachPreisSortieren);

        Action<Buch> ausgabe = (Buch b) => Console.WriteLine($"{b.Titel}: {b.PreisProTag:C}/Tag");
        verfuegbareBuecher.ForEach(ausgabe);
    }
}
```

**Erklärung Aufgabe 2.6:** `FindAll`, `Sort` und `ForEach` sind bereits fertige Methoden von `List<T>`. Man muss sie nicht selbst implementieren – man übergibt ihnen nur den passenden Delegate (`Predicate<T>`, `Comparison<T>`, `Action<T>`), und `List<T>` kümmert sich intern um die Schleifen.

---

## Teil 3: Lambda-Funktionen

### `UebungTeil3.cs`

```csharp
namespace Bibliothek;

public class UebungTeil3
{
    public static void Run()
    {
        Buch buch = new Buch("Der Herr der Ringe", "J.R.R. Tolkien", 1954, "978-3-551-35701-4", 0.55m);

        // Aufgabe 3.1: Lambda einer Variable zuweisen
        Func<Buch, bool> istNeu = buch => buch.Jahr >= 2000;
        Console.WriteLine($"Ist neu: {istNeu(buch)}");

        // Aufgabe 3.2: Lambda direkt als Argument, ohne Zwischenvariable
        List<Buch> buecherListe = new List<Buch>
        {
            new Buch("Harry Potter und der Stein der Weisen", "J.K. Rowling", 1997, "978-3-551-35701-3", 0.45m),
            new Buch("1984", "George Orwell", 1949, "978-3-548-23410-8", 0.30m),
            buch
        };

        // Kein Predicate<Buch>, kein Comparison<Buch>, kein Action<Buch> als Variable –
        // der Compiler leitet den Delegate-Typ direkt aus der Methodensignatur ab.
        var neueBuecher = buecherListe.FindAll(b => b.Jahr >= 2000);
        buecherListe.Sort((b1, b2) => b1.Titel.CompareTo(b2.Titel));
        buecherListe.ForEach(b => Console.WriteLine(b.Titel));

        // Aufgabe 3.3: Block-Lambda mit mehreren Anweisungen
        Func<Buch, string> statusText = (Buch b) =>
        {
            if (b.Verliehen)
            {
                return "Verliehen";
            }
            else
            {
                return "Verfügbar";
            }
        };
        Console.WriteLine(statusText(buch));

        // Aufgabe 3.4: Lambda + eigener Delegate kombinieren
        Buchverwaltung.FuehreAktionAus(buch, (Buch b) =>
        {
            Console.WriteLine(b.Verliehen ? $"{b.Titel} ist verliehen." : $"{b.Titel} ist verfügbar.");
        });
    }
}
```

**Erklärung Aufgabe 3.2:** Der Unterschied zu Teil 2 ist rein stilistisch – im Hintergrund entsteht trotzdem ein `Predicate<Buch>`, `Comparison<Buch>` bzw. `Action<Buch>`-Objekt, nur ohne dass wir dafür eine benannte Variable anlegen. Das ist besonders praktisch, wenn man den Delegate nur ein einziges Mal braucht.

---

## Teil 4: Erweiterung – eigene Klassen

### Aufgabe 4.1: `Buch.cs` erweitert

```csharp
namespace Bibliothek;

public class Buch
{
    public string Titel { get; set; }
    public string Autor { get; set; }
    public int Jahr { get; set; }
    public string ISBN { get; set; }
    public string Genre { get; set; }
    public DateTime Ausleihdatum { get; set; }
    public DateTime Rueckgabedatum { get; set; }

    public decimal PreisProTag { get; set; }
    public bool Verliehen { get; set; } = false;

    public Buch(string titel, string autor, int jahr, string isbn, decimal preisProTag, string genre)
    {
        Titel = titel;
        Autor = autor;
        Jahr = jahr;
        ISBN = isbn;
        PreisProTag = preisProTag;
        Genre = genre;
        Verliehen = false;
    }

    public override string ToString()
    {
        return $"{Titel} von {Autor}, veröffentlicht im Jahr {Jahr}";
    }

    public bool IstVerliehen()
    {
        return Verliehen;
    }
}
```

> Da der Konstruktor um `genre` erweitert wurde, müssen die bestehenden Aufrufe in `Program.cs` (`HarryPotter`, `HerrDerRinge`) ebenfalls angepasst werden, z. B. `new Buch(..., 0.45m, "Fantasy")`.

### Aufgabe 4.2 + 4.3: `Bibliothek.cs`

```csharp
namespace Bibliothek;

public class Bibliothek
{
    public List<Buch> Buecher { get; set; } = new List<Buch>();

    public void BuchHinzufuegen(Buch buch)
    {
        Buecher.Add(buch);
    }

    // Aufgabe 4.2
    public void BuecherVerarbeiten(Predicate<Buch> filter, Comparison<Buch> sortierung, Action<Buch> ausgabe)
    {
        List<Buch> gefiltert = Buecher.FindAll(filter);
        gefiltert.Sort(sortierung);
        gefiltert.ForEach(ausgabe);
    }

    // Aufgabe 4.3
    public Func<List<Buch>, decimal> GesamtPreisProTag = (List<Buch> buecher) =>
    {
        decimal summe = 0;
        foreach (var buch in buecher)
        {
            summe += buch.PreisProTag;
        }
        return summe;
    };
}
```

### `UebungTeil4.cs`

```csharp
namespace Bibliothek;

public class UebungTeil4
{
    public static void Run()
    {
        Bibliothek bibliothek = new Bibliothek();
        bibliothek.BuchHinzufuegen(new Buch("Harry Potter und der Stein der Weisen", "J.K. Rowling", 1997, "978-3-551-35701-3", 0.45m, "Fantasy"));
        bibliothek.BuchHinzufuegen(new Buch("1984", "George Orwell", 1949, "978-3-548-23410-8", 0.30m, "Dystopie"));
        bibliothek.BuchHinzufuegen(new Buch("Der Herr der Ringe", "J.R.R. Tolkien", 1954, "978-3-551-35701-4", 0.55m, "Fantasy"));

        // Aufgabe 4.2: mit Methodennamen
        bibliothek.BuecherVerarbeiten(
            (Buch b) => !b.Verliehen,
            (Buch b1, Buch b2) => b1.Jahr.CompareTo(b2.Jahr),
            BuchMethoden.BuchDrucken);

        // Aufgabe 4.2: mit Lambda-Funktionen
        bibliothek.BuecherVerarbeiten(
            filter: (Buch b) => b.Genre == "Fantasy",
            sortierung: (b1, b2) => b1.Titel.CompareTo(b2.Titel),
            ausgabe: (Buch b) => Console.WriteLine($"Fantasy-Buch: {b.Titel}"));

        // Aufgabe 4.3: Statistik
        decimal gesamt = bibliothek.GesamtPreisProTag(bibliothek.Buecher);
        Console.WriteLine($"Gesamtwert pro Tag: {gesamt:C}");

        // Aufgabe 4.4: Delegate als Callback / Benachrichtigung
        Buch buch = bibliothek.Buecher[0];

        // ohne Benachrichtigung:
        BuchMethoden.Ausleihen(buch);
        BuchMethoden.Zurueckgeben(buch);

        // mit Benachrichtigung (Lambda):
        BuchMethoden.Ausleihen(buch, (string nachricht) => Console.WriteLine($"[BENACHRICHTIGUNG] {nachricht}"));
    }
}
```

**Erklärung Aufgabe 4.4:** Der optionale Parameter `BenachrichtigungsDelegate? benachrichtigung = null` erlaubt es, `Ausleihen` weiterhin wie bisher aufzurufen (ohne Callback), aber optional einen "Beobachter" zu informieren, sobald das Ausleihen abgeschlossen ist. Genau dieses Muster – ein Delegate, der am Ende einer Aktion aufgerufen wird, falls vorhanden – ist die Grundidee hinter **Events** in C#.

---

## Zusammenfassung der Kernkonzepte

| Konzept | Beispiel aus der Lösung |
|---|---|
| Eigener Delegate-Typ | `public delegate void BuchAktion(Buch buch);` |
| Delegate-Variable Methode zuweisen | `BuchAktion a = BuchMethoden.BuchDrucken;` |
| Multicast (`+=`) | `a += BuchMethoden.BuchTrennlinieDrucken;` |
| Delegate als Parameter | `FuehreAktionAus(Buch buch, BuchAktion aktion)` |
| `Action<T>` | `Action<Buch> a = BuchMethoden.BuchDetailsAnzeigen;` |
| `Func<T, TResult>` | `Func<Buch, string> f = BuchMethoden.BuchZusammenfassung;` |
| `Predicate<T>` | `buecherListe.FindAll(b => b.Jahr < 1990);` |
| `Comparison<T>` | `buecherListe.Sort((b1, b2) => b1.Jahr.CompareTo(b2.Jahr));` |
| Lambda mit Variable | `Func<Buch, bool> istNeu = buch => buch.Jahr >= 2000;` |
| Lambda inline (ohne Variable) | `buecherListe.FindAll(b => b.Jahr >= 2000)` |
| Block-Lambda mit `return` | `(Buch b) => { if (...) return "..."; else return "..."; }` |
| Optionaler Delegate-Parameter (Callback) | `void Ausleihen(Buch buch, BenachrichtigungsDelegate? b = null)` |
