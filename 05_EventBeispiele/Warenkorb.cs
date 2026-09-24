namespace EventBeispiele;

public class Warenkorb
{
    private decimal _summe;

    public event EventHandler<ArtikelHinzugefuegtEventArgs>? ArtikelHinzugefuegt;

    public void ArtikelHinzufuegen(string artikelName, decimal preis, string kategorie)
    {
        _summe += preis;
        Console.WriteLine($"[Warenkorb] '{artikelName}' für {preis:C} hinzugefügt.");

        ArtikelHinzugefuegt?.Invoke(this, new ArtikelHinzugefuegtEventArgs(artikelName, preis, _summe, kategorie));
    }
}
