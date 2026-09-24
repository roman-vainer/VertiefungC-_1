using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyList;

// checktest gptchat
// Привет, Лев.
public class MyList<T>(int capacity = 0)
{
    const int DefaultCapacity = 8;
    private T[] _items = new T[capacity];
    public int Count { get; private set; } = 0;

    // Gibt die aktuelle Kapazität des internen Arrays zurück.
    public int Capacity => _items.Length;

    // Fügt ein Element am Ende der Liste hinzu.
    public void Add(T element)
    {
        CheckCapacity();
        _items[Count++] = element;
    }

    // Fügt ein Element an der angegebenen Position ein.
    public void Insert(int index, T element)
    {
        if (index > Count || index < 0)
        {
            throw new ArgumentOutOfRangeException($"Index {index} is ouside of Range");
        }
        CheckCapacity();

        for (int i = Count; i > index; i--)
        {
            _items[i] = _items[i - 1];
        }
        _items[index] = element;
        Count++;
    }

    // Entfernt das erste Vorkommen des angegebenen Elements.
    public bool Remove(T element)
    {
        int indexOfElement = FindIndexOfElement(element);
        return indexOfElement >= 0 && RemoveElement(indexOfElement);

    }

    // Entfernt das Element an der angegebenen Position.
    public bool RemoveAt(int index)
    {
        if (index >= Count || index < 0)
        {
            throw new ArgumentOutOfRangeException($"Index {index} is ouside of Range");
        }
        return RemoveElement(index);
    }

    // Entfernt alle Elemente, die die angegebene Bedingung erfüllen.
    // Gibt die Anzahl der entfernten Elemente zurück.
    public int RemoveAll(Predicate<T> match)
    {
        int freeIndex = 0;
        int oldCount = Count;
        for (int i = 0; i < Count; i++)
        {
            if (!match(_items[i]))
            {
                _items[freeIndex] = _items[i];
                freeIndex++;
            }
        }
        Count = freeIndex;
        Array.Clear(_items, Count, oldCount - Count);
        return oldCount - Count;
    }

    // Sucht den Index des angegebenen Elements.
    // Gibt -1 zurück, wenn das Element nicht gefunden wurde.
    private int FindIndexOfElement(T element) => Array.IndexOf(_items, element, 0, Count);

    // Entfernt ein Element anhand seines Indexes
    // und verschiebt die nachfolgenden Elemente nach links.
    private bool RemoveElement(int indexOfElement)
    {
        for (int i = indexOfElement; i < Count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        Count--;
        Array.Clear(_items, Count, 1);
        return true;
    }

    // Gibt alle gespeicherten Elemente als Zeichenkette zurück.
    public override string ToString() => String.Join(", ", _items.Take(Count));

    // Prüft, ob im internen Array noch genügend Speicherplatz vorhanden ist.
    private void CheckCapacity()
    {
        if (_items.Length <= Count)
        {
            ResizeArray();
        }
    }

    // Vergrößert das interne Array, wenn die aktuelle Kapazität nicht ausreicht.
    private void ResizeArray()
    {
        T[] newBucket = new T[_items.Length == 0 ? DefaultCapacity : _items.Length * 2];
        for (int i = 0; i < _items.Length; i++)
        {
            newBucket[i] = _items[i];
        }
        _items = newBucket;
    }
}
