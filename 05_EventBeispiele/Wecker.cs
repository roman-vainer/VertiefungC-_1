namespace EventBeispiele;

public class Wecker
{
    private readonly string _uhrzeit;

    public Wecker(string uhrzeit) => _uhrzeit = uhrzeit;

    public event EventHandler? Klingelt;

    public void ZeitAblaufenLassen()
    {
        Console.WriteLine($"[Wecker] {_uhrzeit} Uhr erreicht.");
        Klingelt?.Invoke(this, EventArgs.Empty);
    }
}
