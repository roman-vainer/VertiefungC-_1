namespace EventBeispiele;

public class Bonuspunkteberechnung
{
    public void PunkteGutschreibenAlsReaktionAufHinzufuegen(object? sender, ArtikelHinzugefuegtEventArgs e)
    {
        int punkte = (int)(e.Preis / 10);
        Console.WriteLine($"  [Bonuspunkte] +{punkte} Punkte für '{e.ArtikelName}'.");
    }
}
