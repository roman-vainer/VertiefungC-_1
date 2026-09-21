// =====================================================================
// Mini-Projekt 2: Multicast-Delegates + eingebaute Delegate-Typen
// (Action, Func, Predicate)
// -----------------------------------------------------------------
// Ziel: Verstehen, dass ein Delegate mehrere Methoden gleichzeitig
// "kennen" kann (Multicast), wie man Methoden mit += hinzufügt und
// mit -= wieder entfernt, und dass man für die meisten Fälle gar
// keinen eigenen Delegate-Typ braucht, weil .NET bereits Action,
// Func und Predicate mitbringt.
// =====================================================================

using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace MulticastUndFunc;

// Eigener Delegate-Typ für eine "Benachrichtigung" (kein Rückgabewert).
public delegate void BenachrichtigungsHandler(string nachricht);

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Mini-Projekt 2: Multicast-Delegates & Action/Func ===\n");

        //MulticastBeispiel();
        //Console.WriteLine();
        //AbmeldenBeispiel();
        //Console.WriteLine();
        //ActionFuncPredicateBeispiel();
        //Console.WriteLine();
        //RueckgabewertBeiMulticastBeispiel();
        //Aufgaben1();
        //Aufgaben2();
        Aufgaben3();


        Console.WriteLine("\n--- Ende. Siehe README.md für Erklärung & Übungsaufgaben. ---");
    }



    // -----------------------------------------------------------------
    // Teil 1: Multicast - mehrere Methoden an EINEN Delegate hängen.
    // -----------------------------------------------------------------
    private static void MulticastBeispiel()
    {
        Console.WriteLine("--- Teil 1: Multicast-Delegate ---");

        // Ein Delegate ist "multicast-fähig": Mit += kann man weitere
        // Methoden anhängen. Beim Aufruf werden ALLE der Reihe nach
        // ausgeführt (wie eine kleine Benachrichtigungs-Kette / Pipeline).
        BenachrichtigungsHandler? benachrichtigen = null;

        benachrichtigen += NachrichtInKonsoleAusgeben;
        benachrichtigen += NachrichtProtokollieren;
        benachrichtigen += NachrichtAlsWichtigMarkieren;

        // Ein einziger Aufruf löst jetzt DREI Methoden aus:
        benachrichtigen?.Invoke("Server neu gestartet");
    }

    private static void NachrichtInKonsoleAusgeben(string nachricht)
        => Console.WriteLine($"[Konsole]  {nachricht}");

    private static void NachrichtProtokollieren(string nachricht)
        => Console.WriteLine($"[Log-Datei] Geschrieben: \"{nachricht}\"");

    private static void NachrichtAlsWichtigMarkieren(string nachricht)
        => Console.WriteLine($"[Priorität] '{nachricht}' als WICHTIG markiert");

    // -----------------------------------------------------------------
    // Teil 2: Mit -= kann man eine Methode wieder aus der Kette entfernen.
    // -----------------------------------------------------------------
    private static void AbmeldenBeispiel()
    {
        Console.WriteLine("--- Teil 2: Methode wieder abmelden (-=) ---");

        BenachrichtigungsHandler? benachrichtigen = NachrichtInKonsoleAusgeben;
        benachrichtigen += NachrichtProtokollieren;

        Console.WriteLine("Vor dem Entfernen:");
        benachrichtigen?.Invoke("Backup gestartet");

        // Protokollieren wird jetzt wieder entfernt.
        benachrichtigen -= NachrichtProtokollieren;

        Console.WriteLine("Nach dem Entfernen von NachrichtProtokollieren:");
        benachrichtigen?.Invoke("Backup abgeschlossen");
    }

    // -----------------------------------------------------------------
    // Teil 3: Man muss selten eigene Delegate-Typen deklarieren.
    // .NET bringt drei generische, fertige Delegate-Typen mit:
    //   - Action<...>     -> kein Rückgabewert
    //   - Func<..., TResult> -> letzter Typparameter ist der Rückgabewert
    //   - Predicate<T>    -> Func<T, bool>, meist für Filterbedingungen
    // -----------------------------------------------------------------
    private static void ActionFuncPredicateBeispiel()
    {
        Console.WriteLine("--- Teil 3: Action, Func, Predicate ---");

        // Action<string>: entspricht praktisch unserem BenachrichtigungsHandler,
        // aber ohne dass wir dafür einen eigenen Typ deklarieren mussten.
        Action<string> begruessen = name => Console.WriteLine($"Hallo, {name}!");
        begruessen("Alex");

        // Func<int, int, int>: zwei int-Parameter, int-Rückgabewert.
        Func<int, int, int> quadratsumme = (a, b) => a * a + b * b;
        Console.WriteLine($"quadratsumme(3, 4) = {quadratsumme(3, 4)}");

        // Predicate<int>: nimmt ein int, gibt bool zurück - typisch für Filter.
        Predicate<int> istGerade = zahl => zahl % 2 == 0;
        int[] zahlen = { 1, 2, 3, 4, 5, 6 };
        int[] geradeZahlen = Array.FindAll(zahlen, istGerade);
        Console.WriteLine($"Gerade Zahlen: {string.Join(", ", geradeZahlen)}");
    }

    // -----------------------------------------------------------------
    // Teil 4: Vorsicht bei Multicast-Delegates MIT Rückgabewert.
    // Wenn mehrere Methoden angehängt sind, wird beim Aufruf über
    // Invoke() nur der Rückgabewert der ZULETZT angehängten Methode
    // zurückgegeben - alle anderen Rückgabewerte gehen "verloren".
    // -----------------------------------------------------------------
    private static void RueckgabewertBeiMulticastBeispiel()
    {
        Console.WriteLine("--- Teil 4: Rückgabewert bei Multicast (Falle!) ---");

        Func<int, int> verdoppeln = zahl => zahl * 2;
        Func<int, int> quadrieren = zahl => zahl * zahl;

        Func<int, int> kombiniert = verdoppeln;
        kombiniert += quadrieren;

        // Nur das Ergebnis von "quadrieren" (zuletzt angehängt) kommt hier an!
        int ergebnis = kombiniert(5);
        Console.WriteLine($"kombiniert(5) liefert nur den letzten Rückgabewert = {ergebnis}");

        // Möchte man ALLE Ergebnisse einsammeln, muss man die einzelnen
        // Methoden über GetInvocationList() selbst durchgehen:
        Console.WriteLine("Alle Einzelergebnisse über GetInvocationList():");
        foreach (Delegate einzelDelegate in kombiniert.GetInvocationList())
        {
            var einzelFunc = (Func<int, int>)einzelDelegate;
            Console.WriteLine($"  -> {einzelFunc(5)}");
        }
    }

    private static void Aufgaben1()
    {
        //     1. * *Eigene Pipeline bauen**: Erstelle einen `Func<int, int>`-Multicast -
        //        Delegate mit drei Schritten(z.B. `+1`, `*2`, `-3`) und finde mit
        //`GetInvocationList()` heraus, welches Zwischenergebnis jeder Schritt für
        //sich alleine liefern würde.

        Func<int, int> myFunc = i => i + 1;
        myFunc += i => i / 0;
        myFunc += i => i - 3;

        foreach (Func<int, int> func in myFunc.GetInvocationList())
        {
            Console.WriteLine($"{func(5)}");
        }
    }

    private static void Aufgaben2()
    {
        Predicate<string> istUpperCase = s => s.First() == s.ToUpper().First();

        List<string> namen = ["Roman", "bob", "Alice"];

        namen.RemoveAll(name => istUpperCase(name));
        
        Console.WriteLine($"{string.Join(", ", namen)}");
    }

    private static void Aufgaben3()
    {
        //     4. * *Eigene Erweiterung * *: Baue eine kleine "Ereignis-Pipeline" für ein
        //Bestellsystem: Ein `Action<string>`-Delegate namens `bestellungAbgeschlossen`,
        //an den du drei Methoden hängst: `RechnungErstellen`, `LagerAktualisieren`,
        //`KundenEmailSenden`.

        Action<string> bestellungAbgeschlossen = RechnungErstellen;
        bestellungAbgeschlossen += LagerAktualisieren;
        bestellungAbgeschlossen += KundenEmailSenden;
        string bestellungNummer = "101 - 21 - 45";
        bestellungAbgeschlossen(bestellungNummer);
    }

    private static void KundenEmailSenden(string s)
    {
        Console.WriteLine($"[EmailSender]: Die Bestellung {s} ist abgeschloßen.");
    }

    private static void LagerAktualisieren(string s)
    {
        Console.WriteLine($"[LagerService]: Die Bestellung {s} ist ausgelilefert.");
    }

    private static void RechnungErstellen(string s)
    {
        Console.WriteLine($"[RechnungService]: Die Rechnung für die Bestellung {s} ist erstellt.");
    }
}
