// =====================================================================
// Mini-Projekt 5: Drei Event-Beispiele, EINE steigende Linie
// -----------------------------------------------------------------
// Ziel: Events, EventHandler und EventArgs nicht an einem einzigen
// Beispiel sehen, sondern an drei bewusst verschiedenen, die genau
// EINE Sache variieren: wie viel (und welche Art von) Daten das
// EventArgs-Objekt transportiert.
//
//   Beispiel 1  Wecker            -> KEINE Zusatzdaten     -> EventHandler
//   Beispiel 2  Warenkorb         -> NUR-LESBARE Zusatzdaten -> EventHandler<T>, eigene EventArgs
//   Beispiel 3  Tuer              -> LESE-/SCHREIBBARE Daten -> EventHandler<T>, Event kann "zurückreden"
//
// Beispiel 3 ist der eigentliche Aha-Moment: EventArgs ist ein Objekt
// (Referenztyp). Ein Subscriber kann eine beschreibbare Eigenschaft
// darauf setzen, und der Publisher liest sie NACH dem Invoke wieder
// aus. So funktioniert z. B. das reale .NET-Muster mit CancelEventArgs
// (etwa FormClosing in WinForms).
// =====================================================================

namespace EventBeispiele;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Mini-Projekt 5: Drei Event-Beispiele ===\n");

        Beispiel1_Wecker();
        Console.WriteLine();
        Beispiel2_Warenkorb();
        Console.WriteLine();
        Beispiel3_Tuer();

        Console.WriteLine("\n--- Ende. Siehe README.md für Erklärung & Übungsaufgaben. ---");
    }

    private static void Beispiel1_Wecker()
    {
        Console.WriteLine("--- Beispiel 1: Wecker (EventHandler, keine Zusatzdaten) ---");

        var wecker = new Wecker("07:00");
        var licht = new Schlafzimmerlicht();
        var kaffeemaschine = new Kaffeemaschine();

        wecker.Klingelt += licht.AnAlsReaktionAufKlingeln;
        wecker.Klingelt += kaffeemaschine.StartenAlsReaktionAufKlingeln;

        wecker.ZeitAblaufenLassen();
    }

    private static void Beispiel2_Warenkorb()
    {
        Console.WriteLine("--- Beispiel 2: Warenkorb (EventHandler<T>, nur-lesbare EventArgs) ---");

        var warenkorb = new Warenkorb();
        var rabattpruefer = new Rabattpruefer();
        var bonuspunkte = new Bonuspunkteberechnung();

        warenkorb.ArtikelHinzugefuegt += rabattpruefer.PruefenAlsReaktionAufHinzufuegen;
        warenkorb.ArtikelHinzugefuegt += bonuspunkte.PunkteGutschreibenAlsReaktionAufHinzufuegen;

        warenkorb.ArtikelHinzufuegen("Tastatur", 25.00m);
        warenkorb.ArtikelHinzufuegen("Monitor", 30.00m);
    }

    private static void Beispiel3_Tuer()
    {
        Console.WriteLine("--- Beispiel 3: Tür (EventHandler<T>, beschreibbare/abbrechbare EventArgs) ---");

        Console.WriteLine("\n- Durchgang 1: Türrahmen frei ->");
        var tuer1 = new Tuer();
        tuer1.SchliesstSich += new Bewegungsmelder(erkenntBewegungImRahmen: false).PruefenAlsReaktionAufSchliessen;
        tuer1.SchliesstSich += new Tuerprotokoll().ProtokollierenAlsReaktionAufSchliessen;
        tuer1.Schliessen();

        Console.WriteLine("\n- Durchgang 2: jemand steht im Türrahmen ->");
        var tuer2 = new Tuer();
        tuer2.SchliesstSich += new Bewegungsmelder(erkenntBewegungImRahmen: true).PruefenAlsReaktionAufSchliessen;
        tuer2.SchliesstSich += new Tuerprotokoll().ProtokollierenAlsReaktionAufSchliessen;
        tuer2.Schliessen();
    }
}
