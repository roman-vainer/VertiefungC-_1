using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyList;

public class MyList<T>(int capacity = 0)
{
    const int DefaultCapacity = 8;
    private T[] _items = new T[capacity];
    public int Count { get; private set; } = 0;
    public int Capacity() => _items.Length;

    public void Add(T element)
    {
        CheckCapacity();
        _items[Count++] = element;
    }

    public void Insert(int index, T element)
    {
        if (index > Count && index < 0)
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

    public bool Remove(T element)
    {
        int indexOfElement = FindIndexOfElement(element);
        return indexOfElement >= 0 && RemoveElement(indexOfElement);

    }

    public bool RemoveAt(int index)
    {
        if (index > Count && index < 0)
        {
            throw new ArgumentOutOfRangeException($"Index {index} is ouside of Range");
        }
        return RemoveElement(index);
    }
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

    private int FindIndexOfElement(T element) =>
        Array.IndexOf(_items, element, 0, Count);

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

    public override string ToString() => String.Join(", ", _items.Take(Count));

    private void CheckCapacity()
    {
        if (_items.Length <= Count)
        {
            ResizeArray();
        }
    }

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
