namespace EventBeispiele;

public class Bewegungsmelder
{
    private readonly bool _erkenntBewegungImRahmen;

    public Bewegungsmelder(bool erkenntBewegungImRahmen) => _erkenntBewegungImRahmen = erkenntBewegungImRahmen;

    public void PruefenAlsReaktionAufSchliessen(object? sender, TuerSchliesstEventArgs e)
    {
        if (_erkenntBewegungImRahmen)
        {
            Console.WriteLine("  [Bewegungsmelder] Jemand steht im Türrahmen - breche ab!");
            e.Abbrechen = true;
        }
        else
        {
            Console.WriteLine("  [Bewegungsmelder] Türrahmen frei.");
        }
    }
}
