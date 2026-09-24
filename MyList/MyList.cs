using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyList;


public class MyList<T>
{
    const int DefaultCapasity = 8;
    private T[] _items;
    public int Count { get; private set; }

    public MyList(int capacity = 0)
    {
        _items = new T[capacity];
        Count = 0;
    }

    public void Add(T element)
    {
        CheckCapacity();
        _items[Count++] = element;
    }

    public void Add(int index, T element)
    {
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

    public bool RemoveAt(int index) =>
        index < Count && RemoveElement(index);

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
        return true;
    }

    public int Capacity() => _items.Length;

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
        T[] newBucket = new T[_items.Length == 0 ? DefaultCapasity : _items.Length * 2];
        for (int i = 0; i < _items.Length; i++)
        {
            newBucket[i] = _items[i];
        }
        _items = newBucket;
    }




}
