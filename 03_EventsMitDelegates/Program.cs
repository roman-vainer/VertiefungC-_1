// =====================================================================
// Mini-Projekt 3: Events - Delegates "richtig verpackt"
// -----------------------------------------------------------------
// Ziel: Verstehen, dass ein Event im Grunde "nur" ein Delegate mit
// eingeschränktem Zugriff ist (Publisher/Subscriber-Muster), eigene
// EventArgs-Klassen bauen und am Ende das .NET-Standardmuster
// EventHandler<T> kennenlernen.
// =====================================================================

namespace EventsMitDelegates;

// -----------------------------------------------------------------
// Schritt 1: Eigene EventArgs-Klasse.
// Konvention in .NET: Event-Daten werden in einer Klasse gebündelt,
// die von EventArgs erbt. So kann man später beliebig mehr Infos
// hinzufügen, ohne die Delegate-Signatur ändern zu müssen.
// -----------------------------------------------------------------
public class TemperaturEventArgs : EventArgs
{
    public double AktuelleTemperatur { get; }
    public double Schwellenwert { get; }
    public DateTime Zeitpunkt { get; } = DateTime.Now;

    public TemperaturEventArgs(double aktuelleTemperatur, double schwellenwert)
    {
        AktuelleTemperatur = aktuelleTemperatur;
        Schwellenwert = schwellenwert;
    }
}

// -----------------------------------------------------------------
// Schritt 2: Eigener Delegate-Typ speziell für dieses Event.
// Konvention: (object? sender, TEventArgs e)
// -----------------------------------------------------------------
public delegate void TemperaturUeberschrittenHandler(object? sender, TemperaturEventArgs e);

// -----------------------------------------------------------------
// Schritt 3: Der "Publisher" (Herausgeber). Er entscheidet, WANN ein
// Event ausgelöst wird - aber nicht, WAS dann passiert. Das
// überlässt er den Abonnenten (Subscribern).
// -----------------------------------------------------------------
public class Temperatursensor
{
    private readonly double _schwellenwert;
    private double _aktuellnwert;

    public Temperatursensor(double schwellenwert)
    {
        _schwellenwert = schwellenwert;
        _aktuellnwert = 27.0;
    }

    // Das "event"-Schlüsselwort macht aus dem Delegate ein echtes Event:
    // - Von AUSSERHALB der Klasse darf man nur += und -= verwenden.
    // - Von AUSSERHALB darf man das Event NICHT direkt aufrufen (Invoke)
    //   oder mit "=" überschreiben. Nur die Klasse selbst darf das.
    // Das schützt vor Fehlern wie "ein Subscriber löscht versehentlich
    // alle anderen Subscriber".
    public event TemperaturUeberschrittenHandler? TemperaturUeberschritten;
    public event EventHandler<TemperaturEventArgs> TemperaturNormalisiert;

    public void MessungSimulieren(double gemesseneTemperatur)
    {
        Console.WriteLine($"[Sensor] Messung: {gemesseneTemperatur} °C");

        if (gemesseneTemperatur > _schwellenwert)
        {
            _aktuellnwert = gemesseneTemperatur;
            // "OnEventName"-Konvention: eine geschützte Methode, die das
            // Event auslöst. So können abgeleitete Klassen das Event
            // ebenfalls sauber auslösen.
            OnTemperaturUeberschritten(new TemperaturEventArgs(gemesseneTemperatur, _schwellenwert));
        }

        if(_aktuellnwert > _schwellenwert && gemesseneTemperatur < _schwellenwert)
        {
            _aktuellnwert -= gemesseneTemperatur;
            OnTemperaturNormalisiert(new TemperaturEventArgs(gemesseneTemperatur, _schwellenwert));
        }
    }

    private void OnTemperaturNormalisiert(TemperaturEventArgs e)
    {
        TemperaturNormalisiert?.Invoke(this, e);
    }

    protected virtual void OnTemperaturUeberschritten(TemperaturEventArgs e)
    {
        // "?.Invoke" ist wichtig: Falls sich NIEMAND angemeldet hat,
        // ist das Event null, und ein direkter Aufruf würde eine
        // NullReferenceException werfen.
        TemperaturUeberschritten?.Invoke(this, e);
    }
}

// -----------------------------------------------------------------
// Schritt 4: "Subscriber" (Abonnenten). Sie kennen den Sensor, aber
// der Sensor kennt SIE nicht direkt - er kennt nur "irgendwelche
// Methoden mit der passenden Signatur, die sich angemeldet haben".
// Das ist lose Kopplung.
// -----------------------------------------------------------------
public class Alarmanlage
{
    public void AufTemperatur_Reagieren(object? sender, TemperaturEventArgs e)
    {
        Console.WriteLine($"  [Alarmanlage] [{e.Zeitpunkt}] ALARM! {e.AktuelleTemperatur} °C > Schwellenwert {e.Schwellenwert} °C");
    }

    public void AufNormalisierung_Reagieren(object? sender, TemperaturEventArgs e)
    {
        Console.WriteLine($"  [Alarmanlage] [{e.Zeitpunkt}] Temperature wurde normalisiertt! - Aktueller Temperatur ist {e.AktuelleTemperatur} °C ");
    }

}

public class BenachrichtigungsDienst
{
    public void SendeEmail(object? sender, TemperaturEventArgs e)
    {
        Console.WriteLine($"  [E-Mail-Dienst] [{e.Zeitpunkt}] E-Mail versendet: Temperatur liegt bei {e.AktuelleTemperatur} °C.");
    }
}

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Mini-Projekt 3: Events mit eigenem Delegate ===\n");

        var sensor = new Temperatursensor(schwellenwert: 30.0);
        var alarmanlage = new Alarmanlage();
        var emailDienst = new BenachrichtigungsDienst();

        // Anmelden (Subscriben) über +=. Der Sensor "weiss" nichts von
        // Alarmanlage oder BenachrichtigungsDienst als Klassen - er ruft
        // nur die angemeldeten Methoden auf.
        sensor.TemperaturUeberschritten += alarmanlage.AufTemperatur_Reagieren;
        sensor.TemperaturUeberschritten += alarmanlage.AufTemperatur_Reagieren;
        sensor.TemperaturUeberschritten += emailDienst.SendeEmail;
        sensor.TemperaturNormalisiert += alarmanlage.AufNormalisierung_Reagieren;
        sensor.TemperaturNormalisiert += emailDienst.SendeEmail;

        Console.WriteLine("--- Messung 1: unauffällig ---");
        sensor.MessungSimulieren(22.5);

        Console.WriteLine("\n--- Messung 2: Schwellenwert überschritten (beide Abonnenten reagieren) ---");
        sensor.MessungSimulieren(35.0);

        sensor.MessungSimulieren(20.0);

        // Abmelden (Unsubscriben) über -=. Danach reagiert nur noch
        // der verbleibende Abonnent.
        sensor.TemperaturUeberschritten -= emailDienst.SendeEmail;

        Console.WriteLine("\n--- E-Mail-Dienst hat sich abgemeldet ---");
        Console.WriteLine("--- Messung 3: nur noch die Alarmanlage reagiert ---");
        sensor.MessungSimulieren(40.0);

        // Anonyme Methode / Lambda als Subscriber - genauso möglich wie
        // eine "normale" Methode.
        sensor.TemperaturUeberschritten += (sender, e) =>
            Console.WriteLine($"  [Inline-Lambda] Ich wurde auch benachrichtigt: {e.AktuelleTemperatur} °C");

        Console.WriteLine("\n--- Messung 4: Alarmanlage + Inline-Lambda reagieren ---");
        sensor.MessungSimulieren(50.0);

        Console.WriteLine("\n--- Bonus: Das .NET-Standardmuster EventHandler<T> ---");
        StandardEventPatternBeispiel();

        Console.WriteLine("\n--- Ende. Siehe README.md für Erklärung & Übungsaufgaben. ---");
    }

    // .NET bringt für genau dieses (sender, EventArgs)-Muster bereits einen
    // generischen Delegate-Typ mit: EventHandler<TEventArgs>.
    // In der Praxis deklariert man daher SELTEN einen eigenen Delegate-Typ
    // wie "TemperaturUeberschrittenHandler" - man nutzt direkt:
    //   event EventHandler<TemperaturEventArgs> TemperaturUeberschritten;
    private static void StandardEventPatternBeispiel()
    {
        var sensor2 = new StandardTemperatursensor(schwellenwert: 25.0);
        sensor2.TemperaturUeberschritten += (sender, e) =>
            Console.WriteLine($"  [Standardmuster] {e.AktuelleTemperatur} °C > {e.Schwellenwert} °C");

        sensor2.MessungSimulieren(28.0);
    }
}

// Gleiches Beispiel, aber mit dem eingebauten EventHandler<T> statt eines
// eigenen Delegate-Typs - das ist der in der Praxis übliche Weg.
public class StandardTemperatursensor
{
    private readonly double _schwellenwert;

    public StandardTemperatursensor(double schwellenwert) => _schwellenwert = schwellenwert;

    public event EventHandler<TemperaturEventArgs>? TemperaturUeberschritten;

    public void MessungSimulieren(double gemesseneTemperatur)
    {
        if (gemesseneTemperatur > _schwellenwert)
        {
            TemperaturUeberschritten?.Invoke(this, new TemperaturEventArgs(gemesseneTemperatur, _schwellenwert));
        }
    }
}
