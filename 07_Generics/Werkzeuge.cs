namespace Generics;

public static class Werkzeuge
{
    public static void Tausche<T>(ref T a, ref T b)
    {
        T zwischenspeicher = a;
        a = b;
        b = zwischenspeicher;
    }

    public static T Groesseres<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }

    public static T? ErsterOderStandard<T>(IList<T> liste)
    {
        if (liste.Count == 0)
            return default;

        return liste[0];
    }
}
