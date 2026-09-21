namespace EventBeispiele;

public class Tuerprotokoll
{
    public void ProtokollierenAlsReaktionAufSchliessen(object? sender, TuerSchliesstEventArgs e)
        => Console.WriteLine($"  [Protokoll] Schließversuch registriert (bisher Abbrechen={e.Abbrechen}).");
}
