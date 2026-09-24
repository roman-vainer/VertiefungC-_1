namespace Generics;

// Variante B: Typparameter wird weitergereicht — die abgeleitete Klasse
// bleibt selbst generisch. "new" vor Push: bewusstes METHOD HIDING, kein
// Overriding, weil Stapel<T>.Push nicht virtual ist.
public class ProtokollierterStapel<T> : Stapel<T>
{
    public new void Push(T element)
    {
        Console.WriteLine($"Push: {element}");
        base.Push(element);
    }
}
