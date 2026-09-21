namespace EventBeispiele;

public class ArtikelHinzugefuegtEventArgs : EventArgs
{
    public string ArtikelName { get; }
    public decimal Preis { get; }
    public decimal NeueSumme { get; }

    public ArtikelHinzugefuegtEventArgs(string artikelName, decimal preis, decimal neueSumme)
    {
        ArtikelName = artikelName;
        Preis = preis;
        NeueSumme = neueSumme;
    }
}
