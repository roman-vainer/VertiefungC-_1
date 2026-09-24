namespace Generics;

public class Zuordnung<TSchluessel, TWert> where TSchluessel : notnull
{
    private readonly Dictionary<TSchluessel, TWert> _daten = new();

    public void Hinzufuegen(TSchluessel schluessel, TWert wert) => _daten[schluessel] = wert;

    public TWert Abrufen(TSchluessel schluessel) => _daten[schluessel];
}
