namespace EventBeispiele;

public class Tuer
{
    public event EventHandler<TuerSchliesstEventArgs>? SchliesstSich;

    public void Schliessen()
    {
        var e = new TuerSchliesstEventArgs();
        SchliesstSich?.Invoke(this, e);

        if (e.Abbrechen)
        {
            Console.WriteLine("[Tür] Schließen wurde abgebrochen - Tür bleibt offen.");
        }
        else
        {
            Console.WriteLine("[Tür] Tür ist jetzt geschlossen.");
        }
    }
}
