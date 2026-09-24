namespace EventBeispiele;

public class Rabattpruefer
{
    private const decimal RabattSchwelle = 50m;

    public void PruefenAlsReaktionAufHinzufuegen(object? sender, ArtikelHinzugefuegtEventArgs e)
    {
        if (e.Kategorie == "Elektronik")
        {
            if (e.NeueSumme >= RabattSchwelle)
            {
                Console.WriteLine($"  [Rabattprüfer] Warenkorbsumme {e.NeueSumme:C} >= {RabattSchwelle:C} -> 10% Rabatt freigeschaltet!");
            }
            else
            {
                Console.WriteLine($"  [Rabattprüfer] Noch {(RabattSchwelle - e.NeueSumme):C} bis zum Rabatt.");
            }
        }
    }
}
