namespace EventBeispiele;

public class Kaffeemaschine
{
    public void StartenAlsReaktionAufKlingeln(object? sender, EventArgs e)
        => Console.WriteLine("  [Kaffeemaschine] Brühvorgang gestartet.");
}
