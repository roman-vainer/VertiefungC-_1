namespace Generics;

public class Stapel<T>
{
    private readonly List<T> _elemente = new();

    public void Push(T element) => _elemente.Add(element);

    public T Pop()
    {
        int letzterIndex = _elemente.Count - 1;
        T element = _elemente[letzterIndex];
        _elemente.RemoveAt(letzterIndex);
        return element;
    }

    public int Anzahl => _elemente.Count;
}
