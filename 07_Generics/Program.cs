// =====================================================================
// Mini-Projekt 7: Generics
// -----------------------------------------------------------------
// Ziel: Die wichtigsten Generics-Konzepte (eigene generische Klasse,
// generische Methode, Constraints, default(T), mehrere Typparameter)
// kurz an einem Stück durchspielen, und danach an ZWEI bewusst
// unterschiedlichen Vererbungs-Mustern zeigen, was beim Erben von
// einer generischen Basisklasse passiert:
//
//   Beispiel 1  IntStapel : Stapel<int>
//               -> Typparameter wird beim Erben FESTGELEGT
//   Beispiel 2  ProtokollierterStapel<T> : Stapel<T>
//               -> Typparameter wird WEITERGEREICHT, die Klasse
//                  bleibt selbst generisch (inkl. "new void Push" —
//                  Method Hiding, kein Overriding, da Stapel<T>.Push
//                  nicht virtual ist)
//
// Beispiel 3 macht zusätzlich den Boxing-Unterschied zwischen
// ArrayList und List<int> performance-mäßig sichtbar.
//
// Theorie zu allem hier: Foliensatz_03_Generics.
// =====================================================================

using System.Collections;
using System.Diagnostics;

namespace Generics;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Mini-Projekt 7: Generics ===\n");

        Grundlagen_GenerischeKlasse();
        Console.WriteLine();
        Grundlagen_GenerischeMethode();
        Console.WriteLine();
        Grundlagen_ConstraintsUndVergleich();
        Console.WriteLine();
        Grundlagen_DefaultT();
        Console.WriteLine();
        Grundlagen_MehrereTypparameter();
        Console.WriteLine();
        Beispiel1_TypparameterFestgelegt();
        Console.WriteLine();
        Beispiel2_TypparameterWeitergereicht();
        Console.WriteLine();
        Beispiel3_BoxingVermeidung();

        Console.WriteLine("\n--- Ende. Siehe README.md für Erklärung & Übungsaufgaben. ---");
    }

    // ---------- Grundlagen: Eigene generische Klasse ----------
    private static void Grundlagen_GenerischeKlasse()
    {
        Console.WriteLine("--- Grundlagen: Eigene generische Klasse (Stapel<T>) ---");

        var textStapel = new Stapel<string>();
        textStapel.Push("A");
        textStapel.Push("B");
        Console.WriteLine(textStapel.Pop());   // "B"
        Console.WriteLine($"Anzahl: {textStapel.Anzahl}");





    }

    // ---------- Grundlagen: Generische Methode ----------
    private static void Grundlagen_GenerischeMethode()
    {
        Console.WriteLine("--- Grundlagen: Generische Methode (Tausche<T>) ---");

        int x = 1, y = 2;
        Werkzeuge.Tausche(ref x, ref y);
        Console.WriteLine($"{x}, {y}");   // 2, 1

        string s1 = "Hallo", s2 = "Welt";
        Werkzeuge.Tausche(ref s1, ref s2);
        Console.WriteLine($"{s1}, {s2}");





    }

    // ---------- Grundlagen: Constraints + Vergleich ----------
    private static void Grundlagen_ConstraintsUndVergleich()
    {
        Console.WriteLine("--- Grundlagen: Constraints + Vergleich (Groesseres<T>) ---");

        Console.WriteLine(Werkzeuge.Groesseres(3, 7));            // 7
        Console.WriteLine(Werkzeuge.Groesseres("Apfel", "Birne")); // Birne





    }

    // ---------- Grundlagen: default(T) ----------
    private static void Grundlagen_DefaultT()
    {
        Console.WriteLine("--- Grundlagen: default(T) ---");

        Console.WriteLine(Werkzeuge.ErsterOderStandard(new List<int>()));         // 0
        Console.WriteLine($"'{Werkzeuge.ErsterOderStandard(new List<string>())}'"); // '' (null)





    }

    // ---------- Grundlagen: Mehrere Typparameter ----------
    private static void Grundlagen_MehrereTypparameter()
    {
        Console.WriteLine("--- Grundlagen: Mehrere Typparameter (Zuordnung<TSchluessel, TWert>) ---");

        var zuordnung = new Zuordnung<string, int>();
        zuordnung.Hinzufuegen("Anna", 7);
        Console.WriteLine(zuordnung.Abrufen("Anna"));





    }

    // =====================================================================
    // Beispiel 1: Typparameter wird beim Erben FESTGELEGT (Variante A)
    // =====================================================================
    private static void Beispiel1_TypparameterFestgelegt()
    {
        Console.WriteLine("--- Beispiel 1: Typparameter beim Erben festgelegt (IntStapel) ---");

        var stapel = new IntStapel();   // kein <T> mehr nötig — T ist bereits int
        stapel.Push(10);
        stapel.Push(20);
        Console.WriteLine(stapel.Pop());   // 20
        Console.WriteLine($"Anzahl: {stapel.Anzahl}");
    }

    // =====================================================================
    // Beispiel 2: Typparameter wird beim Erben WEITERGEREICHT (Variante B)
    // =====================================================================
    private static void Beispiel2_TypparameterWeitergereicht()
    {
        Console.WriteLine("--- Beispiel 2: Typparameter weitergereicht (ProtokollierterStapel<T>) ---");

        var protokolliert = new ProtokollierterStapel<string>();
        protokolliert.Push("A");   // schreibt zusätzlich "Push: A"
        protokolliert.Push("B");
        Console.WriteLine(protokolliert.Pop());   // "B"
    }

    // ---------- Beispiel 3: Boxing vermeiden (List<int> vs. ArrayList) ----------
    private static void Beispiel3_BoxingVermeidung()
    {
        Console.WriteLine("--- Beispiel 3: Boxing vermeiden (List<int> vs. ArrayList) ---");

        const int anzahl = 500_000;

        var swArrayList = Stopwatch.StartNew();
        var arrayListe = new ArrayList();
        for (int i = 0; i < anzahl; i++)
        {
            arrayListe.Add(i);       // jedes int wird geboxt
        }
        swArrayList.Stop();

        var swListT = Stopwatch.StartNew();
        var listeT = new List<int>();
        for (int i = 0; i < anzahl; i++)
        {
            listeT.Add(i);            // kein Boxing
        }
        swListT.Stop();

        Console.WriteLine($"ArrayList (mit Boxing):  {swArrayList.ElapsedMilliseconds} ms");
        Console.WriteLine($"List<int> (ohne Boxing): {swListT.ElapsedMilliseconds} ms");
    }
}


