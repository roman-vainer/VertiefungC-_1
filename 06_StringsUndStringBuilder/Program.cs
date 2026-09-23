// =====================================================================
// Mini-Projekt 6: Strings und StringBuilder im Zusammenspiel
// -----------------------------------------------------------------
// Ziel: Die wichtigsten String-Operationen (Erzeugen, Vergleichen,
// Suchen, Teilen/Verbinden, "Verändern") kurz an einem Stück
// durchspielen, und danach den Sprung zu StringBuilder an ZWEI
// bewusst unterschiedlichen Mustern zeigen:
//
//   Beispiel 1  ArtikellisteAnhaengen(StringBuilder sb, ...)
//               -> StringBuilder wird von AUSSEN übergeben (vorgegeben)
//   Beispiel 2  ErstelleKassenbon(...)
//               -> StringBuilder wird INNERHALB der Methode selbst
//                  erzeugt (new StringBuilder())
//
// Beide Muster kommen in echtem Code vor: mal baut man an einem
// bereits vorhandenen Puffer weiter (z. B. ein gemeinsamer Report, an
// dem mehrere Methoden nacheinander schreiben), mal ist eine Methode
// von Anfang bis Ende allein für ihren eigenen Text zuständig.
//
// Theorie zu allem hier: Foliensatz_Strings.
// =====================================================================

using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace StringsUndStringBuilder;

public static class Program
{

    public static void Main()
    {
        Console.WriteLine("=== Mini-Projekt 6: Strings und StringBuilder ===\n");

        Grundlagen_Erzeugung();
        Console.WriteLine();
        Grundlagen_Vergleich();
        Console.WriteLine();
        Grundlagen_Suchen();
        Console.WriteLine();
        Grundlagen_TeilenUndVerbinden();
        Console.WriteLine();
        Grundlagen_Veraendern();
        Console.WriteLine();
        Grundlagen_StringBuilder();
        Console.WriteLine();
        Beispiel1_StringBuilderVorgegeben();
        Console.WriteLine();
        Beispiel2_StringBuilderSelbstErstellt();
        Console.WriteLine();
        Beispiel3_PerformanceVergleich();

        Console.WriteLine("\n--- Ende. Siehe README.md für Erklärung & Übungsaufgaben. ---");
    }

    // ---------- Grundlagen: String-Erzeugung ----------
    private static void Grundlagen_Erzeugung()
    {
        Console.WriteLine("--- Grundlagen: String-Erzeugung ---");

        string literal = "Hallo Welt";
        string verbatim = @"C:\Kassen\Bons\heute.txt";
        string name = "Anna";
        int anzahl = 3;
        string interpoliert = $"{name} kauft {anzahl} Artikel";

        Console.WriteLine(literal);
        Console.WriteLine(verbatim);
        Console.WriteLine(interpoliert);
    }

    // ---------- Grundlagen: Vergleich ----------
    private static void Grundlagen_Vergleich()
    {
        Console.WriteLine("--- Grundlagen: Vergleich ---");

        string eingabe = "JA";

        Console.WriteLine(eingabe == "ja");                                              // False – Groß-/Kleinschreibung zählt
        Console.WriteLine(eingabe.Equals("ja", StringComparison.OrdinalIgnoreCase));     // True
        Console.WriteLine(string.Equals(eingabe.Trim(), "ja", StringComparison.OrdinalIgnoreCase));
    }

    // ---------- Grundlagen: Suchen ----------
    private static void Grundlagen_Suchen()
    {
        Console.WriteLine("--- Grundlagen: Suchen ---");

        string artikel = "Bio-Apfel 1kg";

        Console.WriteLine(artikel.Contains("Apfel"));
        Console.WriteLine(artikel.StartsWith("bio", StringComparison.OrdinalIgnoreCase));
        Console.WriteLine(artikel.IndexOf("1kg"));
        Console.WriteLine(artikel.IndexOf("Birne"));   // -1, nicht gefunden
    }

    // ---------- Grundlagen: Teilen und Verbinden ----------
    private static void Grundlagen_TeilenUndVerbinden()
    {
        Console.WriteLine("--- Grundlagen: Teilen und Verbinden ---");

        string csvZeile = "Apfel;0.99;3";
        string[] felder = csvZeile.Split(';');

        string artikelName = felder[0];
        decimal einzelpreis = decimal.Parse(felder[1], CultureInfo.InvariantCulture);
        int menge = int.Parse(felder[2]);

        string zusammengefasst = string.Join(" | ", felder);

        Console.WriteLine($"Artikel: {artikelName}, Preis: {einzelpreis}, Menge: {menge}");
        Console.WriteLine(zusammengefasst);
    }

    // ---------- Grundlagen: Verändern (= neu erzeugen) ----------
    private static void Grundlagen_Veraendern()
    {
        Console.WriteLine("--- Grundlagen: Verändern (= neu erzeugen) ---");

        string roh = "  bio-apfel  ";
        string bereinigt = roh.Trim().Replace("-", " ");
        string angezeigt = bereinigt.ToUpper();
        string artikelNr = "42".PadLeft(6, '0');

        Console.WriteLine($"'{roh}' -> '{angezeigt}'");
        Console.WriteLine($"Artikelnummer: {artikelNr}");
    }

    // ---------- Grundlagen: StringBuilder ----------
    private static void Grundlagen_StringBuilder()
    {
        Console.WriteLine("--- Grundlagen: StringBuilder ---");

        var sb = new StringBuilder();
        sb.Append("Kassenbon").AppendLine();
        sb.AppendLine(new string('=', 20));
        sb.AppendLine("Apfel        0,99 EUR");
        sb.Insert(0, ">> ");
        sb.Replace("Apfel", "Bio-Apfel");

        Console.WriteLine(sb.ToString());
        Console.WriteLine($"Aktuelle Länge: {sb.Length}");
    }

    // =====================================================================
    // Beispiel 1: StringBuilder wird VORGEGEBEN (Muster für Übungsaufgabe 1)
    // =====================================================================
    private static void Beispiel1_StringBuilderVorgegeben()
    {
        Console.WriteLine("--- Beispiel 1: StringBuilder wird vorgegeben ---");

        var artikel = new List<(string Name, decimal Preis)>
        {
            ("Apfel", 0.99m),
            ("Brot", 2.49m),
            ("Milch", 1.19m),
        };

        var sb = new StringBuilder();      // <-- wird HIER, beim Aufrufer, erzeugt ...
        sb.AppendLine("Artikelliste:");
        ArtikellisteAnhaengen(sb, artikel); // <-- ... und an die Methode WEITERGEGEBEN

        Console.WriteLine(sb.ToString());
    }

    // Bekommt einen bereits existierenden StringBuilder als Parameter.
    // Diese Methode erzeugt selbst KEINEN StringBuilder, sondern baut nur
    // am übergebenen Puffer weiter.
    private static void ArtikellisteAnhaengen(StringBuilder sb, IEnumerable<(string Name, decimal Preis)> artikel)
    {
        foreach (var (name, preis) in artikel)
        {
            sb.AppendLine($"{name,-15}{preis,8:C2}");
        }
    }

    // =====================================================================
    // Beispiel 2: StringBuilder wird SELBST ERSTELLT (Muster für Übungsaufgabe 2)
    // =====================================================================
    private static void Beispiel2_StringBuilderSelbstErstellt()
    {
        Console.WriteLine("--- Beispiel 2: StringBuilder wird selbst erstellt ---");

        var artikel = new List<(string Name, decimal Preis, int Menge)>
        {
            ("Apfel", 0.99m, 3),
            ("Brot", 2.49m, 1),
            ("Milch", 1.19m, 2),
        };

        string bon = ErstelleKassenbon("Filiale Mitte", artikel);
        Console.WriteLine(bon);
    }

    // Erzeugt den StringBuilder selbst — niemand übergibt ihn von außen.
    // Die Methode ist von Anfang (Kopf) bis Ende (Summenzeile) allein für
    // den kompletten Text zuständig und gibt am Schluss einen fertigen
    // string zurück.
    private static string ErstelleKassenbon(string filiale, IEnumerable<(string Name, decimal Preis, int Menge)> artikel)
    {
        var sb = new StringBuilder();      // <-- selbst erstellt, kein Parameter

        sb.AppendLine(filiale);
        sb.AppendLine(new string('-', 32));

        decimal summe = 0m;
        foreach (var (name, preis, menge) in artikel)
        {
            decimal zwischensumme = preis * menge;
            summe += zwischensumme;
            sb.AppendLine($"{name,-10}{menge,3}x{preis,7:C2}{zwischensumme,9:C2}");
        }

        sb.AppendLine(new string('-', 32));
        sb.Append("Summe:".PadRight(23)).AppendLine(summe.ToString("C2").PadLeft(9));

        return sb.ToString();
    }

    // ---------- Beispiel 3: Performance += vs. StringBuilder ----------
    private static void Beispiel3_PerformanceVergleich()
    {
        Console.WriteLine("--- Beispiel 3: Performance += vs. StringBuilder ---");

        const int anzahl = 50_000;

        var swVerkettung = Stopwatch.StartNew();
        string ergebnisVerkettung = "";
        for (int i = 0; i < anzahl; i++)
        {
            ergebnisVerkettung += i + ",";
        }
        swVerkettung.Stop();

        var swBuilder = Stopwatch.StartNew();
        var sb = new StringBuilder();
        for (int i = 0; i < anzahl; i++)
        {
            sb.Append(i).Append(',');
        }
        string ergebnisBuilder = sb.ToString();
        swBuilder.Stop();

        Console.WriteLine($"Verkettung (+=):  {swVerkettung.ElapsedMilliseconds} ms  (Länge: {ergebnisVerkettung.Length})");
        Console.WriteLine($"StringBuilder:    {swBuilder.ElapsedMilliseconds} ms  (Länge: {ergebnisBuilder.Length})");
    }
}
